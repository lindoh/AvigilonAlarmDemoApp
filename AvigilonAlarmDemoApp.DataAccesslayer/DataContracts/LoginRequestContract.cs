namespace AvigilonAlarmDemoApp.DataAccesslayer.DataContracts
{
    /// <summary>
    /// Object to model the login request to Avigilon Web Endpoint.
    /// </summary>
    [System.Runtime.Serialization.DataContract]
    public class LoginRequestContract {

        [System.Runtime.Serialization.DataMember]
        public string username
        {
            get;
            set;
        }

        [System.Runtime.Serialization.DataMember]
        public string password
        {
            get;
            set;
        }

        [System.Runtime.Serialization.DataMember(EmitDefaultValue = false)]
        public string siteId
        {
            get;
            set;
        }

        [System.Runtime.Serialization.DataMember(EmitDefaultValue = false)]
        public string siteName
        {
            get;
            set;
        }

        [System.Runtime.Serialization.DataMember(EmitDefaultValue = false)]
        public string clientId
        {
            get;
            set;
        }

        [System.Runtime.Serialization.DataMember]
        public string clientName
        {
            get;
            set;
        }

        [System.Runtime.Serialization.DataMember(EmitDefaultValue = false)]
        public string clientVersion
        {
            get;
            set;
        }       

    }
}

