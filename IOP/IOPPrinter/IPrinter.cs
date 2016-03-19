namespace ILuffy.IOP.Printer
{
    using System;

    /// <summary>
    /// Printer
    /// </summary>
    public interface IPrinter : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="multiple">1-255</param>
        void ChangeLeading(byte multiple);

        void ResetLeading();

        /// <summary>
        /// Only work for one line, cannot set the alignment for particular content.
        /// </summary>
        /// <param name="type"></param>
        void ChangeAlignment(AlignmentType type);

        void Write(string content);

        void Write(string format, params object[] args);

        void WriteLine();

        void WriteLine(string format, params object[] args);

        void WriteLine(string content);

        void Flush();

        void CutPaper();
    }
}