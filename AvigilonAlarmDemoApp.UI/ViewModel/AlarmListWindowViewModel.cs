using NLog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;

namespace AvigilonAlarmDemoApp.UI.ViewModel
{
    /// <summary>
    /// View Model for Alarm List Window
    /// </summary>
    public class AlarmListWindowViewModel : AvigilonAlarmDemoAppBaseViewModel
    {
        private string m_sessionIdText;
        private ObservableCollection<Model.AlarmModel> m_alarms;
        private Model.AlarmModel m_selectedAlarm;
        private bool m_isAcknowledged;
        private bool m_isPurged;
        private bool m_isAck;
        private bool m_isAssign;
        private bool m_isUnassign;
        private bool m_isSessionValid;
        private Services.DialogueService m_dialogueService;
        private DispatcherTimer dispatcherTimer;

        public ICommand AcknowledgeCommand { get; set; }
        public ICommand AssignCommand { get; set; }
        public ICommand UnassignCommand { get; set; }
        public ICommand PurgeCommand { get; set; }
        public ICommand SelectionChangeCommand { get; set; }
        public ICommand LogoutCommand { get; set; }


        private Visibility m_assignButtonVisibility;
        private Visibility m_unassignButtonVisibility;

        
        private Logger m_avigilonAlarmLog = Logging.LoggerManager.AvigilonAlarmLogger;
        /// <summary>
        /// Constructor.
        /// </summary>
        public AlarmListWindowViewModel(BusinessLayer.BusinessLogic.AvigilonWebEndPointServiceBusinessLogic avigilonWebEndpointServiceManager)
            : base(avigilonWebEndpointServiceManager)
        {
            AssignButtonVisibility = Visibility.Visible;
            UnassignButtonVisibility = Visibility.Collapsed;

            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(LoadAllAlarmEvent);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 2);
            dispatcherTimer.Start();

            AcknowledgeCommand = new Commands.CustomCommand(Acknowledge, CanAcknowledgeCommand);
            AssignCommand = new Commands.CustomCommand(Assign, CanAssignCommand);
            UnassignCommand = new Commands.CustomCommand(Unassign, CanUnassignCommand);
            PurgeCommand = new Commands.CustomCommand(Purge, CanPurgeCommand);
            SelectionChangeCommand = new Commands.CustomCommand(SelectionChange, CanSelectionChangeCommand);
            LogoutCommand = new Commands.CustomCommand(Logout, CanLogoutCommand);
        }


        /// <summary>
        /// gets and sets the Visibility of AssignButton
        /// </summary>
        public Visibility AssignButtonVisibility
        {
            get
            {
                return m_assignButtonVisibility;
            }
            set
            {
                m_assignButtonVisibility = value;

                OnPropertyChanged("AssignButtonVisibility");
            }
        }
        /// <summary>
        /// gets and sets the Visibility of UnassignButton
        /// </summary>
        public Visibility UnassignButtonVisibility
        {
            get
            {
                return m_unassignButtonVisibility;
            }
            set
            {
                m_unassignButtonVisibility = value;

                OnPropertyChanged("UnassignButtonVisibility");
            }
        }

        /// <summary>
        /// Event to Get all Avigilon Sites
        /// </summary>
        private void LoadAllAlarmEvent(object sender, EventArgs e)
        {
            LoadAllAlarms();
        }

        /// <summary>
        /// LoadAll Alarms
        /// </summary>
        public void LoadAllAlarms()
        {
            try
            {
                var selectedalarm = SelectedAlarm;
                LoadAlarms();
                if (m_isSessionValid)
                {
                    if (Alarms != null)
                    {
                        Alarms.Clear();
                        Alarms = null;
                    }
                    List<BusinessLayer.Model.AlarmModel> alarms = m_avigilonWebEndpointServiceManager.GetAlarms();
                    List<Model.AlarmModel> alarmModel = new List<Model.AlarmModel>();
                    foreach (BusinessLayer.Model.AlarmModel alarm in alarms)
                    {
                        alarmModel.Add(new Model.AlarmModel
                        {
                            Id = alarm.Id,
                            Name = alarm.Name,
                            State = alarm.State,
                            IsAssignedToCurrentUser = alarm.IsAssignedToCurrentUser,
                            IsNoteRequired = alarm.IsNoteRequired,
                            TimeOfMostRecentActivation = alarm.TimeOfMostRecentActivation,
                            Type = alarm.Type,
                            AssociatedCameras = alarm.AssociatedCameras,
                            Cameras = alarm.Cameras,
                            MissedTriggers = alarm.MissedTriggers
                        });
                    }
                    Alarms =new ObservableCollection<Model.AlarmModel>(alarmModel);
                    if (selectedalarm != null)
                        SelectedAlarm = Alarms.FirstOrDefault(s => s != null && s.Name == selectedalarm.Name);
                }
            }
            catch (Exception ex)
            {
                m_avigilonAlarmLog.Error(ex, ex.Message);
            }

        }
        /// <summary>
        /// gets and sets the Session Id
        /// </summary>
        public string SessionId
        {
            get { return m_sessionIdText; }
            set
            {
                if (!string.Equals(m_sessionIdText, value))
                {
                    m_sessionIdText = value;
                    OnPropertyChanged("SessionId");
                };
            }
        }
        /// <summary>
        /// Gets and sets the Selected alarm property
        /// </summary>

