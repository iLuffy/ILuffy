namespace ILuffy.IOP.Win32.Core
{
    using System;
    using System.IO;
    using System.Runtime.InteropServices;
    public static class KernelAPI
    {
        [DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr CreateFile(
            string lpFileName, 
            uint dwDesiredAccess,
            FileShare dwShareMode, 
            IntPtr lpSecurityAttributes, 
            FileMode dwCreationDisposition, 
            int dwFlagsAndAttributes, 
            IntPtr hTemplateFile);
    }
}
