namespace ILuffy.IOP
{

    public interface ILogger
    {
        Severity CurrentSeverity { get; }

        void WriteMessage(LoggerMessage message);
    }
    
}
