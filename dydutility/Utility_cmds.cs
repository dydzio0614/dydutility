using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace dydutility
{
    public partial class Utility
    {
        public bool ProcessMinorCommands(string chatLine)
        {
            bool cmdFound = true;

            if (customCommands != null)
            {
                foreach (BonusCommand command in customCommands)
                {
                    if (chatLine.Contains("!" + command.Name.ToLower()))
                    {
                        SendChatMessage(command.Response);
                        break;
                    }
                }
            }

            if (chatLine.Contains("!about") || chatLine.Contains(userData.Name + " entered the game"))
            {
                SendChatMessage("^0Dyd^1zio's ^6Utility^1 - v. 0.0.9a");
            }
            else if (chatLine.Contains("!paper"))
            {
                SendChatMessage("^7Paper ^3aka Alex Deane - one of worst shit on EU Lugormod. 0/10. Recently became more tolerable");
            }
            else if (chatLine.Contains("!rico"))
            {
                SendChatMessage("^2R^0ico^3 - KFC chef, overall OK guy. Ancient player with long lugormod past");
            }
            else if (chatLine.Contains("!mrk"))
            {
                SendChatMessage("^7Mr^0.^4K^3 - cool and very clever guy. Good friend. Maintainer of webchat and translator");
            }
            else if (chatLine.Contains("!razot"))
            {
                SendChatMessage("^7Does this guy even play anymore? He keeps returning for a while to go back to cs:go 1 week later");
            }
            else if (chatLine.Contains("!arsa"))
            {
                SendChatMessage("^1arsa^5 - strong skilltard saberist. Mean guy with stereotypal lugormod behavior");
            }
            else if (chatLine.Contains("!pain"))
            {
                SendChatMessage("^0païn^1 - decent staffer. Nothing more to say about him");
            }
            else if (chatLine.Contains("!exodus"))
            {
                SendChatMessage("^1Exodus^7 - 24/7 jkadst_hudx user. Fan of islamic nasheeds made by Lana Lmorhafato");
            }
            else if (chatLine.Contains("!dog"))
            {
                SendChatMessage("^7dog^3 - decent merc, plays efficiently and makes rampage while online.");
            }
            else if (chatLine.Contains("!creature"))
            {
                SendChatMessage("^0creature^1 - really strong player, but nothing more. Recently less active");
            }
            else if (chatLine.Contains("!alcany"))
            {
                SendChatMessage("^5A^7lcany^5 - moderate saberist with annoying jump attacks. Naive Trump supporter");
            }
            else if (chatLine.Contains("!donkey"))
            {
                SendChatMessage("^2D^0onkey^5 - i-oo i-oo ia ia i-aa.");
            }
            else if (chatLine.Contains("!kalthani"))
            {
                SendChatMessage("^1K^0althani^6 - expert at joining one clan/faction multiple times. Low tier but fair player");
            }
            else if(chatLine.Contains("!peteri"))
            {
                SendChatMessage("^4Peteri^1 - versatile player, kinda smart for his age, his account username means 'hrabin is dick'");
            }
            else if (chatLine.Contains("!pete"))
            {
                SendChatMessage("^7pete^4`^3 - ridiculous player, funniest guy on server. Epic troll and very good merc");
            }
            else if (chatLine.Contains("!sonic"))
            {
                SendChatMessage("^4S^6o^4n^6i^4c^6_^4x^6x^2 - ridiculous player, unfortunately in bad meaning of this word");
            }
            else if (chatLine.Contains("!bob"))
            {
                SendChatMessage("^7bob^5 - good and smart player, server king that rarely wastes time on citizens");
            }
            else if (chatLine.Contains("!ufo"))
            {
                SendChatMessage("^6Ufo^5 - strong and wise player, nice friend, deadly when angry");
            }
            else if (chatLine.Contains("!aurora"))
            {
                SendChatMessage("^7Aurora - moderate strength, friendly player, tends to lame occasionally");
            }
            else if (chatLine.Contains("!lumaya"))
            {
                SendChatMessage("^5L^7umaya^6 - good saberist, argues too much, he was less annoying in the past");
            }
            else if (chatLine.Contains("!serbip1"))
            {
                SendChatMessage("^7Serbip1^1 - bad saberist, better at force, pretty smart young guy, comes from Gypsylandia");
            }
            else if (chatLine.Contains("!swagrez"))
            {
                SendChatMessage("^7swagrez^4 - occasional jerk admin, complains about maps and many other stuff, bigmouth");
            }
            else if (chatLine.Contains("!xenonk"))
            {
                SendChatMessage("^4X^0enonk^3 - ex-leader of apprentices, moderate saberist, friendly and intuitive. Lives in safe country");
            }
            else if (chatLine.Contains("!elite"))
            {
                SendChatMessage("^7elite^2 - competent player, who avoids some chat wars. Definitely more tolerable than Arsa, Kitty etc");
            }
            else if (chatLine.Contains("!michael"))
            {
                SendChatMessage("^5Michael^7 - low tier player, annoying as he cannot behave serious even for a short while");
            }
            else if (chatLine.Contains("!paweli"))
            {
                SendChatMessage("check !rageh for description");
            }
            else if (chatLine.Contains("!rageh"))
            {
                SendChatMessage("^4R^7ageh^1 - (LEGEND) utter crap of polish JKA scene, failing 'jka and lugormod expert'");
            }
            else if (chatLine.Contains("!ursa"))
            {
                SendChatMessage("^1Ursa^6 - strong, fair and clever, not a player to complain about. Rarely active");
            }
            else if (chatLine.Contains("!icecream"))
            {
                SendChatMessage("^3I^5c^6e^3C^5r^6e^3a^5m^4 - fairly good fighter, pretty peaceful");
            }
            else if (chatLine.Contains("!apprentices"))
            {
                SendChatMessage("^7Apprentices (Assassins of Al'haair) - faction of low tier butthurts who leave and rejoin faction regularly");
            }
            else if (chatLine.Contains("!dydzio"))
            {
                SendChatMessage("^0Dyd^1zio^3 - he is my master, he created me in C# with WPF framework using WinAPI dllimport. I am happy utility now!");
            }
            else if (chatLine.Contains("!kitty"))
            {
                SendChatMessage("^7k!tty^4 - this player is same as arsa imo, both in skills and behavior. Hunting new players makes him happy");
            }
            else if (chatLine.Contains("!cyanide"))
            {
                SendChatMessage("^6CYANIDE^3 - total horse shit, iq and mentality worse than lugormod stereotypal");
            }
            else if (chatLine.Contains("!animal"))
            {
                SendChatMessage("^0Anim^1@^0l^3 - recently inactive geek player, intermediate saberist, runs a faction");
            }
            else if (chatLine.Contains("!nakiner"))
            {
                SendChatMessage("^6n^7akiner - helpful player, but suffering from RUtard syndrome. Decompiled my utility and made 'putin utility' copy");
            }
            else if (chatLine.Contains("!jamie"))
            {
                SendChatMessage("^2J^0ä^2m^0ï^2e^0.^20^6 - (LEGEND) failtard, bragging about nonexistant skills and 'making maps' by changing author names");
            }
            else if (chatLine.Contains("!gog"))
            {
                SendChatMessage("^1gog^2 - Very good player with typical annoying polish attutude");
            }
            else if (chatLine.Contains("!kasme"))
            {
                SendChatMessage("^7kasme^5 - Retarded as hell, spammer kid and wannabe admin with 'intelectually_disabled' privileges.");
            }
            else if (chatLine.Contains("!raptor"))
            {
                SendChatMessage("^7RAPTOR^4 - known also as Source - average player + good strafer with super high self esteem.");
            }
            else if (chatLine.Contains("!ryder"))
            {
                SendChatMessage("^0RYDER^1 - Banned excessive lamer. Spawnrapist, griphunter, late night ninja newbiekiller.");
            }
            else if (chatLine.Contains("!snow"))
            {
                SendChatMessage("^2§^0now^6 - best dual saber fighter, behaves as if pretending to be very mature");
            }
            else if (chatLine.Contains("!boc"))
            {
                SendChatMessage("^7boc - good merc and lamer spamming 'sit dog', at first I thought bob and boc is same player");
            }
            else if (chatLine.Contains("!god potato"))
            {
                SendChatMessage("^7god potato^3 - God of the potatoes");
            }
            else if (chatLine.Contains("!motorku"))
            {
                SendChatMessage("^7motorku^5 - rrrr rrr wrrrr");
            }
            else if (chatLine.Contains("!gary"))
            {
                SendChatMessage("gary - that word means 'pots' in polish language");
            }
            else if (chatLine.Contains("!stojka"))
            {
                SendChatMessage("100jka - 'sto' means 100 in polish language. I cant tell much more about that guy");
            }
            else if (chatLine.Contains("!bushido"))
            {
                SendChatMessage("^7bushido - Japanese term for the samurai way of life, loosely analogous to the concept of chivalry in Europe");
            }
            else if (chatLine.Contains("!antoni macierewicz"))
            {
                SendChatMessage("en.wikipedia.org/wiki/Antoni_Macierewicz");
            }
            else if (chatLine.Contains("!joke"))
            {
                int index = RandomSingleton.Next(0,37);
                string joke;
                switch(index)
                {
                    case 0:
                        joke = "Just changed my Facebook name to ‘No one' so when I see stupid posts I can click like and it will say ‘No one likes this'.";
                        break;
                    case 1:
                        joke = "What's the difference between snowmen and snowladies? Snowballs";
                        break;
                    case 2:
                        joke = "How do you make holy water? You boil the hell out of it.";
                        break;
                    case 3:
                        joke = "Why did the blonde get excited after finishing her puzzle in 6 months? -- The box said 2-4 years!";
                        break;
                    case 4:
                        joke = "I once farted in an elevator, it was wrong on so many levels.";
                        break;
                    case 5:
                        joke = "If 4 out of 5 people SUFFER from diarrhea - does that mean that one enjoys it?";
                        break;
                    case 6:
                        joke = "I used to like my neighbors, until they put a password on their Wi-Fi.";
                        break;
                    case 7:
                        joke = "For anyone who think a woman's place is in the kitchen, remember that's where the knives are kept.";
                        break;
                    case 8:
                        joke = "What's the difference between a smart man and a stupid man? Nothing. They both think they know everything.";
                        break;
                    case 9:
                        joke = "Stalking is when two people go for a long romantic walk together but only one of them knows about it.";
                        break;
                    case 10:
                        joke = "If practice makes perfect, and nobody's perfect, why practice?";
                        break;
                    case 11:
                        joke = "Light travels faster than sound. This is why some people appear bright until they speak.";
                        break;
                    case 12:
                        joke = "Why do farts smell? So deaf people can enjoy them too.";
                        break;
                    case 13:
                        joke = "I asked God for a bike, but I know God doesn't work that way. So I stole a bike and asked for forgiveness.";
                        break;
                    case 14:
                        joke = "How did the blonde die while raking leaves? She fell out of the tree.";
                        break;
                    case 15:
                        joke = "Politicians and diapers have one thing in common. They should both be changed regularly, and for the same reason.";
                        break;
                    case 16:
                        joke = "Why is Christmas just like a day at the office? You do all the work and the fat guy with the suit gets all the credit.";
                        break;
                    case 17:
                        joke = "How do you seduce a fat woman? Piece of cake.";
                        break;
                    case 18:
                        joke = "What do you call two fat people having a chat? -- A heavy discussion";
                        break;
                    case 19:
                        joke = "If at first you don't succeed, destroy all evidence that you tried.";
                        break;
                    case 20:
                        joke = "Why did the skeleton go to the party alone? -- He had no body to go with him!";
                        break;
                    case 21:
                        joke = "What do you call a sheep with no legs? A cloud.";
                        break;
                    case 22:
                        joke = "What do you do if a idiot throws a grenade at you? -- Pull the pin and throw it back at him!";
                        break;
                    case 23:
                        joke = "When a man opens the car door for his wife, you can be sure of one thing, either the car is new or the wife is.";
                        break;
                    case 24:
                        joke = "Always remember you're unique, just like everyone else.";
                        break;
                    case 25:
                        joke = "What do you call a cow with no legs? -- Ground beef.";
                        break;
                    case 26:
                        joke = "What's 6 inches long, has a head on it and drives women crazy? $100 bill";
                        break;
                    case 27:
                        joke = "Why don't cannibals eat clowns? -- Because they taste funny.";
                        break;
                    case 28:
                        joke = "How did the blonde die drinking milk? The cow fell on her.";
                        break;
                    case 29:
                        joke = "Do not underestimate your abilities. That is your boss's job.";
                        break;
                    case 30:
                        joke = "Why are horses always so fit? Because they're on a stable diet.";
                        break;
                    case 31:
                        joke = "Want to look thinner? Hang out with fat people.";
                        break;
                    case 32:
                        joke = "A well-educated friend of mine with three advanced degrees can say “I’m unemployed” in six languages.";
                        break;
                    case 33:
                        joke = "How can you kill a stupid person with a coin? Throw it in front of an oncoming bus.";
                        break;
                    case 34:
                        joke = "Nothing ruins a Friday more than realizing that today is Tuesday.";
                        break;
                    case 35:
                        joke = "My job is secure. No one else wants it.";
                        break;
                    case 36:
                        joke = "I wanted to grow my own food but I couldn’t get bacon seeds anywhere.";
                        break;
                    default:
                        joke = "Just changed my Facebook name to ‘No one' so when I see stupid posts I can click like and it will say ‘No one likes this'.";
                        break;
                }
                SendChatMessage(joke);
            }
            else if (chatLine.Contains("!register") || chatLine.Contains("how register") || chatLine.Contains("how to register"))
            {
                SendChatMessage("^31. Change name from 'Padawan', if name does not work try another.");
                SendChatMessage("^32. Open console with SHIFT + ~ and type /register (name) (password)");
            }
            else if (chatLine.Contains("!credits"))
            {
                SendChatMessage("^3Credits on this server serve 2 main purposes: 1 - you can buy things on maps");
                SendChatMessage("^32 - you can buy special skills, try /sskills command in console");
            }
            else if (chatLine.Contains("!lugormod"))
            {
                SendChatMessage("^3This servers runs on mod called Lugormod.");
                SendChatMessage("^3It allows players to interact with prepared map objects in addition to normal playing.");
                SendChatMessage("^3Map objects can be built by admins and players can use them as new way of playing game");
            }
            else if (chatLine.Contains("hi dyd") || chatLine.Contains("hey dyd") || chatLine.Contains("hello dyd"))
            {
                SendChatMessage("hi");
            }
            else if (chatLine.Contains("bye dyd") || chatLine.Contains("bb dyd") || chatLine.Contains("cya dyd"))
            {
                SendChatMessage("bye");
            }
            else if (chatLine.Contains("what is this"))
            {
                SendChatMessage("This is Jedi Academy server with lugormod");
            }
            else
                cmdFound = false;

            return cmdFound;
        }


        private string EngToNoob(string text)
        {
            if (text == "")
                return "";

            string modifiedInput = Regex.Replace(text, "(too)|(to)", "2", RegexOptions.IgnoreCase);
            modifiedInput = Regex.Replace(modifiedInput, "(fore)|(for)", "4", RegexOptions.IgnoreCase);
            modifiedInput = Regex.Replace(modifiedInput, "(oo)", "00", RegexOptions.IgnoreCase);
            modifiedInput = Regex.Replace(modifiedInput, "(be)", "b", RegexOptions.IgnoreCase);
            modifiedInput = Regex.Replace(modifiedInput, "(are)", "r", RegexOptions.IgnoreCase);
            modifiedInput = Regex.Replace(modifiedInput, "(you)", "u", RegexOptions.IgnoreCase);
            modifiedInput = Regex.Replace(modifiedInput, "(please)", "plz", RegexOptions.IgnoreCase);
            modifiedInput = Regex.Replace(modifiedInput, "(people)", "ppl", RegexOptions.IgnoreCase);
            modifiedInput = Regex.Replace(modifiedInput, "(really)", "rly", RegexOptions.IgnoreCase);
            modifiedInput = Regex.Replace(modifiedInput, "(have)", "haz", RegexOptions.IgnoreCase);
            modifiedInput = Regex.Replace(modifiedInput, "(know)", "no", RegexOptions.IgnoreCase);
            modifiedInput = Regex.Replace(modifiedInput, "s", "z", RegexOptions.IgnoreCase);

            bool allCaps = false;
            bool lolAdded = false;

            if (char.ToLower(modifiedInput[0]) == 'h')
            {
                allCaps = true;
                modifiedInput = modifiedInput.ToUpper();
            }

            if (char.ToLower(modifiedInput[0]) == 'w')
            {
                lolAdded = true;
                modifiedInput = modifiedInput.Insert(0, "LOL ");
            }

            while (true)
            {
                int index = modifiedInput.IndexOfAny(new char[] { '.', ',', '\'' });
                if (index != -1)
                    modifiedInput = modifiedInput.Remove(index, 1);
                else
                    break;
            }

            string strippedInput = modifiedInput;

            while (true)
            {
                int index = strippedInput.IndexOfAny(new char[] { '!', '?' });
                if (index != -1)
                    strippedInput = strippedInput.Remove(index, 1);
                else
                    break;
            }

            if (strippedInput.Length >= 32)
                modifiedInput = modifiedInput.Insert(lolAdded ? 4 : 0, "OMG ");

            string[] words = modifiedInput.Split(' ');

            string questionMarkTemplate = "";
            string exclamationMarkTemplate = "";

            for (int i = 0; i < words.Length; i++)
            {
                questionMarkTemplate += "?";
                exclamationMarkTemplate += (i % 2 == 0) ? "!" : "1";
            }

            for (int i = 0; i < words.Length; i++)
            {
                words[i] = words[i].Replace("!", exclamationMarkTemplate);
                words[i] = words[i].Replace("?", questionMarkTemplate);
            }

            if (!allCaps)
                for (int i = 0; i < words.Length; i++)
                {
                    if (i % 2 == 1)
                        words[i] = words[i].ToUpper();
                }

            modifiedInput = string.Join(" ", words);
            return modifiedInput;
        }
    }
}
