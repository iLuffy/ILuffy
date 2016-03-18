namespace ILuffy.IOP.Printer
{
    using System;
    using I18N;
    using USB;
    public static class PrinterFactory
    {

        public static IPrinter CreateInstance(PrinterParameter parameter)
        {
            if (parameter == null)
            {
                throw new ArgumentNullException("parameter");
            }

            switch (parameter.Type)
            {
                case PrinterType.USB:
                    return new USBPrinter(parameter);
            }

            throw new NotSupportedException(CoreRS.PrinterNotSupportFormat(parameter.Type));
        }
    }
}
