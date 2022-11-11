using System.Collections.Generic;

namespace AvigilonAlarmDemoApp.BusinessLayer.Model
{
    /// <summary>
    /// Object to model the alarm details response from Avigilon Web Endpoint.
    /// </summary>

    public class AlarmModel
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


        public string State
        {
            get;
            set;
        }


        public bool IsAssignedToCurrentUser
        {
            get;
            set;
        }


        public string[] AssociatedCameras
        {
            get;
            set;
        }

        public bool IsNoteRequired
        {
            get;
            set;
        }

        public string Type
        {
            get;
            set;
        }

        public string TimeOfMostRecentActivation
        {
            get;
            set;
        }

        public List<string> Cameras
        {
            get;
            set;
        }

        public string MissedTriggers
        {
            get;
            set;
        }
    }
}