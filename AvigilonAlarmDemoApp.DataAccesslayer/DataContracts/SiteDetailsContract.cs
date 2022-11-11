using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvigilonAlarmDemoApp.DataAccesslayer.DataContracts
{
    /// <summary>
    /// Object to model the get-alarms response from Avigilon Web Endpoint.
    /// </summary>
    [System.Runtime.Serialization.DataContract]
    public class SiteDetailsContract
    {
        [System.Runtime.Serialization.DataMember]
        public string status
        {
            get;
            set;
        }

        [System.Runtime.Serialization.DataMember]
        public SitesListContract result
        {
            get;
            set;
        }
    }


}