using System.Collections.Generic;

namespace AvigilonAlarmDemoApp.BusinessLayer.Model
{
    /// <summary>
    /// Object to model the get-alarms response from Avigilon Web Endpoint.
    /// </summary>

    public class AlarmResponseModel
    {
        
        public string Status
        {
            get;
            set;
        }

        
        public List<AlarmModel> Result
        {
            get;
            set;
        }

        public bool IsValid()
        {
            return (Status != null &&
                    Status == AvigilonAlarmDemoAppBusinessLayer.Success);
        }
    }
}