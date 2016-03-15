namespace ILuffy.IOP.Win32.Hardware
{
    using System;
    using System.Runtime.InteropServices;
    public static class PnPCMAPI
    {

        [DllImport("cfgmgr32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern uint CM_Get_Parent(
            out uint pdnDevInst, 
            uint dnDevInst, 
            uint ulFlags);

        [DllImport("cfgmgr32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern uint CM_Get_Device_ID(
            uint dnDevInst, 
            string buffer, 
            uint bufferLen, 
            uint ulFlags);

        [DllImport("cfgmgr32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern uint CM_Get_Device_ID_Size(
            out uint pulLen, 
            uint dnDevInst, 
            uint ulFlags);

    }
}
