namespace AvigilonAlarmDemoApp.DataAccesslayer.DataContracts
{
    /// <summary>
    /// Object to model the get-cameras response from Avigilon Web Endpoint.
    /// </summary>
    [System.Runtime.Serialization.DataContract]
    public class CameraResponseContract
    {
        [System.Runtime.Serialization.DataMember]
        public string status
        {
            get;
            set;
        }

        [System.Runtime.Serialization.DataMember]
        public CamerasContract result
        {
            get;
            set;
        }
    }


}