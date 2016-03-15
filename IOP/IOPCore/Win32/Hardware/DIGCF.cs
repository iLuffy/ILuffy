namespace ILuffy.IOP.Win32.Hardware
{
    using System;

    [Flags]
    public enum DIGCF : uint
    {
        DIGCF_PRESENT = 0x00000002U,
        DIGCF_ALLCLASSES = 0x00000004U,
        DIGCF_DEVICEINTERFACE = 0x00000010U,
    }
}
