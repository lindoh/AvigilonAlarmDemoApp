using System.Collections.Generic;
using System.Drawing;

namespace AvigilonAlarmDemoApp.UI.Model
{
    /// <summary>
    /// Object to model the alarm details response from Avigilon Web Endpoint.
    /// </summary>   
    public class AlarmModel : System.ComponentModel.INotifyPropertyChanged
    {
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        string m_state = string.Empty;
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
            get { return m_state; }
            set
            {
                if (m_state != value)
                {
                    m_state = value;
                    OnPropertyChanged("State");
                    OnPropertyChanged("NameBrush");
                }
            }
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

        public Brush NameBrush
        {
            get
            {
                switch (State)
                {
                    case "ACTIVE":
                        return Brushes.Orange;
                    default:
                        break;
                }

                return Brushes.Transparent;
            }
        }

        public void OnPropertyChanged(string propertyName)
        {
            System.ComponentModel.PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
}