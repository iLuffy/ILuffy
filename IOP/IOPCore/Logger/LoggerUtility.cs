namespace ILuffy.IOP
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Threading;
    using I18N;    /// <summary>
                   /// Logger Utility
                   /// </summary>
    public static class LoggerUtility
    {
        /// <summary>
        /// Global Instance
        /// </summary>
        public static ILogger Logger { get; set; }

        private static List<LoggerMessage> messages;
        private static ManualResetEvent manualWriteLogEvent;
        private static Thread innerThread;

        private static void AddMessage(LoggerMessage message)
        {
            lock (Logger)
            {
                if (manualWriteLogEvent == null)
                {
                    manualWriteLogEvent = new ManualResetEvent(false);
                }

                if (innerThread == null || innerThread.ThreadState == System.Threading.ThreadState.Stopped)
                {
                    innerThread = new Thread(WriteLogs);
                    innerThread.IsBackground = true;
                    innerThread.Start();
                }

                if (messages == null)
                {
                    messages = new List<LoggerMessage>() { message };
                }
                else
                {
                    messages.Add(message);

                    if (messages.Count > IOPConfig.Configuration.Logger.MaxLoggers)
                    {
                        manualWriteLogEvent.Set();
                    }
                }
            }
        }

        private static void WriteLogs()
        {
            try
            {
                var flushInterval = IOPConfig.Configuration.Logger.FlushInterval;
                while (true)
                {
                    if (messages != null && messages.Count > 0)
                    {
                        List<LoggerMessage> loggers = null;
                        lock (Logger)
                        {
                            loggers = new List<LoggerMessage>(messages);
                            messages.Clear();
                        }

                        foreach (var item in loggers)
                        {
                            Logger.WriteMessage(item);
                        }
                    }

                    manualWriteLogEvent.WaitOne(flushInterval);
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError(CoreRS.FlushLoggerErrorFormat(ex));
            }
        }

        /// <summary>
        /// Close
        /// </summary>
        public static void Close()
        {
            if (manualWriteLogEvent != null)
            {
                manualWriteLogEvent.Set();
                while (messages.Count != 0)
                {
                    Thread.Sleep(IOPConfig.Configuration.Logger.FlushInterval);
                }

                manualWriteLogEvent.Close();
            }
        }

        /// <summary>
        /// Write message
        /// </summary>
        /// <param name="severity"></param>
        /// <param name="format"></param>
        /// <param name="objs"></param>
        /// <returns></returns>
        public static bool WriteMessage(Severity severity, string format, params object[] objs)
        {
            var logger = Logger;
            if (logger != null && logger.CurrentSeverity <= severity)
            {
                var message = new LoggerMessage(severity, Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.Name, string.Format(format, objs));

                if (IOPConfig.Configuration.Logger.IsMultipleThreadEnabled)
                {
                    AddMessage(message);
                }
                else
                {
                    logger.WriteMessage(message);
                }

                return true;
            }

            return false;
        }
    }
}
