using NLog;
using System;
using System.Windows;
using System.Windows.Input;

namespace AvigilonAlarmDemoApp.UI.ViewModel
{
    /// <summary>
    /// View model for Commet window to acknowledge the alarms
    /// </summary>
    
    public class CommentWindowViewModel : AvigilonAlarmDemoAppBaseViewModel
    {
        public ICommand OkCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        private string m_comment;
        private Model.AlarmModel SelectedAlarm;
        private Services.DialogueService m_dialogueService;
        private Visibility m_commentErrorVisibility;

        private Logger m_avigilonAlarmLog = Logging.LoggerManager.AvigilonAlarmLogger;

        /// <summary>
        /// Gets and sets the Comment property.
        /// </summary>
        public string Comment
        {
            get { return m_comment; }
            set
            {
                m_comment = value;
                OnPropertyChanged("Comment");
            }
        }

        /// <summary>
        /// gets and sets the Visibility of CommentMissing
        /// </summary>
        public Visibility CommentErrorVisibility
        {
            get
            {
                return m_commentErrorVisibility;
            }
            set
            {
                m_commentErrorVisibility = value;

                OnPropertyChanged("CommentErrorVisibility");
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="avigilonWebEndpointServiceManager"></param>
        public CommentWindowViewModel(BusinessLayer.BusinessLogic.AvigilonWebEndPointServiceBusinessLogic avigilonWebEndpointServiceManager)
            : base(avigilonWebEndpointServiceManager)
        {
            Utility.Messenger.Default.Register<Model.AlarmModel>(this, this.LoadSelectedAlarm);
            OkCommand = new Commands.CustomCommand(AddComment, CanAddComment);
            CancelCommand = new Commands.CustomCommand(CancelCommentWindow, CanCancelCommentWindow);
            CommentErrorVisibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Loads the selected Alarm
        /// </summary>
        /// <param name="_selectedAlarm"></param>
        private void LoadSelectedAlarm(Model.AlarmModel _selectedAlarm)
        {
            SelectedAlarm = _selectedAlarm;
        }

        /// <summary>
        /// Method to AddComment
        /// </summary>
        /// <param name="obj"></param>
        private void AddComment(Object obj)
        {
            try
            {
                if (string.IsNullOrEmpty(Comment))
                {
                    CommentErrorVisibility = Visibility.Visible;
                }
                else
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
                    m_avigilonWebEndpointServiceManager.QueryAvigilonWebEndpointForUpdatingAlarmStatus(selectedAlarm, Comment, action);
                    Utility.Messenger.Default.Send<BusinessLayer.Model.ActionsModelType>(action);
                    if (m_dialogueService == null)
                        m_dialogueService = new Services.DialogueService();
                    m_dialogueService.CloseCommentWindowViewDialog();
                    m_dialogueService.ShowSuccessWndowViewDialog();
                    if (Comment != null)
                    {
                        Comment = string.Empty;
                    }
                    CommentErrorVisibility = Visibility.Collapsed;
                }
            }
            catch (Exception ex)
            {
                m_avigilonAlarmLog.Error(ex, ex.Message);
            }
        }
        private bool CanAddComment(object obj)
        {
            return true;
        }

        /// <summary>
        /// Method to AddComment
        /// </summary>
        /// <param name="obj"></param>
        private void CancelCommentWindow(Object obj)
        {
            if (m_dialogueService == null)
                m_dialogueService = new Services.DialogueService();
            m_dialogueService.CloseCommentWindowViewDialog();
            if (Comment != null)
            {
               Comment = string.Empty;
            }
        }

        private bool CanCancelCommentWindow(object obj)
        {
            return true;
        }
    }
}