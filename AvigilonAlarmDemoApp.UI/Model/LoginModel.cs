namespace AvigilonAlarmDemoApp.UI.Model
{
    /// <summary>
    /// Object to model the login request to Avigilon Web Endpoint.
    /// </summary>  
    public class LoginModel : System.ComponentModel.INotifyPropertyChanged
    {       
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        string m_userName = string.Empty;
        string m_password = string.Empty;
        string m_siteName = string.Empty;
        public string Username
        {
            get
            {
                return m_userName;
            }
            set
            {
                m_userName = value;
                OnPropertyChanged("Username");
            }
        }
       
        public string Password
        {
            get
            {
                return m_password;
            }
            set
            {
                m_password = value;
                OnPropertyChanged("Password");
            }
        }

        public string SiteId
        {
            get;
            set;
        }
       
        public string SiteName
        {
            get
            {
                return m_siteName;
            }
            set
            {
                m_siteName = value;
                OnPropertyChanged("SiteName");
            }
        }
      
        public string ClientName
        {
            get;
            set;
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

