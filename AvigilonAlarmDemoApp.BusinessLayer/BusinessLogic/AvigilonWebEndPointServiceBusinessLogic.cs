using System.Collections.Generic;
using NLog;
using System;
using System.Linq;

namespace AvigilonAlarmDemoApp.BusinessLayer.BusinessLogic
{
    /// <summary>
    /// Class containing the Busiess logic for the Alarm Demo App Management
    /// </summary>
    public class AvigilonWebEndPointServiceBusinessLogic
    {
        private DataAccesslayer.Manager.AvigilonWebEndpointServiceManager m_avigilonWebEndpointServiceManager;
        private string m_session;
        private List<Model.AlarmModel> m_alarms;
        private List<Model.CameraModel> m_cameras;
        private Logger m_avigilonAlarmLog = Logging.LoggerManager.AvigilonAlarmLogger;

        /// <summary>
        /// constructor
        /// </summary>
        public AvigilonWebEndPointServiceBusinessLogic()
        {
            if (m_avigilonWebEndpointServiceManager == null)
                m_avigilonWebEndpointServiceManager = new DataAccesslayer.Manager.AvigilonWebEndpointServiceManager();
        }

        /// <summary>
        /// Sets the base URL to the Avigilon Web Endpoint.
        /// </summary>
        /// <param name="baseUrl">The URL to reach Avigilon Web Endpoint.</param>
        public void SetBaseUrl(string baseUrl)
        {
            m_avigilonAlarmLog.Info(new System.Exception("Message"), AvigilonAlarmDemoAppBusinessLayer.BaseURLSetUpLog, baseUrl);           
            try
            {
                m_avigilonWebEndpointServiceManager.SetBaseUrl(baseUrl);
            }
            catch (Exception ex)
            {
                m_avigilonAlarmLog.Error(ex, ex.Message);
            }

        }

        /// <summary>
        /// Determine whether or not we are logged in to Avigilon Web Endpoint and has obtained valid session ID.
        /// </summary>
        /// <returns>True if we are authenticated, false otherwise.</returns>
        public bool IsAuthenticated()
        {         
            
                return (m_session != null);
           
        }

        /// <summary>
        /// Gets the session ID.
        /// </summary>
        /// <returns>The valid session ID if we are authenticated, empty string otherwise.</returns>
        public string GetLoginSessionId()
        {
            m_avigilonAlarmLog.Info(new System.Exception("Message"), AvigilonAlarmDemoAppBusinessLayer.ValidSessionIdLog);
            try
            {

                if (!IsAuthenticated())
                    return string.Empty;
                return m_session;
            }        
            catch(Exception ex)
            {
                m_avigilonAlarmLog.Error(ex, ex.Message);
                return ex.Message;
            }
}
        /// <summary>
        /// Reading all sites
        /// </summary>
        /// <returns></returns>
        public List<Model.SitesModel> ReadSitesFromAvigilonWebEndPoint()
        {
            m_avigilonAlarmLog.Info(new System.Exception("Message"), AvigilonAlarmDemoAppBusinessLayer.ReadSitesLog);
            try
            {
                DataAccesslayer.DataContracts.SiteDetailsContract sitesContract = m_avigilonWebEndpointServiceManager.ReadSitesFromAvigilonWebEndPoint();
                List<Model.SitesModel> sitesModel = Mappers.Mapper.GetSitesContract(sitesContract);
                return sitesModel;
            }
            catch(Exception ex)
            {
                m_avigilonAlarmLog.Error(ex, ex.Message);
                return new List<Model.SitesModel>();
            }

        }
        /// <summary>
        /// Return resume from querying the list of alarms.
        /// </summary>
        /// <returns>List of Alarms.</returns>
        public List<Model.AlarmModel> GetAlarms()
        {
            return m_alarms;
        }

        /// <summary>
        /// Log into the Avigilon Web Endpoint.
        /// </summary>
        /// <param name="loginRequest">The parameter required to log into Avigilon Web Endpoint.</param>
        /// <returns>Boolean value to indicate whether or not log in was successful.</returns>
        public bool LoginToAvigilonWebEndpoint(Model.LoginRequestModel loginRequest)
        {
            m_avigilonAlarmLog.Info(new System.Exception("Message"), AvigilonAlarmDemoAppBusinessLayer.LoggInLog,loginRequest.SiteName,loginRequest.UserName);
            try
            {
                DataAccesslayer.DataContracts.LoginRequestContract loginContract = Mappers.Mapper.GetLoginContract(loginRequest);
                DataAccesslayer.DataContracts.LoginResponseContract loginResponseContract = m_avigilonWebEndpointServiceManager.LoginToAvigilonWebEndpoint(loginContract);
                if (loginResponseContract != null)
                {
                    m_session = loginResponseContract.result.session;
                }
                return (loginResponseContract != null);
            }
            catch (Exception ex)
            {
                m_avigilonAlarmLog.Error(ex, ex.Message);
                return false; ;
            }

        }

