namespace ILuffy.IOP.Printer.USB
{
    using System;
    using System.IO;
    using System.Text;
    using Hardware;
    using I18N;
    using Microsoft.Win32.SafeHandles;
    using Win32.Core;
    using Win32.Hardware;

    internal class USBPrinter : IPrinter
    {
        private PrinterParameter parameter;
        private Encoding encoding;
        private FileStream fileStream;

        public USBPrinter(PrinterParameter parameter)
        {
            if (parameter == null)
            {
                throw new ArgumentNullException("parameter");
            }

            this.parameter = parameter;
            encoding = Encoding.GetEncoding(parameter.EncodingName);
            Initialize();
        }

        private void Initialize()
        {
            var printer = DeviceUtil.GetAllDevicePath(SetupAPI.GUID_DEVINTERFACE_USBPRINT, null);
            if (printer == null || printer.Count == 0)
            {
                throw new IOPException(IOPErrorCode.NoAvailablePrinter, CoreRS.NoAvailablePrinter);
            }

            var fileHandle = new SafeFileHandle(
                KernelAPI.CreateFile(
                    printer[0],
                    (uint)(GenericAccessRight.GENERIC_READ | GenericAccessRight.GENERIC_WRITE),
                    FileShare.ReadWrite,
                    IntPtr.Zero,
                    FileMode.Open,
                    0,
                    IntPtr.Zero),
                true);
            fileStream = new FileStream(fileHandle, FileAccess.ReadWrite);
        }

        private void Release()
        {
            if(fileStream != null)
            {
                fileStream.Dispose();
                fileStream = null;
            }
        }

        private void Assert()
        {
            if (fileStream == null)
            {
                throw new NullReferenceException("fileStream");
            }
        }

        public void WriteLine(string format, params object[] args)
        {
            WriteLine(string.Format(format, args));
        }

        public void WriteLine()
        {
            Assert();
            fileStream.WriteByte(10);
        }

        public void WriteLine(string line)
        {
            Assert();
            var bytes = encoding.GetBytes(line);
            fileStream.Write(bytes, 0, bytes.Length);
            WriteLine();
        }

        public void CutPaper()
        {
            
        }

        public void Flush()
        {
            Assert();
            fileStream.Flush();
        }

        

        public void Dispose()
        {
            Release();
        }
    }
}