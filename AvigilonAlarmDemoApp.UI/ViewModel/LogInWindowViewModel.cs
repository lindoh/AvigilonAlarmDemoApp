using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace AvigilonAlarmDemoApp.UI.ViewModel
{
    /// <summary>
    /// View model for Log in Window
    /// </summary>
    public class LogInWindowViewModel : AvigilonAlarmDemoAppBaseViewModel
    {
        private string m_hostNameOrIpAddr = AvigilonAlarmDemoAppResource.HostNameorIpAddress;
        private Model.LoginModel m_newUser;
        private ObservableCollection<Model.SiteModel> m_sites;
        private Model.SiteModel m_selectedSite;

        public ICommand LogInCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        private Services.DialogueService m_dialogueService;
        private Visibility m_credentialMissingVisibility;
        private Visibility m_siteNameMissingVisibility;
        private Visibility m_invalidUserVisibility;

        /// <summary>
        /// Constructor
        /// </summary>
        public LogInWindowViewModel(BusinessLayer.BusinessLogic.AvigilonWebEndPointServiceBusinessLogic avigilonWebEndpointServiceManager)
            : base(avigilonWebEndpointServiceManager)
        {

            m_hostNameOrIpAddr = string.Format(m_hostNameOrIpAddr, System.Net.Dns.GetHostName());
            m_avigilonWebEndpointServiceManager.SetBaseUrl(m_hostNameOrIpAddr);

            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(GetAllSites);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
            //  dispatcherTimer

            NewUser = new Model.LoginModel();
            LogInCommand = new Commands.CustomCommand(LogIn, CanLogInCommand);
            CancelCommand = new Commands.CustomCommand(Cancel, CanCancelCommand);
            CredentialMissingVisibility = Visibility.Collapsed;
            SiteNameMissingVisibility = Visibility.Collapsed;
            InvalidUserVisibility = Visibility.Collapsed;

        }

        /// <summary>
        /// Event to Get all Avigilon Sites
        /// </summary>
        private void GetAllSites(object sender, EventArgs e)
        {
            var selectedsite = SelectedSite;
            var sites = m_avigilonWebEndpointServiceManager.ReadSitesFromAvigilonWebEndPoint();
            var siteModel = new List<Model.SiteModel>();
            foreach (var site in sites)
            {
                siteModel.Add(new Model.SiteModel
                {
                    Name = site.Name

                });
            }
            Sites = new ObservableCollection<Model.SiteModel>(siteModel);
            if (selectedsite != null)
                SelectedSite = Sites.FirstOrDefault(s => s != null && s.Name == selectedsite.Name);
        }


        /// <summary>
        /// gets and sets the HostName or IpAddress
        /// </summary>
        public string HostnameOrIpAddr
        {
            get { return m_hostNameOrIpAddr; }
            set
            {
                if (!string.Equals(m_hostNameOrIpAddr, value))
                {
                    m_hostNameOrIpAddr = value;
                    OnPropertyChanged("HostnameOrIpAddr");
                };
            }
        }

        /// <summary>
        /// gets and sets the Visibility of CredentialMissingLabel
        /// </summary>
        public Visibility CredentialMissingVisibility
        {
            get
            {
                return m_credentialMissingVisibility;
            }
            set
            {
                m_credentialMissingVisibility = value;
                OnPropertyChanged("CredentialMissingVisibility");
            }
        }

        /// <summary>
        /// gets and sets the Visibility of SiteNameMissingLabel
        /// </summary>
        public Visibility SiteNameMissingVisibility
        {
            get
            {
                return m_siteNameMissingVisibility;
            }
            set
            {
                m_siteNameMissingVisibility = value;
                OnPropertyChanged("SiteNameMissingVisibility");
            }
        }

        /// <summary>
        /// gets and sets the Visibility of SiteNameMissingLabel
        /// </summary>
        public Visibility InvalidUserVisibility
        {
            get
            {
                return m_invalidUserVisibility;
            }
            set
            {
                m_invalidUserVisibility = value;
                OnPropertyChanged("InvalidUserVisibility");
            }
        }
        /// <summary>
        /// Gets and sets the New User
        /// </summary>
        public Model.LoginModel NewUser
        {
            get
            {
                return m_newUser;
            }
            set
            {
                m_newUser = value;
                OnPropertyChanged("NewUser");
            }
        }

        /// <summary>
        /// Gets and sets the SiteName
        /// </summary>
        public ObservableCollection<Model.SiteModel> Sites
        {
            get { return m_sites; }
            set
            {

                if (m_sites == null || !m_sites.Equals(value))
                {
                    m_sites = value;
                    OnPropertyChanged("Sites");
                };
            }
        }

        /// <summary>
        /// gets and sets the Selected site
        /// </summary>
        public Model.SiteModel SelectedSite
        {
            get
            {
                return m_selectedSite;
            }
            set
            {
                m_selectedSite = value;
                OnPropertyChanged("SelectedSite");
            }
        }

        /// <summary>
        /// Method to LogIn
        /// </summary>
        /// <param name="_newuser"></param>
        private void LogIn(object _newuser)
        {
            Model.LoginModel logInRequestDetails = (Model.LoginModel)_newuser;
            if (string.IsNullOrEmpty(logInRequestDetails.Password) || string.IsNullOrEmpty(logInRequestDetails.Username))
            {
                CredentialMissingVisibility = Visibility.Visible;
            }
            else if (SelectedSite == null)
            {
                SiteNameMissingVisibility = Visibility.Visible;
            }
            else
            {
                logInRequestDetails.ClientName = System.Diagnostics.Process.GetCurrentProcess().ProcessName.Replace(".vshost", "");
                BusinessLayer.Model.LoginRequestModel loginModel = new BusinessLayer.Model.LoginRequestModel
                {
                    UserName = logInRequestDetails.Username,
                    Password = logInRequestDetails.Password,
                    ClientName = logInRequestDetails.ClientName,
                    SiteName = SelectedSite.Name
                };
                bool isValid = m_avigilonWebEndpointServiceManager.LoginToAvigilonWebEndpoint(loginModel);

                CredentialMissingVisibility = Visibility.Collapsed;
                SiteNameMissingVisibility = Visibility.Collapsed;
                InvalidUserVisibility = Visibility.Collapsed;
                //if user successfully logged into site then close login window and show alarm window
                if (isValid)
                {
                    if (m_dialogueService != null)
                    {
                        m_dialogueService.Dispose();
                        m_dialogueService = null;
                    }

                    m_dialogueService = new Services.DialogueService();                   
                    m_dialogueService.ShowAlarmListWndowViewDialog();


                }
                else
                {
                    InvalidUserVisibility = Visibility.Visible;
                }
            }
        }

        private bool CanLogInCommand(object obj)
        {
            return true;
        }


        /// <summary>
        /// Method to Cancel
        /// </summary>
        /// <param name="_newuser"></param>
        private void Cancel(object _newuser)
        {

            if (NewUser != null)
            {
                NewUser.Username = string.Empty;
                NewUser.Password = string.Empty;
            }
        }

        private bool CanCancelCommand(object obj)
        {
            return true;
        }
    }
}