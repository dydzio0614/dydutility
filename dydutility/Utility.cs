﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using ChatterBotAPI;
using System.IO;

namespace dydutility
{
    public partial class Utility
    {
        private IntPtr consoleWriteHandle;
        private IntPtr consoleReadHandle;
        private IntPtr gameProcessHandle;

        private bool gameHooked;

        public bool GameHooked { get { return gameHooked; } }
        private bool privateMode = false;
        private bool restrictMode = false;
        private bool teleportMode = false;

        private readonly PlayerData userData = new PlayerData();

        public string StateMsg { get; private set; }

        private int? duelRestrict = null;

        private List<string> tournamentParticipants = new List<string>();
        private List<BonusCommand> customCommands;

        private ChatterBotSession cleverBotSession;

        public Utility()
        {
            ChatterBotFactory botFactory = new ChatterBotFactory();
            ChatterBot bot = botFactory.Create(ChatterBotType.CLEVERBOT);
            cleverBotSession = bot.CreateSession();

            if (File.Exists("commands.json"))
            {
                using (FileStream commandsFile = new FileStream("commands.json", FileMode.Open, FileAccess.Read))
                {
                    DataContractJsonSerializer deserializer = new DataContractJsonSerializer(typeof(BonusCommandList));
                    BonusCommandList output = deserializer.ReadObject(commandsFile) as BonusCommandList;
                    customCommands = output?.CommandList;
                }
            }
        }

        public bool FindJKAConsole()
        {
            if (gameHooked)
                return true;

            bool hookSucceeded = HookHelper.HookGame(ref consoleWriteHandle, ref consoleReadHandle, ref gameProcessHandle);

            if (hookSucceeded)
            {
                gameHooked = true;
                return true;
            }
            else
                return false;
        }

        public void GetUserData()
        {
            IntPtr buffer = new IntPtr();
            WinAPIHelper.ReadProcessMemory(gameProcessHandle, (IntPtr)HookHelper.PlayerClientNumAddress, ref buffer, 4, IntPtr.Zero); //get clientnum
            userData.ClientNum = buffer.ToInt32();
            userData.Name = GetPlayerName(userData.ClientNum);
        }

        public string GetPlayerName(int clientNum)
        {
            if (clientNum < 0 && clientNum >= 32)
                return null;

            IntPtr playerdataOffset = new IntPtr();
            WinAPIHelper.ReadProcessMemory(gameProcessHandle, (IntPtr)0x977d54 + 4 * (1131 + clientNum), ref playerdataOffset, 4, IntPtr.Zero); //get

            if (playerdataOffset.ToInt32() == 0)
                return null;

            StringBuilder clientData = new StringBuilder(2000);
            WinAPIHelper.ReadProcessMemory(gameProcessHandle, new IntPtr(0x9797e4 + playerdataOffset.ToInt32()), clientData, 1024, IntPtr.Zero);
            string[] stats = clientData.ToString().Split('\\');
            return RemoveColorModifiers(stats[1]); //hardcoded name offset
        }

        public string ReadConsole()
        {
            StringBuilder temp = new StringBuilder(50000);
            int consoleLength = WinAPIHelper.SendMessage(consoleReadHandle, WinAPIHelper.WM_GETTEXT, 50000, temp);
            
            return temp.ToString();
        }

        public string GetLastChatLine(string chatLog)
        {
            if (string.IsNullOrEmpty(chatLog)) return chatLog;

            string[] chatLines = chatLog.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            string lastLine = chatLines[chatLines.Count() - 2];
            return lastLine;
        }

        public string RemoveColorModifiers(string rawMsg) //make it string extension method?
        {
            string pattern = @"\^[0-9]";
            string processedMsg = System.Text.RegularExpressions.Regex.Replace(rawMsg, pattern, string.Empty);
            return processedMsg;
        }

