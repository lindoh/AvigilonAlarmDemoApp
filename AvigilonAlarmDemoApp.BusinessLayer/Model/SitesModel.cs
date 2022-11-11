namespace AvigilonAlarmDemoApp.BusinessLayer.Model
{
    /// <summary>
    /// Object to model the alarm details response from Avigilon Web Endpoint.
    /// </summary>

    public class SitesModel
    {
        public string Id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string Reachable
        {
            get;
            set;
        }
    }
}