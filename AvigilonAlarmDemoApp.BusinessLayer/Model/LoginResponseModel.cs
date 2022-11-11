namespace AvigilonAlarmDemoApp.BusinessLayer.Model
{
    /// <summary>
    /// Object to model the log-in result from Avigilon Web Endpoint.
    /// </summary>

    public class LoginResponseModel
    {
        public string Session
        {
            get;
            set;
        }
      
        public string ExternalUserId
        {
            get;
            set;
        }
      
        public string DomainId
        {
            get;
            set;

        }
    }
}