        public void ProcessLastChatLine(string chatLine)
        {
            if(chatLine.StartsWith(">------>") || chatLine.StartsWith("3======"))
            {
                return;
            }

            if(privateMode)
            {
                string[] lineDivided = System.Text.RegularExpressions.Regex.Split(chatLine, userData.Name + ":");
                if (lineDivided.Length != 2 || lineDivided[0].Contains(':')) return;
                lineDivided[0] = lineDivided[0].Trim();
                lineDivided[0] = lineDivided[0].Trim('(', ')');
                int targetClientNum;
                if (int.TryParse(lineDivided[0], out targetClientNum) && targetClientNum == userData.ClientNum)
                    chatLine = lineDivided[1];
                else return;
            }

            chatLine = chatLine.ToLower();

            if (ProcessMinorCommands(chatLine)) return;
            
            if (chatLine.Contains("!recover"))
            {
                GetUserData();
            }

            if(chatLine.Contains("!restrict"))
            {
                if (restrictMode)
                {
                    restrictMode = false;
                    ExecuteConsoleCommand("echo ^1Restrict mode off");
                }
                else
                {
                    restrictMode = true;
                    ExecuteConsoleCommand("echo ^1Restrict mode on");
                }
            }

            if (chatLine.Contains("!mode"))
            {
                if (privateMode)
                {
                    privateMode = false;
                    SendChatMessage("^0DU^1 - public mode on");
                }
                else
                {
                    privateMode = true;
                    SendChatMessage("^0DU^1 - public mode off");
                }
            }

            if (chatLine.Contains("!telemode"))
            {
                if (teleportMode)
                {
                    teleportMode = false;
                    SendChatMessage("^0DU^1 - teleport mode off");
                }
                else
                {
                    teleportMode = true;
                    SendChatMessage("^0DU^1 - teleport mode on");

                    if (privateMode)
                        ExecuteConsoleCommand("echo ^1Warning! Disable private mode to let teleport mode work!");
                }
            }

            if(chatLine.Contains("me"))
            {
                if(teleportMode)
                {
                    string[] lineDivided = chatLine.Split(':');
                    if (lineDivided.Length == 2 && lineDivided[1] == " me")
                    {
                        string clientIDRaw = lineDivided[0].Split(' ')[0];
                        string clientID = clientIDRaw.Trim('(', ')');
                        ExecuteConsoleCommand("gethere " + clientID);
                    }
                }
            }

            if (chatLine.Contains("!translate"))
            {
                string[] lineDivided = chatLine.Split(':');
                if (lineDivided.Length == 2 && lineDivided[1].StartsWith( " !translate "))
                {
                    string rawText = lineDivided[1].Replace(" !translate ", "");

                    SendChatMessage(EngToNoob(rawText));
                }
            }

            if (chatLine.Contains("!utility"))
            {
                string[] lineDivided = chatLine.Split(':');
                if (lineDivided.Length == 2 && lineDivided[1].StartsWith(" !utility "))
                {
                    string rawText = lineDivided[1].Replace(" !utility ", "");
                    try
                    {
                        string response = cleverBotSession.Think(rawText);
                        SendChatMessage("^0DU^1 - " + response);
                    }
                    catch (System.Net.WebException ex)
                    {
                        System.Diagnostics.Debug.Print("Error occured while getting bot response.");
                        SendChatMessage(ex.Message);
                    }                    
                }
            }

            if (chatLine.Contains("!scaleall"))
            {
                string[] prms = chatLine.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < 32; i++)
                {
                    if (privateMode)
                    {
                        ExecuteConsoleCommand("scale " + i + " " + prms[1]);
                    }
                    else
                        ExecuteConsoleCommand("scale " + i + " " + prms[3]);
                }
            }

            if (chatLine.Contains("!forward"))
            {
                ExecuteConsoleCommand("-back");
                ExecuteConsoleCommand("+forward");
            }

            if (chatLine.Contains("!back"))
            {
                ExecuteConsoleCommand("-forward");
                ExecuteConsoleCommand("+back");
            }

            if (chatLine.Contains("!left"))
            {
                ExecuteConsoleCommand("-right");
                ExecuteConsoleCommand("+left");
            }

            if (chatLine.Contains("!right"))
            {
                ExecuteConsoleCommand("-left");
                ExecuteConsoleCommand("+right");
            }

            if (chatLine.Contains("!jump"))
            {
                ExecuteConsoleCommand("+moveup");
                System.Threading.Thread.Sleep(50);
                ExecuteConsoleCommand("-moveup");
            }

            if (chatLine.Contains("!heal"))
            {
                ExecuteConsoleCommand("force_heal");
            }

            if (chatLine.Contains("!stop"))
            {
                ExecuteConsoleCommand("-forward");
                ExecuteConsoleCommand("-back");
                ExecuteConsoleCommand("-left");
                ExecuteConsoleCommand("-right");
                ExecuteConsoleCommand("-attack");
            }

            if (chatLine.Contains("!attack"))
            {
                ExecuteConsoleCommand("+attack");
            }

