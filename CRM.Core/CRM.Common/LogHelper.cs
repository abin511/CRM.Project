using System;

namespace CRM.Common
{
    public class LogHelper
    {
        private static readonly log4net.ILog Log4Net = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        //private readonly Type _logType;
        //public LogHelper(Type logType)
        //{
        //    _logType = logType;
        //}
        
        public static void Info(string msg)
        {
            Log4Net.Info(msg);

        }
        public static void Info(Exception ex)
        {
            Log4Net.Info(ex);

        }
        public static void InfoFormat(string format, params object[] args)
        {
            Log4Net.InfoFormat(format, args);

        }
        public static void Debug(string message)
        {
            Log4Net.Debug(message);
        }

        public static void Debug(Exception exception)
        {
            Log4Net.Debug(exception);
        }

        public static void DebugFormat(string format, params object[] args)
        {
            Log4Net.DebugFormat(format, args);
        }
        public static void Warn(string msg)
        {
            Log4Net.Warn(msg);

        }
        public static void Warn(Exception ex)
        {
            Log4Net.Warn(ex);

        }
        public static void WarnFormat(string format, params object[] args)
        {
            Log4Net.WarnFormat(format, args);
        }
        public static void Error(string msg)
        {
            Log4Net.Error(msg);

        }
        public static void Error(Exception ex)
        {
            Log4Net.Error(ex);

        }
        public static void ErrorFormat(string format, params object[] args)
        {
            Log4Net.ErrorFormat(format, args);
        }
    }
}
