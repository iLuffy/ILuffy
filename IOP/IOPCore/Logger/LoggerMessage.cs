namespace ILuffy.IOP
{
    public class LoggerMessage
    {
        public LoggerMessage(Severity severity, int threadId, string threadName, string message)
        {
            Severity = severity;
            ThreadId = threadId;
            ThreadName = threadName;
            Message = message;
        }

        public Severity Severity { get; private set; }

        public int ThreadId { get; private set; }

        public string ThreadName { get; private set; }

        public string Message { get; private set; }
    }
}
