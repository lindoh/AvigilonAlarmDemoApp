namespace AvigilonAlarmDemoApp.BusinessLayer.Model
{
    /// <summary>
    /// Object to model the login request to Avigilon Web Endpoint.
    /// </summary>
    public class LoginRequestModel
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string SiteId { get; set; }

        public string SiteName { get; set; }

        public string ClientId { get; set; }

        public string ClientName { get; set; }

        public string ClientVersion { get; set; }
    }
}