        /// <summary>
        /// Query the Avigilon Web Endpoint with the log in session ID for alarms
        /// </summary>
        /// <returns>True for successful query, false otherwise.</returns>
        public bool QueryAvigilonWebEndpointForAlarms()
        {
            m_avigilonAlarmLog.Info(new System.Exception("Message"), AvigilonAlarmDemoAppBusinessLayer.QueryAlarmsLog);
            try
            {
                bool isValid = false;
                if (!string.IsNullOrEmpty(m_session))
                {
                    DataAccesslayer.DataContracts.AlarmResponseContract alarmContract = m_avigilonWebEndpointServiceManager.QueryAvigilonWebEndpointForAlarms(m_session);
                    DataAccesslayer.DataContracts.CameraResponseContract cameraContract = m_avigilonWebEndpointServiceManager.QueryAvigilonWebEndpointForCameras(m_session);
                    m_cameras = Mappers.Mapper.GetCameraModel(cameraContract.result);
                    isValid = (alarmContract != null);
                    if (alarmContract != null)
                    {
                        m_alarms = Mappers.Mapper.GetAlarmsModel(alarmContract.result);
                    }
                    if (m_alarms != null && m_cameras != null)
                    {
                        foreach (Model.AlarmModel alarm in m_alarms)
                        {
                            if (alarm != null && alarm.AssociatedCameras != null)
                            {
                                foreach (string camId in alarm.AssociatedCameras)
                                {
                                    Model.CameraModel matchedCam = m_cameras.FirstOrDefault(x => string.Compare(x.Id, camId, StringComparison.OrdinalIgnoreCase) == 0);
                                    if (alarm.Cameras == null)
                                        alarm.Cameras = new List<string>();

                                    if (matchedCam != null)
                                        alarm.Cameras.Add(matchedCam.Name);
                                    else
                                        alarm.Cameras.Add("Not Connected");
                                }
                            }
                        }
                    }
                }
                return isValid;
            }
            catch(Exception ex)
            {
                m_avigilonAlarmLog.Error(ex, ex.Message);
                return false; ;
            }           
        }

        /// <summary>
        /// Query the Avigilon Web Endpoint for updating alarm status
        /// </summary>
        /// <param name="selectedAlarm"></param>
        /// <param name="comment"></param>
        /// <param name="action"></param>
        /// <returns>alarmContract</returns>
        public bool QueryAvigilonWebEndpointForUpdatingAlarmStatus(Model.AlarmModel selectedAlarm,
            string comment,
            Model.ActionsModelType action)
        {
            
            try
            {
                m_avigilonAlarmLog.Info(new System.Exception("Message"), AvigilonAlarmDemoAppBusinessLayer.QueryUpdateAlarmsLog, selectedAlarm.Name, action);
                if (comment == string.Empty)
                {
                    comment = " ";
                }
                DataAccesslayer.DataContracts.UpdateAlarmRequestContract alarmContract = Mappers.Mapper.GetUpdateAlarmRequestContract(selectedAlarm,
                    comment,
                    m_session);
                switch (action)
                {
                    case Model.ActionsModelType.ACKNOWLEDGE: alarmContract.action = "ACKNOWLEDGE"; break;
                    case Model.ActionsModelType.CLAIM: alarmContract.action = "CLAIM"; break;
                    case Model.ActionsModelType.UNCLAIM: alarmContract.action = "UNCLAIM"; break;
                    case Model.ActionsModelType.PURGE: alarmContract.action = "PURGE"; break;
                    default: alarmContract.action = string.Empty; break;
                }
                return m_avigilonWebEndpointServiceManager.QueryAvigilonWebEndpointForUpdatingAlarmStatus(alarmContract);
               
            }
            catch (Exception ex)
            {
                m_avigilonAlarmLog.Error(ex, ex.Message);
                return false;
            }

        }

        /// <summary>
        /// Log out of the Avigilon Web Endpoint.
        /// </summary>
        /// <returns>Boolean value to indicate whether or not log out was successful.</returns>
        public bool LogoutFromAvigilonWebEndpoint()
        {
            m_avigilonAlarmLog.Info(new System.Exception("Message"), AvigilonAlarmDemoAppBusinessLayer.LogOutLogger);
            try
            {
                if (!IsAuthenticated())
                    return false;
                DataAccesslayer.DataContracts.Session session = new DataAccesslayer.DataContracts.Session()
                {
                    session = m_session
                };
                return m_avigilonWebEndpointServiceManager.LogoutFromAvigilonWebEndpoint(session);
            }
            catch (Exception ex)
            {
                m_avigilonAlarmLog.Error(ex, ex.Message);
                return false; ;
            }
        }
    }
}
