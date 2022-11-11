using System;
using System.Windows;
using System.Windows.Input;

namespace AvigilonAlarmDemoApp.UI.ViewModel
{
    /// <summary>
    /// View model for Failure Window when the alarm acknowledge/purge is a success
    /// </summary>
    public class SuccessWindowViewModel : AvigilonAlarmDemoAppBaseViewModel
    {
        public ICommand OkCommand { get; set; }
        private Services.DialogueService m_dialogueService;
        private BusinessLayer.Model.ActionsModelType SelectedAction;
        private Visibility m_succesfullyAcknowledgedVisibility = Visibility.Collapsed;
        private Visibility m_succesfullyPurgedVisibility = Visibility.Collapsed;
        private Visibility m_succesfulVisibility = Visibility.Collapsed;

        public SuccessWindowViewModel(BusinessLayer.BusinessLogic.AvigilonWebEndPointServiceBusinessLogic avigilonWebEndpointServiceManager)
            : base(avigilonWebEndpointServiceManager)
        {            
            Utility.Messenger.Default.Register<BusinessLayer.Model.ActionsModelType>(this, this.LoadAction);
            SuccesfullyAcknowledgedVisibility = Visibility.Collapsed;
            SuccesfullyPurgedVisibility = Visibility.Collapsed;
            SuccesfulVisibility = Visibility.Collapsed;
            OkCommand = new Commands.CustomCommand(AckPurgeCommand, CanackPurgeCommand);
        }      

        /// <summary>
        /// Loads the selected Alarm
        /// </summary>
        /// <param name="_selectedAlarm"></param>
        private void LoadAction(BusinessLayer.Model.ActionsModelType _action)
        {
            SelectedAction = _action;
            if (SelectedAction == (BusinessLayer.Model.ActionsModelType)((int)Model.ActionTypes.ACKNOWLEDGE))
            {
                SuccesfullyAcknowledgedVisibility = Visibility.Visible;
                SuccesfullyPurgedVisibility = Visibility.Collapsed;
            }
            else if (SelectedAction == (BusinessLayer.Model.ActionsModelType)((int)Model.ActionTypes.PURGE))
            {
                SuccesfullyPurgedVisibility = Visibility.Visible;
                SuccesfullyAcknowledgedVisibility = Visibility.Collapsed;
            }
            else
            {
                SuccesfulVisibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// gets and sets the Visibility of CredentialMissingLabel
        /// </summary>
        public Visibility SuccesfullyAcknowledgedVisibility
        {
            get
            {
                return m_succesfullyAcknowledgedVisibility;
            }
            set
            {
                m_succesfullyAcknowledgedVisibility = value;
                OnPropertyChanged("SuccesfullyAcknowledgedVisibility");
            }
        }

           /// <summary>
        /// gets and sets the Visibility of SuccesfullyPurgedVisibility
        /// </summary>
        public Visibility SuccesfullyPurgedVisibility
        {
            get
            {
                return m_succesfullyPurgedVisibility;
            }
            set
            {
                m_succesfullyPurgedVisibility = value;
                OnPropertyChanged("SuccesfullyPurgedVisibility");
            }
        }

        public Visibility SuccesfulVisibility
        {
            get
            {
                return m_succesfulVisibility;
            }
            set
            {
                m_succesfulVisibility = value;
                OnPropertyChanged("SuccesfulVisibility");
            }
        }
        
       
        /// <summary>
        /// Method to acknowledge the success view
        /// </summary>
        /// <param name="obj"></param>
        private void AckPurgeCommand(Object obj)
        {
            SuccesfullyAcknowledgedVisibility = Visibility.Collapsed;
            SuccesfullyPurgedVisibility = Visibility.Collapsed;
            SuccesfulVisibility = Visibility.Collapsed;
            if (m_dialogueService == null)
                m_dialogueService = new Services.DialogueService();
            m_dialogueService.CloseSuccessWndowViewDialog();            
        }

        private bool CanackPurgeCommand(object obj)
        {
            return true;
        }
    }
}
