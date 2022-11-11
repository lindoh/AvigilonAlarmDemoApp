namespace AvigilonAlarmDemoApp.DataAccesslayer.DataContracts
{
    /// <summary>
    /// Object to model the get-alarms response from Avigilon Web Endpoint.
    /// </summary>
    [System.Runtime.Serialization.DataContract]
    public class AlarmResponseContract
    {
        [System.Runtime.Serialization.DataMember]
        public string status
        {
            get;
            set;
        }

        [System.Runtime.Serialization.DataMember]
        public AlarmsContract result
        {
            get;
            set;
        }    
    }

    
}