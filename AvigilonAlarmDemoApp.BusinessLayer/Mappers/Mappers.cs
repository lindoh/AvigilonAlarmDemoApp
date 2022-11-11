using System.Collections.Generic;

namespace AvigilonAlarmDemoApp.BusinessLayer.Mappers
{
    /// <summary>
    /// Class to map models to contract or vice versa
    /// </summary>
    public static class Mapper
    {
        /// <summary>
        /// mapping login request model to login request contract
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns></returns>
       public static DataAccesslayer.DataContracts.LoginRequestContract GetLoginContract(
           Model.LoginRequestModel loginRequest)
        {
            DataAccesslayer.DataContracts.LoginRequestContract loginContract = 
                new DataAccesslayer.DataContracts.LoginRequestContract
            {
                clientId = loginRequest.ClientId,
                clientName = loginRequest.ClientName,
                clientVersion = loginRequest.ClientVersion,
                password = loginRequest.Password,
                siteId = loginRequest.SiteId,
                siteName = loginRequest.SiteName,
                username = loginRequest.UserName
            };
            return loginContract;
        }

        /// <summary>
        /// mapping login response cotract to log in response model
        /// </summary>
        /// <param name="loginResponseContract"></param>
        /// <returns></returns>

        public static Model.LoginResponseModel GetLoginModel(
          DataAccesslayer.DataContracts.LoginResponseContract loginResponseContract)
        {
            Model.LoginResponseModel loginResponse =
                new Model.LoginResponseModel
                {
                   Session = loginResponseContract.result.session,
                   DomainId = loginResponseContract.result.domainId,
                   ExternalUserId = loginResponseContract.result.externalUserId
                };
            return loginResponse;
        }

        /// <summary>
        /// mappig Alarmscontract to alarms model
        /// </summary>
        /// <param name="alarmlist"></param>
        /// <returns></returns>
        public static List<Model.AlarmModel> GetAlarmsModel(DataAccesslayer.DataContracts.AlarmsContract alarmlist)
        {
            List<Model.AlarmModel> alarmModel = new List<Model.AlarmModel>();
            foreach (DataAccesslayer.DataContracts.AlarmContract alarm in alarmlist.alarms)
            {
                alarmModel.Add(new Model.AlarmModel
                {
                    Id = alarm.id,
                    Name = alarm.name,
                    State = alarm.state,
                    IsAssignedToCurrentUser = alarm.isAssignedToCurrentUser,
                    IsNoteRequired = alarm.isNoteRequired,
                    TimeOfMostRecentActivation = alarm.timeOfMostRecentActivation,
                    Type = alarm.type,
                    MissedTriggers=alarm.missedTriggers,
                    AssociatedCameras = alarm.associatedCameras
                });
            }
            return alarmModel;
        }

        /// <summary>
        /// mappingAlarm Model to UpdateAlarmRequest Contract
        /// </summary>
        /// <param name="alarmModel"></param>
        /// <param name="comment"></param>
        /// <param name="session"></param>
        /// <returns></returns>

        public static DataAccesslayer.DataContracts.UpdateAlarmRequestContract GetUpdateAlarmRequestContract(
            Model.AlarmModel alarmModel, string comment, string session)
        {
            DataAccesslayer.DataContracts.UpdateAlarmRequestContract alarmsContract = new 
                DataAccesslayer.DataContracts.UpdateAlarmRequestContract
            {
                id = alarmModel.Id,
                note = comment,
                permission = alarmModel.Type,
                session = session
            };
            return alarmsContract;

        }

        /// <summary>
        /// Mapping site details cotract to site model
        /// </summary>
        /// <param name="sitesDetailsContract"></param>
        /// <returns></returns>
        
        public static List<Model.SitesModel> GetSitesContract(DataAccesslayer.DataContracts.SiteDetailsContract sitesDetailsContract)
        {

            List<Model.SitesModel> sitesModel = new List<Model.SitesModel>();
            foreach (DataAccesslayer.DataContracts.SitesContract site in sitesDetailsContract.result.sites)
            {
                sitesModel.Add(new Model.SitesModel
                {
                    Id = site.id,
                    Name = site.name,
                    Reachable = site.reachable
                });
            }
            return sitesModel;

        }

        /// <summary>
        /// Mapping camera details to camera model
        /// </summary>
        /// <param name="cameralist"></param>
        /// <returns></returns>
        public static List<Model.CameraModel> GetCameraModel(DataAccesslayer.DataContracts.CamerasContract cameralist)
        {
            List<Model.CameraModel> cameraModel = new List<Model.CameraModel>();
            foreach (DataAccesslayer.DataContracts.CameraContract alarm in cameralist.cameras)
            {
                cameraModel.Add(new Model.CameraModel
                {
                    Id = alarm.id,
                    Name = alarm.name                   
                });
            }
            return cameraModel;
        }
    }
}