            if (chatLine.Contains("!tour"))
            {
                string[] words = chatLine.Split(' ');
                int cmdStartIndex = Array.FindIndex(words, x => x == "!tour");

                if(cmdStartIndex < words.Length - 1)
                {
                    if (words[cmdStartIndex + 1] == "restrict")
                        ExecuteConsoleCommand(@"place lmd_restrict 0 spawnflags,48,mins,-1000 -1000 0,maxs,1000 1000 1000");

                    else if(words[cmdStartIndex + 1] == "restrict_id")
                    {
                        if(cmdStartIndex < words.Length - 2)
                        {
                            int entityId;
                            if (int.TryParse(words[cmdStartIndex + 2], out entityId))
                            {
                                duelRestrict = entityId;
                                SendChatMessage("^0DU^3 - ID set");
                            }
                            else
                                SendChatMessage("^0DU^1 - Wrong parameter");
                        }
                        else
                            SendChatMessage("^0DU^1 - No parameter specified");
                    }

                    else if (words[cmdStartIndex + 1] == "announce")
                    {
                        ExecuteConsoleCommand("announce 10 ^1Saber tournament!^5 Say 'me' for teleport");
                    }

                    else if (words[cmdStartIndex + 1] == "help")
                    {
                        SendChatMessage("restrict|restrict_id|delrestrict|togglerestrict|announce|add|remove|clear|generate|view");
                    }

                    else if (words[cmdStartIndex + 1] == "delrestrict")
                    {
                        if(duelRestrict.HasValue)
                        {
                            ExecuteConsoleCommand("delent " + duelRestrict);
                            duelRestrict = null;
                        }
                        else
                            SendChatMessage("^0DU^1 - Restrict ID not provided, use restrict_id to do this");
                    }

                    else if(words[cmdStartIndex + 1] == "togglerestrict")
                    {
                        if (duelRestrict.HasValue)
                        {
                            ExecuteConsoleCommand("disable " + duelRestrict);
                        }
                        else
                            SendChatMessage("^0DU^1 - Restrict ID not provided, use restrict_id to do this");
                    }

                    else if (words[cmdStartIndex + 1] == "add")
                    {
                        if (cmdStartIndex < words.Length - 2)
                        {
                            tournamentParticipants.Add(words[cmdStartIndex + 2]);
                            SendChatMessage("^0DU^3 - " + words[cmdStartIndex + 2] + " added");
                        }
                        else
                            SendChatMessage("^0DU^1 - No name specified");
                    }

                    else if (words[cmdStartIndex + 1] == "remove")
                    {
                        if (cmdStartIndex < words.Length - 2)
                        {
                            if (tournamentParticipants.Remove(words[cmdStartIndex + 2]))
                                SendChatMessage("^0DU^3 - " + words[cmdStartIndex + 2] + " successfully removed");
                            else
                                SendChatMessage("^0DU^1 - Name not found");
                        }
                        else
                            SendChatMessage("^0DU^1 - No name specified");
                    }

                    else if(words[cmdStartIndex + 1] == "generate")
                    {
                        if (tournamentParticipants.Count % 2 == 0 && tournamentParticipants.Count != 0)
                        {
                            List<string> teams = new List<string>();
                            while(tournamentParticipants.Count > 0)
                            {
                                int index = RandomSingleton.Next(0, tournamentParticipants.Count);
                                teams.Add(tournamentParticipants[index]);
                                tournamentParticipants.RemoveAt(index);
                            }
                            tournamentParticipants = teams;
                        }
                        else
                            SendChatMessage("^0DU^1 - Wrong number of players - impaired or zero");
                    }
                    else if (words[cmdStartIndex + 1] == "view")
                    {
                        StateMsg = "";
                        if (tournamentParticipants.Count % 2 == 0 && tournamentParticipants.Count != 0)
                        {
                            for(int i = 0; i < tournamentParticipants.Count; i++)
                            {
                                if (i % 2 == 1)
                                    StateMsg += "vs ";
                                StateMsg += tournamentParticipants[i] + " ";
                                if (i % 2 == 1)
                                    StateMsg += " | ";
                            }
                            SendChatMessage(StateMsg);
                        }
                        else
                            SendChatMessage("^0DU^1 - Cannot list teams - unpaired or zero players");
                    }
                    else if (words[cmdStartIndex + 1] == "clear")
                    {
                        tournamentParticipants.Clear();
                    }

                    else
                        SendChatMessage("^0DU^1 - Unknown task");
                }
                else
                    SendChatMessage("^0DU^1 - No task specified");
            }
        }


        public void SendChatMessage(string msg)
        {
            if (restrictMode)
                System.Threading.Thread.Sleep(1000);

            ExecuteConsoleCommand("say " + msg);
        }

        public void ExecuteConsoleCommand(string cmd)
        {
            WinAPIHelper.SendMessage(consoleWriteHandle, WinAPIHelper.WM_SETTEXT, 0, cmd);
            WinAPIHelper.SendMessage(consoleWriteHandle, 258, 13, 0); //pseudo "enter" key
        }
    }
}
