namespace ILuffy.IOP.Win32.Hardware
{
    using System;
    using System.Runtime.InteropServices;
    public static class SetupAPI
    {
        public static readonly Guid GUID_DEVINTERFACE_USBPRINT = new Guid(0x28d78fad, 0x5a12, 0x11D1, 0xae, 0x5b, 0x00, 0x00, 0xf8, 0x03, 0xa8, 0xc2);

        //
        //https://msdn.microsoft.com/en-us/library/windows/hardware/ff551069(v=vs.85).aspx
        //
        [DllImport("setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr SetupDiGetClassDevs(
            [In(), MarshalAs(UnmanagedType.LPStruct)] Guid classGuid, 
            string enumerator, 
            IntPtr hwndParent, 
            uint flags);

        [DllImport("setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int SetupDiEnumDeviceInfo(
            IntPtr deviceInfoSet, 
            uint memberIndex, 
            ref SP_DEVINFO_DATA deviceInfoData);

        [DllImport("setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int SetupDiEnumDeviceInterfaces(
            IntPtr deviceInfoSet, 
            [In()] ref SP_DEVINFO_DATA deviceInfoData, 
            [In(), MarshalAs(UnmanagedType.LPStruct)] Guid interfaceClassGuid, 
            uint memberIndex, 
            ref SP_DEVICE_INTERFACE_DATA deviceInterfaceData);

        [DllImport("setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int SetupDiGetDeviceInterfaceDetail(
            IntPtr deviceInfoSet, 
            [In()] ref SP_DEVICE_INTERFACE_DATA deviceInterfaceData, 
            IntPtr deviceInterfaceDetailData, 
            uint deviceInterfaceDetailDataSize, 
            out uint requiredSize, 
            IntPtr deviceInfoData);

        [DllImport("setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int SetupDiDestroyDeviceInfoList(IntPtr deviceInfoSet);

        /// <summary>
        /// The SetupDiGetDeviceRegistryProperty function retrieves the specified device property.
        /// This handle is typically returned by the SetupDiGetClassDevs or SetupDiGetClassDevsEx function.
        /// </summary>
        /// <param Name="DeviceInfoSet">Handle to the device information set that contains the interface and its underlying device.</param>
        /// <param Name="DeviceInfoData">Pointer to an SP_DEVINFO_DATA structure that defines the device instance.</param>
        /// <param Name="Property">Device property to be retrieved. SEE MSDN</param>
        /// <param Name="PropertyRegDataType">Pointer to a variable that receives the registry data Type. This parameter can be NULL.</param>
        /// <param Name="PropertyBuffer">Pointer to a buffer that receives the requested device property.</param>
        /// <param Name="PropertyBufferSize">Size of the buffer, in bytes.</param>
        /// <param Name="RequiredSize">Pointer to a variable that receives the required buffer size, in bytes. This parameter can be NULL.</param>
        /// <returns>If the function succeeds, the return value is nonzero.</returns>
        [DllImport("setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetupDiGetDeviceRegistryProperty(
            IntPtr deviceInfoSet,
            ref SP_DEVINFO_DATA deviceInfoData, //ref
            UInt32 property,
            ref UInt32 propertyRegDataType,
            IntPtr propertyBuffer,
            UInt32 propertyBufferSize,
            ref UInt32 requiredSize
        );
    }
}
