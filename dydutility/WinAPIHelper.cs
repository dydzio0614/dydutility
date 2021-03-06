﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace dydutility
{
    static class WinAPIHelper
    {

        //constants

        //for SendMessage
        public const int WM_SETTEXT = 0xC;
        public const int WM_GETTEXT = 0xD;
        public const int WM_GETTEXTLENGTH = 0xE;


        //WinAPI functions
        [DllImport("user32", EntryPoint = "SendMessageA")]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        [DllImport("user32", EntryPoint = "SendMessageA")]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, StringBuilder lParam);
        [DllImport("user32", EntryPoint = "SendMessageA")]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, string lParam);
        [DllImport("kernel32")]
        public static extern IntPtr OpenProcess(int accessBitmask, bool inheritHandle, int procId);
        [DllImport("user32")]
        public static extern int GetWindowThreadProcessId(IntPtr hwnd, ref IntPtr outputProcId);
        [DllImport("kernel32")]
        public static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr addressToRead, ref IntPtr resultBuffer, int size, IntPtr numberOfBytesRead);
        [DllImport("kernel32")]
        public static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr addressToRead, StringBuilder resultBuffer, int size, IntPtr numberOfBytesRead);
        [DllImport("user32", EntryPoint = "FindWindowA")]
        public static extern IntPtr FindWindow(string className, string windowName);
        [DllImport("user32", EntryPoint = "FindWindowExA")]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string className, string windowName);
        [DllImport("user32")]
        public static extern int CloseHandle(IntPtr hObject);
        [DllImport("kernel32")]
        public static extern int GetLastError();
    }
}
