namespace AvigilonAlarmDemoApp.BusinessLayer.Model
{
    /// <summary>
    /// Object to model the log-out result from Avigilon Web Endpoint.
    /// </summary>

    public class LogoutModel
    {       
        public string Status;

        public bool IsSuccess()
        {
            return (Status != null &&
                    Status == AvigilonAlarmDemoAppBusinessLayer.Success);
        }
    }
}
