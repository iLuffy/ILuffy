namespace ILuffy.IOP.UI.Input
{
    using System;

    public class MessageItem
    {
        public MessageItem(MessageLevel level, string title, string message)
        {
            Level = level;
            Title = title;
            Message = message;
        }

        public MessageItem(MessageLevel level, string title, string message, MessageCallback callback)
            : this(level, title, message)
        {
            Callback = callback;
        }

        public MessageLevel Level { get; private set; }

        public string Title { get; private set; }

        public string Message { get; private set; }

        public MessageCallback Callback { get; private set; }
    }

    public class MessageResult
    {
        public MessageResult(bool dialogResult)
        {
            DialogResult = dialogResult;
        }

        public bool DialogResult { get; private set; }
    }

    public delegate void MessageCallback(MessageResult result);

    public enum MessageLevel
    {
        Verbose,
        Info,
        Warn,
        Error
    }
}
