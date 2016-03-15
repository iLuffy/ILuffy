namespace ILuffy.IOP.Win32.Core
{
    using System;

    [Flags]
    public enum GenericAccessRight : uint
    {
        GENERIC_WRITE = 0x40000000,
        GENERIC_READ = 0x80000000,
    }
}
