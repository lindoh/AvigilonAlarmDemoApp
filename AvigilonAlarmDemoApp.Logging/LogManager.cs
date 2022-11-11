using NLog;

namespace AvigilonAlarmDemoApp.Logging
{
    public static class LoggerManager
    {
        public static Logger AvigilonAlarmLogger
        {
            get
            {
               return LogManager.GetLogger("AvigilonAlarmLogger");
            }
        }
    }
}
