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
    }
}
