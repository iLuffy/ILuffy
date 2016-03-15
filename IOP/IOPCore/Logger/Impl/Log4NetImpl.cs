namespace ILuffy.IOP.Logger.Impl
{
    using System;
    using System.Diagnostics;

    public class Log4NetImpl : ILogger
    {
        static Log4NetImpl()
        {
            if (log4net.GlobalContext.Properties["RelatedPath"] == null)
            {
                log4net.GlobalContext.Properties["RelatedPath"] = string.Empty;
            }
            if (log4net.GlobalContext.Properties["ProcessName"] == null)
            {
                log4net.GlobalContext.Properties["ProcessName"] = Process.GetCurrentProcess().ProcessName + ".exe";
            }

            if (System.Configuration.ConfigurationManager.GetSection("log4net") != null)
            {
                log4net.Config.XmlConfigurator.Configure();
            }
        }

        private log4net.ILog log;
        private Severity currentSeverity;

        public Log4NetImpl()
        {
            log = log4net.LogManager.GetLogger("");
            currentSeverity = Convert(
                ((log4net.Repository.Hierarchy.Hierarchy)log4net.LogManager.GetRepository()).Root.Level);
        }

        public Severity CurrentSeverity
        {
            get
            {
                return currentSeverity;
            }

            //set
            //{
            //    ((log4net.Repository.Hierarchy.Hierarchy)log4net.LogManager.GetRepository()).Root.Level = Convert(value);
            //}
        }

        private static Severity Convert(log4net.Core.Level level)
        {
            if (level == log4net.Core.Level.Debug)
            {
                return Severity.Debug;
            }
            else if (level == log4net.Core.Level.Info)
            {
                return Severity.Information;
            }
            else if (level == log4net.Core.Level.Warn)
            {
                return Severity.Warning;
            }
            else if (level == log4net.Core.Level.Error)
            {
                return Severity.Error;
            }
            else if (level == log4net.Core.Level.Trace)
            {
                return Severity.Trace;
            }

            return Severity.Information;
        }

        private static log4net.Core.Level Convert(Severity severity)
        {
            switch (severity)
            {
                case Severity.Debug:
                    return log4net.Core.Level.Debug;
                case Severity.Error:
                    return log4net.Core.Level.Error;
                case Severity.Information:
                    return log4net.Core.Level.Info;
                case Severity.Trace:
                    return log4net.Core.Level.Trace;
                case Severity.Warning:
                    return log4net.Core.Level.Warn;
            }

            return log4net.Core.Level.Info;
        }

        public void WriteMessage(LoggerMessage message)
        {
            var logLevel = Convert(message.Severity);
            var logEvent = new log4net.Core.LoggingEvent(null, log.Logger.Repository, log.Logger.Name, logLevel, message.Message, null);
            logEvent.Properties["ThreadId"] = message.ThreadId;
            logEvent.Properties["ThreadName"] = message.ThreadName ?? string.Empty;
            log.Logger.Log(logEvent);
        }
    }
}
