namespace AvigilonAlarmDemoApp.DataAccesslayer.DataContracts
{
    /// <summary>
    /// Object to model the alarm update request from Avigilon Web Endpoint.
    /// </summary>
    [System.Runtime.Serialization.DataContract]
    public class UpdateAlarmRequestContract
    {
        [System.Runtime.Serialization.DataMember]
        public string id
        {
            get;
            set;
        }

        [System.Runtime.Serialization.DataMember]
        public string action
        {
            get;
            set;
        }
        [System.Runtime.Serialization.DataMember]
        public string note
        {
            get;
            set;
        }

        [System.Runtime.Serialization.DataMember]
        public string session
        {
            get;
            set;
        }

        [System.Runtime.Serialization.DataMember]
        public string permission
        {
            get;
            set;
        }
    }
}
