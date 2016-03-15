namespace ILuffy.IOP.Hardware
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.InteropServices;
    using I18N;
    using Win32;
    using Win32.Hardware;

    public static class DeviceUtil
    {
        private static readonly IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);

        public static List<DeviceProperties> GetAllDeviceProperties(string enumerator)
        {
            var devices = new List<DeviceProperties>();

            IntPtr hdi = SetupAPI.SetupDiGetClassDevs(
                Guid.Empty, enumerator,
                IntPtr.Zero, 
                (uint)(DIGCF.DIGCF_PRESENT | DIGCF.DIGCF_ALLCLASSES));

            if (hdi.Equals(INVALID_HANDLE_VALUE))
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }

            var bufferSize = 1024;

            IntPtr intPtrBuffer = IntPtr.Zero;
            try
            {
                intPtrBuffer = Marshal.AllocHGlobal(bufferSize);

                SP_DEVINFO_DATA deviceInfoData = new SP_DEVINFO_DATA();
                deviceInfoData.cbSize = (uint)Marshal.SizeOf(typeof(SP_DEVINFO_DATA));

                uint memberIndex = 0;

                while (true)
                {
                    if (SetupAPI.SetupDiEnumDeviceInfo(
                        hdi, memberIndex, ref deviceInfoData) == 0)
                    {
                        var win32Error = Marshal.GetLastWin32Error();
                        break;
                    }

                    memberIndex++;

                    UInt32 regType = 0;
                    UInt32 requiredSize = 0;
                    SetupAPI.SetupDiGetDeviceRegistryProperty(
                        hdi, 
                        ref deviceInfoData, 
                        (UInt32)SPDRP.SPDRP_HARDWAREID, 
                        ref regType, 
                        IntPtr.Zero, 
                        0, 
                        ref requiredSize);

                    var lastError = Marshal.GetLastWin32Error();
                    if (lastError == Win32ErrorCode.ERROR_INSUFFICIENT_BUFFER)
                    {
                        if (requiredSize > bufferSize)
                        {
                            bufferSize = (int)requiredSize;
                            Marshal.FreeHGlobal(intPtrBuffer);
                            intPtrBuffer = Marshal.AllocHGlobal(bufferSize);
                        }

                        if (SetupAPI.SetupDiGetDeviceRegistryProperty(hdi, ref deviceInfoData, (UInt32)SPDRP.SPDRP_HARDWAREID, ref regType, intPtrBuffer, (uint)bufferSize, ref requiredSize))
                        {
                            var device = new DeviceProperties();
                            string hardwareID = Marshal.PtrToStringAuto(intPtrBuffer);
                            device.HardwareId = hardwareID;

                            if (SetupAPI.SetupDiGetDeviceRegistryProperty(hdi, ref deviceInfoData, (UInt32)SPDRP.SPDRP_FRIENDLYNAME, ref regType, intPtrBuffer, (uint)bufferSize, ref requiredSize))
                            {
                                string friendlyName = Marshal.PtrToStringAuto(intPtrBuffer);
                                device.FriendlyName = friendlyName;
                            }
                            if (SetupAPI.SetupDiGetDeviceRegistryProperty(hdi, ref deviceInfoData, (UInt32)SPDRP.SPDRP_DEVTYPE, ref regType, intPtrBuffer, (uint)bufferSize, ref requiredSize))
                            {
                                string deviceType = Marshal.PtrToStringAuto(intPtrBuffer);
                                device.DeviceType = deviceType;
                            }

                            if (SetupAPI.SetupDiGetDeviceRegistryProperty(hdi, ref deviceInfoData, (UInt32)SPDRP.SPDRP_CLASS, ref regType, intPtrBuffer, (uint)bufferSize, ref requiredSize))
                            {
                                string deviceClass = Marshal.PtrToStringAuto(intPtrBuffer);
                                device.DeviceClass = deviceClass;
                            }
                            if (SetupAPI.SetupDiGetDeviceRegistryProperty(hdi, ref deviceInfoData, (UInt32)SPDRP.SPDRP_MFG, ref regType, intPtrBuffer, (uint)bufferSize, ref requiredSize))
                            {
                                string deviceManufacturer = Marshal.PtrToStringAuto(intPtrBuffer);
                                device.DeviceManufacturer = deviceManufacturer;
                            }
                            if (SetupAPI.SetupDiGetDeviceRegistryProperty(hdi, ref deviceInfoData, (UInt32)SPDRP.SPDRP_LOCATION_INFORMATION, ref regType, intPtrBuffer, (uint)bufferSize, ref requiredSize))
                            {
                                string deviceLocation = Marshal.PtrToStringAuto(intPtrBuffer);
                                device.DeviceLocation = deviceLocation;
                            }
                            if (SetupAPI.SetupDiGetDeviceRegistryProperty(hdi, ref deviceInfoData, (UInt32)SPDRP.SPDRP_LOCATION_PATHS, ref regType, intPtrBuffer, (uint)bufferSize, ref requiredSize))
                            {
                                string devicePath = Marshal.PtrToStringAuto(intPtrBuffer);
                                device.DevicePath = devicePath;
                            }
                            if (SetupAPI.SetupDiGetDeviceRegistryProperty(hdi, ref deviceInfoData, (UInt32)SPDRP.SPDRP_PHYSICAL_DEVICE_OBJECT_NAME, ref regType, intPtrBuffer, (uint)bufferSize, ref requiredSize))
                            {
                                string devicePhysicalObjectName = Marshal.PtrToStringAuto(intPtrBuffer);
                                device.DevicePhysicalObjectName = devicePhysicalObjectName;
                            }

                            if (SetupAPI.SetupDiGetDeviceRegistryProperty(hdi, ref deviceInfoData, (UInt32)SPDRP.SPDRP_DEVICEDESC, ref regType, intPtrBuffer, (uint)bufferSize, ref requiredSize))
                            {
                                string deviceDescription = Marshal.PtrToStringAuto(intPtrBuffer);
                                device.DeviceDescription = deviceDescription;
                            }

                            uint device_id_size = 0;
                            var cmret = PnPCMAPI.CM_Get_Device_ID_Size(out device_id_size, deviceInfoData.DevInst, 0);
                            if (cmret != 0)
                            {
                                throw new Exception(string.Format("Failed to get size of the device ID of the device '{0}'. Error code: 0x{1:X8}", hardwareID, cmret));
                            }

                            //device_id_size++;  // To include the null character

                            string device_id = new string('\0', (int)device_id_size);
                            cmret = PnPCMAPI.CM_Get_Device_ID(deviceInfoData.DevInst, device_id, device_id_size, 0);
                            if (cmret != 0)
                            {
                                throw new Exception(string.Format("Failed to get device ID of the device '{0}'. Error code: 0x{1:X8}", hardwareID, cmret));
                            }

                            device.DeviceId = device_id;
                            var deviceInterfacePath = GetAllDevicePath(Guid.Empty, device_id);
                            if (deviceInterfacePath != null && deviceInterfacePath.Count > 0)
                            {
                                device.DeviceInterfacePath = deviceInterfacePath[0];
                            }

                            devices.Add(device);
                        }
                    }
                }
            }
            finally
            {
                if (intPtrBuffer != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(intPtrBuffer);
                }
                SetupAPI.SetupDiDestroyDeviceInfoList(hdi);
            }

            return devices;
        }

        public static List<string> GetAllDevicePath(Guid classGuid, string enumerator)
        {
            var devicePaths = new List<string>();

            IntPtr hdi = SetupAPI.SetupDiGetClassDevs(
                classGuid,
                enumerator, 
                IntPtr.Zero, 
                (uint)(DIGCF.DIGCF_PRESENT | DIGCF.DIGCF_DEVICEINTERFACE));

            if (hdi.Equals(INVALID_HANDLE_VALUE))
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }

            var bufferSize = 1024;

            IntPtr intPtrBuffer = IntPtr.Zero;
            try
            {
                intPtrBuffer = Marshal.AllocHGlobal(bufferSize);

                SP_DEVINFO_DATA deviceInfo = new SP_DEVINFO_DATA();
                deviceInfo.cbSize = (uint)Marshal.SizeOf(typeof(SP_DEVINFO_DATA));

                uint memberIndex = 0;

                while (true)
                {
                    if (SetupAPI.SetupDiEnumDeviceInfo(hdi, memberIndex, ref deviceInfo) == 0)
                    {
                        var errorCode = Marshal.GetLastWin32Error();

                        if (errorCode != 0)
                        {
                            throw new Win32Exception(errorCode);
                        }

                        break;
                    }

                    memberIndex++;

                    SP_DEVICE_INTERFACE_DATA interfaceData = new SP_DEVICE_INTERFACE_DATA();
                    interfaceData.cbSize = (uint)Marshal.SizeOf(typeof(SP_DEVICE_INTERFACE_DATA));

                    uint interfaceMemberIndex = 0;

                    while (true)
                    {
                        if (SetupAPI.SetupDiEnumDeviceInterfaces(
                            hdi, 
                            ref deviceInfo, 
                            classGuid, 
                            interfaceMemberIndex, 
                            ref interfaceData) == 0)
                        {
                            var errorCode = Marshal.GetLastWin32Error();

                            if (errorCode != 0)
                            {
                                throw new Win32Exception(errorCode);
                            }
                            break;
                        }
                        interfaceMemberIndex++;

                        devicePaths.Add(GetDevicePath(hdi, interfaceData));
                    }
                }
            }
            finally
            {
                if (intPtrBuffer != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(intPtrBuffer);
                }
                SetupAPI.SetupDiDestroyDeviceInfoList(hdi);
            }

            return devicePaths;
        }

        public static string GetDevicePath(IntPtr deviceInfoSet, SP_DEVICE_INTERFACE_DATA interfaceData)
        {
            // Get required buffer size
            uint required_size = 0;
            SetupAPI.SetupDiGetDeviceInterfaceDetail(
                deviceInfoSet,
                ref interfaceData,
                IntPtr.Zero,
                0,
                out required_size,
                IntPtr.Zero);

            var lastErrorCode = Marshal.GetLastWin32Error();
            if (lastErrorCode != Win32ErrorCode.ERROR_INSUFFICIENT_BUFFER)
            {
                throw new System.ComponentModel.Win32Exception(lastErrorCode);
            }

            IntPtr interfaceDetailData = Marshal.AllocCoTaskMem((int)required_size);
            try
            {
                // See http://stackoverflow.com/questions/10728644/properly-declare-sp-device-interface-detail-data-for-pinvoke
                switch (IntPtr.Size)
                {
                    case 4:
                        Marshal.WriteInt32(interfaceDetailData, 4 + Marshal.SystemDefaultCharSize);
                        break;

                    case 8:
                        Marshal.WriteInt32(interfaceDetailData, 8);
                        break;

                    default:
                        throw new NotSupportedException(CoreRS.ArchitectureNotSupported);
                }

                if (SetupAPI.SetupDiGetDeviceInterfaceDetail(
                        deviceInfoSet,
                        ref interfaceData,
                        interfaceDetailData,
                        required_size,
                        out required_size,
                        IntPtr.Zero) == 0)
                {
                    var errorCode = Marshal.GetLastWin32Error();

                    if (errorCode != 0)
                    {
                        throw new Win32Exception(errorCode);
                    }
                }

                //return Marshal.PtrToStringAuto(IntPtr.Add(interface_detail_data, sizeof(int)));

                return Marshal.PtrToStringAuto(new IntPtr(
                    interfaceDetailData.ToInt64() +
                    Marshal.OffsetOf(typeof(SP_DEVICE_INTERFACE_DETAIL_DATA),
                    "DevicePath").ToInt64()));
            }
            finally
            {
                Marshal.FreeCoTaskMem(interfaceDetailData);
            }
        }


        //private static readonly Guid GUID_PRINTER_INSTALL_CLASS = new Guid(0x4d36e979, 0xe325, 0x11ce, 0xbf, 0xc1, 0x08, 0x00, 0x2b, 0xe1, 0x03, 0x18);

        //private static string GetPrinterRegistryInstanceID(string printerName)
        //{
        //    if (string.IsNullOrEmpty(printerName)) throw new ArgumentNullException("printerName");

        //    const string key_template = @"SYSTEM\CurrentControlSet\Control\Print\Printers\{0}\PNPData";

        //    using (var hk = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(
        //                        string.Format(key_template, printerName),
        //                        Microsoft.Win32.RegistryKeyPermissionCheck.Default,
        //                        System.Security.AccessControl.RegistryRights.QueryValues
        //                    )
        //            )
        //    {
        //        if (hk == null) throw new ArgumentOutOfRangeException("printerName", "This printer does not have PnP data.");

        //        return (string)hk.GetValue("DeviceInstanceId");
        //    }
        //}

        //private static string GetPrinterParentDeviceId(string registryInstanceID)
        //{
        //    if (string.IsNullOrEmpty(registryInstanceID)) throw new ArgumentNullException("registryInstanceID");

        //    IntPtr hdi = SetupAPI.SetupDiGetClassDevs(GUID_PRINTER_INSTALL_CLASS, registryInstanceID, IntPtr.Zero, DIGCF_PRESENT);
        //    if (hdi.Equals(INVALID_HANDLE_VALUE)) throw new System.ComponentModel.Win32Exception();

        //    try
        //    {
        //        SP_DEVINFO_DATA printer_data = new SP_DEVINFO_DATA();
        //        printer_data.cbSize = (uint)Marshal.SizeOf(typeof(SP_DEVINFO_DATA));

        //        if (SetupAPI.SetupDiEnumDeviceInfo(hdi, 0, ref printer_data) == 0) throw new System.ComponentModel.Win32Exception();   // Only one device in the set

        //        uint cmret = 0;

        //        uint parent_devinst = 0;
        //        cmret = CM_Get_Parent(out parent_devinst, printer_data.DevInst, 0);
        //        if (cmret != CR_SUCCESS) throw new Exception(string.Format("Failed to get parent of the device '{0}'. Error code: 0x{1:X8}", registryInstanceID, cmret));

        //        uint parent_device_id_size = 0;
        //        cmret = CM_Get_Device_ID_Size(out parent_device_id_size, parent_devinst, 0);
        //        if (cmret != CR_SUCCESS) throw new Exception(string.Format("Failed to get size of the device ID of the parent of the device '{0}'. Error code: 0x{1:X8}", registryInstanceID, cmret));

        //        parent_device_id_size++;  // To include the null character

        //        string parent_device_id = new string('\0', (int)parent_device_id_size);
        //        cmret = CM_Get_Device_ID(parent_devinst, parent_device_id, parent_device_id_size, 0);
        //        if (cmret != CR_SUCCESS) throw new Exception(string.Format("Failed to get device ID of the parent of the device '{0}'. Error code: 0x{1:X8}", registryInstanceID, cmret));

        //        return parent_device_id;
        //    }
        //    finally
        //    {
        //        SetupAPI.SetupDiDestroyDeviceInfoList(hdi);
        //    }
        //}
    }
}
