namespace AvigilonAlarmDemoApp.DataAccesslayer.DataContracts
{
    /// <summary>
    /// Object to model the log-in result from Avigilon Web Endpoint.
    /// </summary>
    [System.Runtime.Serialization.DataContract]
    public class LoginResultContract
    {
        [System.Runtime.Serialization.DataMember]
        public string session
        {
            get;
            set;
        }

        [System.Runtime.Serialization.DataMember]
        public string externalUserId
        {
            get;
            set;
        }

        [System.Runtime.Serialization.DataMember]
        public string domainId
        {
            get;
            set;

        }
    }
}