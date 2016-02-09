using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dydutility
{
    class Utility
    {
        private IntPtr consoleWriteHandle;
        private IntPtr consoleReadHandle;
        private bool gameHooked;

        public bool GameHooked { get { return gameHooked; } }

        public bool FindJKAConsole()
        {
            if (gameHooked)
                return true;

            bool hookSucceeded = HookHelper.HookGame(ref consoleWriteHandle, ref consoleReadHandle);

            if (hookSucceeded)
            {
                gameHooked = true;
                return true;
            }
            else
                return false;
        }

        public string ReadConsole()
        {
            StringBuilder temp = new StringBuilder(50000);
            int consoleLength = WinAPIHelper.SendMessage(consoleReadHandle, WinAPIHelper.WM_GETTEXT, 50000, temp);
            
            return temp.ToString();
        }

        public void SendChatMessage(string msg)
        {
            WinAPIHelper.SendMessage(consoleWriteHandle, WinAPIHelper.WM_SETTEXT, 0, "say ^0(^1DU^0) ^2" + msg);
            WinAPIHelper.SendMessage(consoleWriteHandle, 258, 13, 0);
        }
    }
}
