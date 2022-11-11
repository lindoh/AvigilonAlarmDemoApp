namespace AvigilonAlarmDemoApp.UI.ViewModel
{
    /// <summary>
    /// Base view model for Avigilon Alarm Demo App
    /// </summary>
    public abstract class AvigilonAlarmDemoAppBaseViewModel : System.ComponentModel.INotifyPropertyChanged
    {
        protected BusinessLayer.BusinessLogic.AvigilonWebEndPointServiceBusinessLogic m_avigilonWebEndpointServiceManager;
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="avigilonWebEndpointServiceManager">Avigilon WebEndpoint Service manger to interact with Avigilon WebEndpoint.</param>
        protected AvigilonAlarmDemoAppBaseViewModel(BusinessLayer.BusinessLogic.AvigilonWebEndPointServiceBusinessLogic avigilonWebEndpointServiceManager)
        {
            m_avigilonWebEndpointServiceManager = avigilonWebEndpointServiceManager;
        }
        /// <summary>
        ///  Occurs when a property value changes.
        /// </summary>
        /// <param name="propertyName">The property name in which the value changed.</param>
        protected void OnPropertyChanged(string propertyName)
        {
            System.ComponentModel.PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
        }

    }
}
