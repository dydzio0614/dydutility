using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dydutility
{
    static class HookHelper
    {
        public static bool HookGame(ref IntPtr writeHandle, ref IntPtr readHandle)
        {
            IntPtr GameConsoleWindow = WinAPIHelper.FindWindow("JAMP WinConsole", null);
            if (GameConsoleWindow == IntPtr.Zero)
                return false;

            //add port open process stuff if needed

            writeHandle = WinAPIHelper.FindWindowEx(GameConsoleWindow, IntPtr.Zero, "edit", null);
            readHandle = WinAPIHelper.FindWindowEx(GameConsoleWindow, writeHandle, "edit", null);
            return true;
        }
    }
}
