using System;
using System.Diagnostics;

namespace GlossaryLogging
{
    public class EventLogger : ILogger
    {
        private const string eventsource = "Application";
        /// <summary>
        ///Will write the given message to the local eventlog.
        /// </summary>
        /// <param name="message">message to write to the local eventlog</param>
        /// <param name="isError">If isError is true then the entry in log will be flagged as an error and as information otherwise</param>
        public void WriteLog(string message, bool isError)
        {
            if (message == string.Empty)
                message = "Got empty log message!";

            try
            {
                // Create the source if it doesn't exist.
                if (!System.Diagnostics.EventLog.SourceExists(eventsource))
                {
                    System.Diagnostics.EventLog.CreateEventSource(eventsource, "Application");
                }

                EventLog objEventLog = new EventLog("Application", System.Environment.MachineName, eventsource);

                if (isError)
                {
                    objEventLog.WriteEntry(message, System.Diagnostics.EventLogEntryType.Error);
                }
                else
                {
                    objEventLog.WriteEntry(message, System.Diagnostics.EventLogEntryType.Information);
                }
                objEventLog.Close();
            }
            catch (Exception e)
            {
                throw new System.Exception("Couldn't create event entry", e);
            }
        }
    }
}
