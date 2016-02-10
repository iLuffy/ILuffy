using System;
using ILuffy.Halo.Printer.USB;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ILuffy.Halo.Printer.Test
{
    [TestClass]
    public class USBPrinterTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            USBPrinter.TestUSBPrinterWriter();
            //var printer = new USBPrinter(null);
            var devices = USBPrinter.TestUSBPrinterDirectly();

            Console.WriteLine(devices.Count);
        }
    }
}