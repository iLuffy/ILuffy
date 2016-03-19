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

        public void ChangeAlignment(AlignmentType type)
        {
            switch(type)
            {
                case AlignmentType.Left:
                    fileStream.WriteByte(27);
                    fileStream.WriteByte(97);
                    fileStream.WriteByte(48);
                    break;
                case AlignmentType.Center:
                    fileStream.WriteByte(27);
                    fileStream.WriteByte(97);
                    fileStream.WriteByte(49);
                    break;
                case AlignmentType.Right:
                    fileStream.WriteByte(27);
                    fileStream.WriteByte(97);
                    fileStream.WriteByte(50);
                    break;
            }
        }

        public void Write(string content)
        {
            Assert();
            var bytes = encoding.GetBytes(content);
            fileStream.Write(bytes, 0, bytes.Length);
        }

        public void Write(string format, params object[] args)
        {
            Write(string.Format(format, args));
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

        public void WriteLine(string content)
        {
            Write(content);
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

        public void ChangeLeading(byte multiple)
        {
            Assert();
            fileStream.WriteByte(27);
            fileStream.WriteByte(51);
            fileStream.WriteByte(multiple);
        }

        public void ResetLeading()
        {
            Assert();
            fileStream.WriteByte(27);
            fileStream.WriteByte(50);
        }
    }
}