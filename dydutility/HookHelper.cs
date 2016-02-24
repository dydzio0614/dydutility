using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dydutility
{
    static class HookHelper
    {
        public const int PlayerClientNumAddress = 0x9140c8;

        public static bool HookGame(ref IntPtr writeHandle, ref IntPtr readHandle, ref IntPtr processHandle)
        {

            IntPtr GameConsoleWindow = WinAPIHelper.FindWindow("JAMP WinConsole", null);
            if (GameConsoleWindow == IntPtr.Zero)
                return false;


            IntPtr processId = new IntPtr();
            WinAPIHelper.GetWindowThreadProcessId(GameConsoleWindow, ref processId);
            if (processId == IntPtr.Zero)
                return false;

            processHandle = WinAPIHelper.OpenProcess(0x1F0FFF, false, (int)processId);
            if (processHandle == IntPtr.Zero)
                return false;

            writeHandle = WinAPIHelper.FindWindowEx(GameConsoleWindow, IntPtr.Zero, "edit", null);
            readHandle = WinAPIHelper.FindWindowEx(GameConsoleWindow, writeHandle, "edit", null);
            return true;
        }
    }
}
