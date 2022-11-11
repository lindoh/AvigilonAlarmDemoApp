namespace AvigilonAlarmDemoApp.DataAccesslayer.DataContracts
{
    /// <summary>
    /// Object to model the login response from Avigilon Web Endpoint.
    /// </summary>
    [System.Runtime.Serialization.DataContract]
    public class LoginResponseContract
    {
        [System.Runtime.Serialization.DataMember]
        public string status
        {
            get;
            set;
        }

        [System.Runtime.Serialization.DataMember]
        public LoginResultContract result;
    }
}