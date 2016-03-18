namespace ILuffy.IOP.Printer
{
    /// <summary>
    /// Parameter
    /// </summary>
    public class PrinterParameter : BaseProperties
    {
        public PrinterType Type { get; set; }

        public string EncodingName { get; set; }
    }
}