        public Model.AlarmModel SelectedAlarm
        {
            get
            {
                return m_selectedAlarm;
            }
            set
            {
                m_selectedAlarm = value;
                OnPropertyChanged("SelectedAlarm");
            }
        }


        /// <summary>
        /// Gets and sets the Alarms property.
        /// </summary>
        public ObservableCollection<Model.AlarmModel> Alarms
        {
            get { return m_alarms; }
            set
            {

                if (m_alarms == null || !m_alarms.Equals(value))
                {
                    m_alarms = value;
                    OnPropertyChanged("Alarms");
                };
            }
        }      
      
       


        /// <summary>
        /// Gets and sets the IsPurgeEnabled property
        /// </summary>
        public bool IsPurgeEnabled
        {
            get
            {
                return m_isPurged;
            }
            set
            {
                m_isPurged = value;
                OnPropertyChanged("IsPurgeEnabled");
            }
        }

        /// <summary>
        /// Gets and sets the IsAckEnabled property
        /// </summary>
        public bool IsAckEnabled
        {
            get
            {
                return m_isAck;
            }
            set
            {
                m_isAck = value;
                OnPropertyChanged("IsAckEnabled");
            }
        }

        /// <summary>
        /// Gets and sets the IsAssignEnabled property
        /// </summary>
        public bool IsAssignEnabled
        {
            get
            {
                return m_isAssign;
            }
            set
            {
                m_isAssign = value;
                OnPropertyChanged("IsAssignEnabled");
            }
        }

        /// <summary>
        /// Gets and sets the IsUnassignEnabled property
        /// </summary>
        public bool IsUnassignEnabled
        {
            get
            {
                return m_isUnassign;
            }
            set
            {
                m_isUnassign = value;
                OnPropertyChanged("IsUnassignEnabled");
            }
        }

