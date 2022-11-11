namespace AvigilonAlarmDemoApp.DataAccesslayer.Manager
{
    /// <summary>
    ///  Class to communicate with ACC webApi
    /// </summary>
    public class AvigilonWebEndpointServiceManager
    {
        private readonly AvigilonAlarmDemoApp.DataAccesslayer.Utils.HttpClient m_httpClient;
        public AvigilonWebEndpointServiceManager()
        {
            m_httpClient = new AvigilonAlarmDemoApp.DataAccesslayer.Utils.HttpClient();
        }

        /// <summary>
        /// Sets the base URL to the Avigilon Web Endpoint.
        /// </summary>
        /// <param name="baseUrl">The URL to reach Avigilon Web Endpoint.</param>
        public void SetBaseUrl(string baseUrl)
        {
            m_httpClient.SetBaseUrl(baseUrl);
        }

        /// <summary>
        /// Log into the Avigilon Web Endpoint.
        /// </summary>
        /// <param name="loginRequest">The parameter required to log into Avigilon Web Endpoint.</param>
        /// <returns>Boolean value to indicate whether or not log in was successful.</returns>
        public DataContracts.LoginResponseContract LoginToAvigilonWebEndpoint(DataContracts.LoginRequestContract loginRequest)
        {
            SanitizeLoginRequest_(loginRequest);
            string loginParamJson = JsonSerializationHelper.SeralizeObjectToJson(loginRequest);
            return m_httpClient.HttpPost<DataContracts.LoginResponseContract>(AvigilonAlarmDemoAppDataLayerResource.LoginPath, loginParamJson);
        }

        /// <summary>
        /// Sanitizes the log in request parameters.
        /// Empty json value as a parameter is invalid request to Avigilon Web Endpoint.
        /// </summary>
        /// <param name="loginRequest">The parameter required to log into Avigilon Web Endpoint.</param>
        private void SanitizeLoginRequest_(DataContracts.LoginRequestContract loginRequest)
        {
            if (string.Empty == loginRequest.siteId)
                loginRequest.siteId = null;
            if (string.Empty == loginRequest.siteName)
                loginRequest.siteName = null;
            if (string.Empty == loginRequest.clientId)
                loginRequest.clientId = null;
            if (string.Empty == loginRequest.clientVersion)
                loginRequest.clientVersion = null;
        }
        /// <summary>
        /// Read Sites From AvigilonWebEndPoint
        /// </summary>
        /// <returns></returns>
        public DataContracts.SiteDetailsContract ReadSitesFromAvigilonWebEndPoint()
        {
            return m_httpClient.HttpGetWithNoParametrs<DataContracts.SiteDetailsContract>(AvigilonAlarmDemoAppDataLayerResource.SitePath);
            
        }


        /// <summary>
        /// Query the Avigilon Web Endpoint with the log in session ID for alarms
        /// </summary>
        /// <returns>True for successful query, false otherwise.</returns>
        public DataContracts.AlarmResponseContract QueryAvigilonWebEndpointForAlarms(string session)
        {
            string currentSessionQuery = AvigilonAlarmDemoAppDataLayerResource.SessionQuery + session;
            DataContracts.AlarmResponseContract alarms = m_httpClient.HttpGet<DataContracts.AlarmResponseContract>(AvigilonAlarmDemoAppDataLayerResource.AlarmsPath, currentSessionQuery);
            return alarms;
        }

        /// <summary>
        /// Query the Avigilon Web Endpoint with the log in session ID for cameras
        /// </summary>
        /// /// <param name="session"></param>
        /// <returns>cameras</returns>
        public DataContracts.CameraResponseContract QueryAvigilonWebEndpointForCameras(string session)
        {
            string currentSessionQuery = AvigilonAlarmDemoAppDataLayerResource.SessionQuery + session;
            DataContracts.CameraResponseContract cameras = m_httpClient.HttpGet<DataContracts.CameraResponseContract>(AvigilonAlarmDemoAppDataLayerResource.CamerasPath, currentSessionQuery);
            return cameras;
        }

        /// <summary>
        /// Query the Avigilon Web Endpoint for updating alarm status
        /// </summary>
        /// <param name="selectedAlarm"></param>
        /// <returns>True for successful query, false otherwise.</returns>
        public bool QueryAvigilonWebEndpointForUpdatingAlarmStatus(DataContracts.UpdateAlarmRequestContract selectedAlarm)
        {
            bool isSuccess = false;
            DataContracts.UpdateAlarmRequestContract m_alarmUpdateRequest = selectedAlarm;
            string m_alarmUrlParamJson = JsonSerializationHelper.SeralizeObjectToJson(m_alarmUpdateRequest);
            DataContracts.UpdateAlarmResponseContract getAlarmUpdate = m_httpClient.HttpPut<DataContracts.UpdateAlarmResponseContract>(AvigilonAlarmDemoAppDataLayerResource.AlarmUpdatePath, m_alarmUrlParamJson);
            DataContracts.UpdateAlarmResponseContract m_alarmupdate = getAlarmUpdate;
            isSuccess = (m_alarmupdate != null);
            return isSuccess;
           
        }

        /// <summary>
        /// Log out of the Avigilon Web Endpoint.
        /// </summary>
        /// <param name="session"></param>
        /// <returns>Boolean value to indicate whether or not log out was successful.</returns>
        public bool LogoutFromAvigilonWebEndpoint(DataContracts.Session session)
        {
            string currentSessionQuery = JsonSerializationHelper.SeralizeObjectToJson(session);
            DataContracts.LogoutContract logoutResult = m_httpClient.HttpPost<DataContracts.LogoutContract>(AvigilonAlarmDemoAppDataLayerResource.LogoutPath, currentSessionQuery);
            return (logoutResult != null);
        }
    }
}