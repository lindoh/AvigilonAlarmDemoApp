using System.Collections.Generic;

namespace AvigilonAlarmDemoApp.DataAccesslayer.DataContracts
{
    /// <summary>
    /// Object to model the result from get-camera response from Avigilon Web Endpoint.
    /// </summary>
    [System.Runtime.Serialization.DataContract]
    public class CamerasContract
    {
        [System.Runtime.Serialization.DataMember]
        public List<CameraContract> cameras
        {
            get;
            set;
        }
    }

    /// <summary>
    /// Object to model the camera details response from Avigilon Web Endpoint.
    /// </summary>
    [System.Runtime.Serialization.DataContract]
    public class CameraContract
    {
        [System.Runtime.Serialization.DataMember]
        public string id { get; set; }
        [System.Runtime.Serialization.DataMember]
        public string name { get; set; }
        [System.Runtime.Serialization.DataMember]
        public string model { get; set; }
        [System.Runtime.Serialization.DataMember]
        public string serial { get; set; }
        [System.Runtime.Serialization.DataMember]
        public string manufacturer { get; set; }
        [System.Runtime.Serialization.DataMember]
        public bool connected { get; set; }
        [System.Runtime.Serialization.DataMember]
        public bool active { get; set; }
        [System.Runtime.Serialization.DataMember]
        public string connectionState { get; set; }
        [System.Runtime.Serialization.DataMember]
        public int firmwareVersion { get; set; }
        [System.Runtime.Serialization.DataMember]
        public int operatingPriority { get; set; }
        [System.Runtime.Serialization.DataMember]
        public string physicalAddress { get; set; }
        [System.Runtime.Serialization.DataMember]
        public string ipAddress { get; set; }
        [System.Runtime.Serialization.DataMember]
        public object logicalId { get; set; }
        [System.Runtime.Serialization.DataMember]
        public Capabilities capabilities { get; set; }
        [System.Runtime.Serialization.DataMember]
        public bool recordedData { get; set; }
        [System.Runtime.Serialization.DataMember]
        public int defaultWidth { get; set; }
        [System.Runtime.Serialization.DataMember]
        public int defaultHeight { get; set; }
        [System.Runtime.Serialization.DataMember]
        public string location { get; set; }
    }

    [System.Runtime.Serialization.DataContract]
    public class Capabilities
    {
        [System.Runtime.Serialization.DataMember]
        public List<string> general { get; set; }
        [System.Runtime.Serialization.DataMember]
        public List<string> ptz { get; set; }
        [System.Runtime.Serialization.DataMember]
        public List<string> network { get; set; }
        [System.Runtime.Serialization.DataMember]
        public List<string> compression { get; set; }
        [System.Runtime.Serialization.DataMember]
        public List<string> mjpeg { get; set; }
        [System.Runtime.Serialization.DataMember]
        public List<string> h264 { get; set; }
        [System.Runtime.Serialization.DataMember]
        public List<string> acquisition { get; set; }
        [System.Runtime.Serialization.DataMember]
        public List<string> motion { get; set; }
        [System.Runtime.Serialization.DataMember]
        public List<string> digitalIo { get; set; }
        [System.Runtime.Serialization.DataMember]
        public List<string> audio { get; set; }
        [System.Runtime.Serialization.DataMember]
        public List<string> speaker { get; set; }
        [System.Runtime.Serialization.DataMember]
        public List<string> exposure { get; set; }
        [System.Runtime.Serialization.DataMember]
        public List<string> streamRecording { get; set; }
        [System.Runtime.Serialization.DataMember]
        public List<string> profileRecording { get; set; }
        [System.Runtime.Serialization.DataMember]
        public List<string> analytic { get; set; }
    }
}