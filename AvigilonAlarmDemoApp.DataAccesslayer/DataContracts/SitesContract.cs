using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvigilonAlarmDemoApp.DataAccesslayer.DataContracts
{
    /// <summary>
    /// Object to model the result from get-sites response from Avigilon Web Endpoint.
    /// </summary>
    [System.Runtime.Serialization.DataContract]
    public class SitesListContract
    {
        [System.Runtime.Serialization.DataMember]
        public System.Collections.Generic.List<SitesContract> sites
        {
            get;
            set;
        }
    }
    /// <summary>
    /// Object to model the alarm details response from Avigilon Web Endpoint.
    /// </summary>
    [System.Runtime.Serialization.DataContract]
    public class SitesContract
    {
        [System.Runtime.Serialization.DataMember]
        public string id
        {
            get;
            set;
        }

        [System.Runtime.Serialization.DataMember]
        public string name
        {
            get;
            set;
        }


        [System.Runtime.Serialization.DataMember]
        public string reachable
        {
            get;
            set;
        }
        
    }
}