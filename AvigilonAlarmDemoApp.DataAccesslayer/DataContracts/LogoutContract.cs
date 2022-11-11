namespace AvigilonAlarmDemoApp.DataAccesslayer.DataContracts
{
    /// <summary>
    /// Object to model the log-out result from Avigilon Web Endpoint.
    /// </summary>
    [System.Runtime.Serialization.DataContract]
    public class LogoutContract
    {
        [System.Runtime.Serialization.DataMember]
        public string status;
    }

    /// <summary>
    /// Object to model the session parameter in the requests to Avigilon Web Endpoint.
    /// </summary>
    [System.Runtime.Serialization.DataContract]
    public class Session
    {
        [System.Runtime.Serialization.DataMember]
        public string session;
    }
}
