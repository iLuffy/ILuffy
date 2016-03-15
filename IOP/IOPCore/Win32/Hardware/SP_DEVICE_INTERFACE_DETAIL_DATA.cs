namespace ILuffy.IOP.Win32.Hardware
{
    using System.Runtime.InteropServices;

    /// <summary>
    /// http://www.pinvoke.net/default.aspx/Structures/SP_DEVICE_INTERFACE_DETAIL_DATA.html
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 1)]
    public struct SP_DEVICE_INTERFACE_DETAIL_DATA  // Only used for Marshal.SizeOf.
    {
        public uint cbSize;
        public char DevicePath;
    }
}
