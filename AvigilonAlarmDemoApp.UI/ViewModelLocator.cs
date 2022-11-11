using AvigilonAlarmDemoApp.BusinessLayer.BusinessLogic;
using AvigilonAlarmDemoApp.UI.ViewModel;

namespace AvigilonAlarmDemoApp.UI
{
    /// <summary>
    /// View Model Locator
    /// </summary>
    public class ViewModelLocator
    {
        static AvigilonWebEndPointServiceBusinessLogic m_avigilonWebEndpointServiceManager;
        private LogInWindowViewModel logInWindowViewModel;
        private AlarmListWindowViewModel alarmListWindowViewModel;
        private CommentWindowViewModel commentWindowViewModel;
        private SuccessWindowViewModel successWindowViewModel;
        private FailureWindowViewModel failureWindowViewModel;


        public ViewModelLocator()
        {
            if (m_avigilonWebEndpointServiceManager == null)
                m_avigilonWebEndpointServiceManager = new AvigilonWebEndPointServiceBusinessLogic();
        }
        public LogInWindowViewModel LogInWindowViewModel
        {
            get
            {
                return logInWindowViewModel=new LogInWindowViewModel(m_avigilonWebEndpointServiceManager);

            }
            set { logInWindowViewModel = value; }
        }
    

        public AlarmListWindowViewModel AlarmListWindowViewModel
        {
            get
            {
                return alarmListWindowViewModel=new AlarmListWindowViewModel(m_avigilonWebEndpointServiceManager);

            }
        set
        {
            alarmListWindowViewModel = value;
        }
        }

        public CommentWindowViewModel CommentWindowViewModel
        {
            get
            {
                return commentWindowViewModel=new CommentWindowViewModel(m_avigilonWebEndpointServiceManager);

            }
            set
            {
                commentWindowViewModel = value;
            }
        }
        public SuccessWindowViewModel SuccessWindowViewModel
        {
            get
            {
                return successWindowViewModel = new SuccessWindowViewModel(m_avigilonWebEndpointServiceManager);

            }
            set
            {
                successWindowViewModel = value;
            }
        }
        public FailureWindowViewModel FailureWindowViewModel
        {
            get
            {
                return failureWindowViewModel = new FailureWindowViewModel(m_avigilonWebEndpointServiceManager);

            }
            set
            {
                failureWindowViewModel = value;
            }
        }
    }
}