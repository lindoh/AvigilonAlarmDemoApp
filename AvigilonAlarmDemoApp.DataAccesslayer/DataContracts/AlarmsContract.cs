using System.Collections.Generic;

namespace AvigilonAlarmDemoApp.DataAccesslayer.DataContracts
{

    /// <summary>
    /// Object to model the result from get-alarms response from Avigilon Web Endpoint.
    /// </summary>
    [System.Runtime.Serialization.DataContract]
    public class AlarmsContract
    {
        [System.Runtime.Serialization.DataMember]
        public List<AlarmContract> alarms
        {
            get;
            set;
        }
    }

    /// <summary>
    /// Object to model the alarm details response from Avigilon Web Endpoint.
    /// </summary>
    [System.Runtime.Serialization.DataContract]
    public class AlarmContract
    {
        [System.Runtime.Serialization.DataMember]
        public string id
        {
            get;
            set;
        }

        [System.Runtime.Serialization.DataMember]
        public string name
        {
            get;
            set;
        }

        [System.Runtime.Serialization.DataMember]
        public string state
        {
            get;
            set;
        }

        [System.Runtime.Serialization.DataMember]
        public bool isAssignedToCurrentUser
        {
            get;
            set;
        }

        [System.Runtime.Serialization.DataMember]
        public string[] associatedCameras
        {
            get;
            set;
        }

        [System.Runtime.Serialization.DataMember]
        public bool isNoteRequired
        {
            get;
            set;
        }

        [System.Runtime.Serialization.DataMember]
        public string type
        {
            get;
            set;
        }

        [System.Runtime.Serialization.DataMember]
        public string timeOfMostRecentActivation
        {
            get;
            set;
        }
        [System.Runtime.Serialization.DataMember]
        public string missedTriggers
        {
            get;
            set;
        }
    }
}