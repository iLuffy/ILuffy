namespace ILuffy.IOP.Printer
{
    using System;

    /// <summary>
    /// Printer
    /// </summary>
    public interface IPrinter : IDisposable
    {
        void WriteLine();

        void WriteLine(string format, params object[] args);

        void WriteLine(string line);

        void Flush();

        void CutPaper();
    }
}