        /// <summary>
        /// Method to Load Alarms
        /// </summary>
        /// <returns></returns>
        private bool LoadAlarms()
        {
            try
            {

                m_isSessionValid = m_avigilonWebEndpointServiceManager.QueryAvigilonWebEndpointForAlarms();
                return m_isSessionValid;
            }
            catch (Exception ex)
            {
                m_avigilonAlarmLog.Error(ex, ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Method to acknowledge alarm
        /// </summary>
        /// <param name="_newConnection"></param>
        private void Acknowledge(object _selected)
        {

            m_isAcknowledged = true;
            IsNoteRequired(SelectedAlarm);
        }

        private bool CanAcknowledgeCommand(object obj)
        {
            return true;
        }


        /// <summary>
        /// Method to selection Change
        /// </summary>
        /// <param name="_selected"></param>
        private void SelectionChange(object _selected)
        {
            ActionsToSelectionChange(SelectedAlarm);

        }
        private bool CanSelectionChangeCommand(object obj)
        {
            return true;
        }

        /// <summary>
        /// Method for enabling and disabling buttons on selection change
        /// </summary>
        /// <param name="selectedAlarm"></param>
        private void ActionsToSelectionChange(Model.AlarmModel selectedAlarm)
        {
            if (selectedAlarm != null && !string.IsNullOrEmpty(selectedAlarm.State))
            {

                AssignButtonVisibility = Visibility.Visible;
                UnassignButtonVisibility = Visibility.Collapsed;
                switch (selectedAlarm.State.ToLower())
                {
                    case "acknowledged":
                        IsPurgeEnabled = true;
                        IsAckEnabled = false;
                        IsAssignEnabled = false;
                        break;
                    case "assigned":
                        IsPurgeEnabled = false;
                        if (SelectedAlarm.IsAssignedToCurrentUser)
                            IsAckEnabled = true;
                        else
                            IsAckEnabled = false;
                        AssignButtonVisibility = Visibility.Collapsed;
                        UnassignButtonVisibility = Visibility.Visible;
                        IsAssignEnabled = false;
                        if (IsUnassignEnabled)
                            IsUnassignEnabled = false;
                        break;
                    case "active":
                        IsPurgeEnabled = false;
                        IsAckEnabled = true;
                        IsAssignEnabled = true;
                        IsUnassignEnabled = false;
                        break;
                    case "purged":
                        IsPurgeEnabled = false;
                        IsAckEnabled = false;
                        IsAssignEnabled = false;
                        if (IsUnassignEnabled)
                            IsUnassignEnabled = false;
                        break;
                    default:
                        IsPurgeEnabled = false;
                        IsAckEnabled = false;
                        IsAssignEnabled = false;
                        break;
                }
            }
            else
            {
                IsPurgeEnabled = false;
                IsAckEnabled = false;
                IsAssignEnabled = false;
            }

        }

        /// <summary>
        /// Method to Assign the alarm
        /// </summary>
        /// <param name="_selected"></param>
        private void Assign(object _selected)
        {
            try
            {
                BusinessLayer.Model.AlarmModel selectedAlarm = new BusinessLayer.Model.AlarmModel
                {
                    Id = SelectedAlarm.Id,
                    Name = SelectedAlarm.Name,
                    State = SelectedAlarm.State,
                    IsAssignedToCurrentUser = SelectedAlarm.IsAssignedToCurrentUser,
                    AssociatedCameras = SelectedAlarm.AssociatedCameras,
                    IsNoteRequired = SelectedAlarm.IsNoteRequired,
                    Type = SelectedAlarm.Type,
                    TimeOfMostRecentActivation = SelectedAlarm.TimeOfMostRecentActivation,
                    MissedTriggers = SelectedAlarm.MissedTriggers
                };

                BusinessLayer.Model.ActionsModelType action = (BusinessLayer.Model.ActionsModelType)((int)Model.ActionTypes.CLAIM);
                m_avigilonWebEndpointServiceManager.QueryAvigilonWebEndpointForUpdatingAlarmStatus(selectedAlarm, string.Empty, action);
                LoadAllAlarms();
                AssignButtonVisibility = Visibility.Collapsed;
                UnassignButtonVisibility = Visibility.Visible;
                IsUnassignEnabled = true;
            }
            catch (Exception ex)
            {
                m_avigilonAlarmLog.Error(ex, ex.Message);
            }
        }


        private bool CanAssignCommand(object obj)
        {
            return true;
        }

        /// <summary>
        /// Method to Purge the alarm
        /// </summary>
        /// <param name="_selected"></param>
        private void Purge(object _selected)
        {
            try
            {
                BusinessLayer.Model.AlarmModel selectedAlarm = new BusinessLayer.Model.AlarmModel
                {
                    Id = SelectedAlarm.Id,
                    Name = SelectedAlarm.Name,
                    State = SelectedAlarm.State,
                    IsAssignedToCurrentUser = SelectedAlarm.IsAssignedToCurrentUser,
                    AssociatedCameras = SelectedAlarm.AssociatedCameras,
                    IsNoteRequired = SelectedAlarm.IsNoteRequired,
                    Type = SelectedAlarm.Type,
                    TimeOfMostRecentActivation = SelectedAlarm.TimeOfMostRecentActivation,
                    MissedTriggers = SelectedAlarm.MissedTriggers
                };

                BusinessLayer.Model.ActionsModelType action = (BusinessLayer.Model.ActionsModelType)((int)Model.ActionTypes.PURGE);
                bool isSuccess = m_avigilonWebEndpointServiceManager.QueryAvigilonWebEndpointForUpdatingAlarmStatus(selectedAlarm, string.Empty, action);
                Utility.Messenger.Default.Send<BusinessLayer.Model.ActionsModelType>(action);
                if (m_dialogueService == null)
                    m_dialogueService = new Services.DialogueService();
                if (isSuccess)
                {
                    m_dialogueService.ShowSuccessWndowViewDialog();
                }
                else
                {
                    m_dialogueService.ShowFailureWndowViewDialog();
                }
                LoadAllAlarms();
            }
            catch (Exception ex)
            {
                m_avigilonAlarmLog.Error(ex, ex.Message);
            }
        }

        private bool CanPurgeCommand(object obj)
        {
            return true;
        }


        /// <summary>
        /// Method to Unassign the alarm
        /// </summary>
        /// <param name="_selected"></param>
        private void Unassign(object _selected)
        {
            try
            {
                BusinessLayer.Model.AlarmModel selectedAlarm = new BusinessLayer.Model.AlarmModel
                {
                    Id = SelectedAlarm.Id,
                    Name = SelectedAlarm.Name,
                    State = SelectedAlarm.State,
                    IsAssignedToCurrentUser = SelectedAlarm.IsAssignedToCurrentUser,
                    AssociatedCameras = SelectedAlarm.AssociatedCameras,
                    IsNoteRequired = SelectedAlarm.IsNoteRequired,
                    Type = SelectedAlarm.Type,
                    TimeOfMostRecentActivation = SelectedAlarm.TimeOfMostRecentActivation,
                    MissedTriggers = SelectedAlarm.MissedTriggers
                };

                BusinessLayer.Model.ActionsModelType action = (BusinessLayer.Model.ActionsModelType)((int)Model.ActionTypes.UNCLAIM);
                m_avigilonWebEndpointServiceManager.QueryAvigilonWebEndpointForUpdatingAlarmStatus(selectedAlarm, string.Empty, action);
                LoadAllAlarms();
                UnassignButtonVisibility = Visibility.Collapsed;
                AssignButtonVisibility = Visibility.Visible;
                IsAssignEnabled = true;
            }
            catch (Exception ex)
            {
                m_avigilonAlarmLog.Error(ex, ex.Message);
            }
        }
        private bool CanUnassignCommand(object obj)
        {
            return true;
        }


        /// <summary>
        /// Method to Logout
        /// </summary>
        /// <param name="selectedUser"></param>
        private void Logout(object selectedUser)
        {
            try
            {                
                dispatcherTimer.Stop();
                if (m_avigilonWebEndpointServiceManager.IsAuthenticated())
                    m_avigilonWebEndpointServiceManager.LogoutFromAvigilonWebEndpoint();
                if (m_dialogueService == null)
                    m_dialogueService = new Services.DialogueService();
                m_dialogueService.CloseAlarmListWndowViewDialog();
                
            }
            catch (Exception ex)
            {
                m_avigilonAlarmLog.Error(ex, ex.Message);
            }

        }

        private bool CanLogoutCommand(object obj)
        {
            return true;
        }

        /// <summary>
        /// Method to Acknowledge Alarm based on Note required
        /// </summary>
        /// <param name="selectedAlarm"></param>

        private void IsNoteRequired(Model.AlarmModel selectedAlarmDetail)
        {
            try
            {
                if (!selectedAlarmDetail.IsNoteRequired && m_isAcknowledged)
                {
                    BusinessLayer.Model.AlarmModel selectedAlarm = new BusinessLayer.Model.AlarmModel
                    {
                        Id = SelectedAlarm.Id,
                        Name = SelectedAlarm.Name,
                        State = SelectedAlarm.State,
                        IsAssignedToCurrentUser = SelectedAlarm.IsAssignedToCurrentUser,
                        AssociatedCameras = SelectedAlarm.AssociatedCameras,
                        IsNoteRequired = SelectedAlarm.IsNoteRequired,
                        Type = SelectedAlarm.Type,
                        TimeOfMostRecentActivation = SelectedAlarm.TimeOfMostRecentActivation,
                        MissedTriggers = SelectedAlarm.MissedTriggers
                    };

                    BusinessLayer.Model.ActionsModelType action = (BusinessLayer.Model.ActionsModelType)((int)Model.ActionTypes.ACKNOWLEDGE);
                    bool isSuccess = m_avigilonWebEndpointServiceManager.QueryAvigilonWebEndpointForUpdatingAlarmStatus(selectedAlarm, string.Empty, action);
                    Utility.Messenger.Default.Send<BusinessLayer.Model.ActionsModelType>(action);
                    if (m_dialogueService == null)
                        m_dialogueService = new Services.DialogueService();
                    if (isSuccess)
                    {
                        m_dialogueService.ShowSuccessWndowViewDialog();
                    }
                    else
                    {
                        m_dialogueService.ShowFailureWndowViewDialog();
                    }
                }
                else
                {
                    Utility.Messenger.Default.Send<Model.AlarmModel>(selectedAlarmDetail);
                    if (m_dialogueService == null)
                        m_dialogueService = new Services.DialogueService();
                    m_dialogueService.ShowCommentWindowViewDialog();
                }
                LoadAllAlarms();
            }

            catch (Exception ex)
            {
                m_avigilonAlarmLog.Error(ex, ex.Message);
            }
        }             
    }
}



