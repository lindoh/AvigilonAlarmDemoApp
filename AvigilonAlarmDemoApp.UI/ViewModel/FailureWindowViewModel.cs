using System;
using System.Windows;
using System.Windows.Input;

namespace AvigilonAlarmDemoApp.UI.ViewModel
{
    /// <summary>
    /// View model for Failure Window when the alarm acknowledge/purge fails
    /// </summary>
    public class FailureWindowViewModel:AvigilonAlarmDemoAppBaseViewModel
    {
        public ICommand OkCommand { get; set; }
        private Services.DialogueService m_dialogueService;
        private BusinessLayer.Model.ActionsModelType SelectedAction;
        private Visibility m_failedAcknowledgedVisibility = Visibility.Collapsed;
        private Visibility m_failedPurgedVisibility = Visibility.Collapsed;
        private Visibility m_failedVisibility = Visibility.Collapsed;

        /// <summary>
        /// gets and sets the Visibility of FailedAcknowledgedVisibility
        /// </summary>
        public Visibility FailedAcknowledgedVisibility
        {
            get
            {
                return m_failedAcknowledgedVisibility;
            }
            set
            {
                m_failedAcknowledgedVisibility = value;
                OnPropertyChanged("FailedAcknowledgedVisibility");
            }
        }

        /// <summary>
        /// gets and sets the Visibility of FailedPurgedVisibility
        /// </summary>
        public Visibility FailedPurgedVisibility
        {
            get
            {
                return m_failedPurgedVisibility;
            }
            set
            {
                m_failedPurgedVisibility = value;
                OnPropertyChanged("FailedPurgedVisibility");
            }
        }

        public Visibility FailedVisibility
        {
            get
            {
                return m_failedVisibility;
            }
            set
            {
                m_failedVisibility = value;
                OnPropertyChanged("FailedVisibility");
            }
        }

        public FailureWindowViewModel(BusinessLayer.BusinessLogic.AvigilonWebEndPointServiceBusinessLogic avigilonWebEndpointServiceManager)
            : base(avigilonWebEndpointServiceManager)
        {
            Utility.Messenger.Default.Register<BusinessLayer.Model.ActionsModelType>(this, this.LoadAction);
            FailedAcknowledgedVisibility = Visibility.Collapsed;
            FailedPurgedVisibility = Visibility.Collapsed;
            FailedVisibility = Visibility.Collapsed;
            OkCommand = new Commands.CustomCommand(FailedAckPurge, CanFailedAckPurge);
        }

        private void LoadAction(BusinessLayer.Model.ActionsModelType _action)
        {
            SelectedAction = _action;
            if (SelectedAction == (BusinessLayer.Model.ActionsModelType)((int)Model.ActionTypes.ACKNOWLEDGE))
            {
                FailedAcknowledgedVisibility = Visibility.Visible;
                FailedPurgedVisibility = Visibility.Collapsed;
            }
            else if (SelectedAction == (BusinessLayer.Model.ActionsModelType)((int)Model.ActionTypes.PURGE))
            {
                FailedPurgedVisibility = Visibility.Visible;
                FailedAcknowledgedVisibility = Visibility.Collapsed;
            }
            else
            {
                FailedVisibility = Visibility.Visible;
            }
        }

       
        /// <summary>
        /// Method to acknowledge with failure view
        /// </summary>
        /// <param name="obj"></param>
        private void FailedAckPurge(Object obj)
        {
            FailedAcknowledgedVisibility = Visibility.Collapsed;
            FailedPurgedVisibility = Visibility.Collapsed;
            FailedVisibility = Visibility.Collapsed;
            if (m_dialogueService == null)
                m_dialogueService = new Services.DialogueService();
            m_dialogueService.CloseFailureWndowViewDialog();
        }
        private bool CanFailedAckPurge(object obj)
        {
            return true;
        }
    }
}