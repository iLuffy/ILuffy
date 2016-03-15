namespace ILuffy.IOP.Retry
{
    using System;

    public sealed class RetryContext
    {
        public Int32 CurrentRetryCount { get; set; }
        public Int32 StatusCode { get; set; }
        public Exception LastException { get; set; }
        public TimeSpan RetryInterval { get; set; }
    }
}
