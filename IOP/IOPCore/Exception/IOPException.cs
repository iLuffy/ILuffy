namespace ILuffy.IOP
{
    using System;

    public class IOPException : Exception
    {
        public int ErrorCode { get; set; }

        public IOPException(string message) : base(message) { }

        public IOPException(int errorCode, string message) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
