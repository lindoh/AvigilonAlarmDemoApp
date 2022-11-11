namespace AvigilonAlarmDemoApp.DataAccesslayer
{
    /// <summary>
    /// Class to seraialize and deserialize json objects
    /// </summary>
    public class JsonSerializationHelper
    {
        private const string k_errorMessage = "Error!";

        /// <summary>
        /// Serialize the given object to json string.
        /// </summary>
        /// <param name="obj">The object to serialize.</param>
        /// <returns>Serialize json string.</returns>
        public static string SeralizeObjectToJson(System.Object obj)
        {
            if (obj != null)
            {
                try
                {                  
                    System.Runtime.Serialization.Json.DataContractJsonSerializer jsonSerializers =
                        new System.Runtime.Serialization.Json.DataContractJsonSerializer(obj.GetType());
                    System.IO.MemoryStream memoryStreams = new System.IO.MemoryStream();
                    jsonSerializers.WriteObject(memoryStreams, obj);
                    return System.Text.Encoding.UTF8.GetString(memoryStreams.ToArray());
                }
                catch (System.Runtime.Serialization.SerializationException serializationException)
                {
                    System.Windows.MessageBox.Show(
                        serializationException.Message,
                        k_errorMessage,
                        System.Windows.MessageBoxButton.OK);
                }
            }
            return System.String.Empty;
        }

        /// <summary>
        /// Deserialize the json string to the Object belonging to "T"
        /// </summary>
        /// <typeparam name="T">The class of the return Object.</typeparam>
        /// <param name="jsonStr">The json string to deserialize.</param>
        /// <returns>Deserialized Object.</returns>
        public static T DeserializeJsonToObject<T>(string jsonStr)
            where T : class
        {
            T deserializedObject = null;
            if (!System.String.IsNullOrEmpty(jsonStr))
            {
                try
                {
                    System.IO.MemoryStream ms = new System.IO.MemoryStream(System.Text.Encoding.Unicode.GetBytes(jsonStr));
                    System.Runtime.Serialization.Json.DataContractJsonSerializer ser =
                        new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(T));
                    deserializedObject = (T)ser.ReadObject(ms);
                    ms.Close();
                }
                catch (System.Runtime.Serialization.SerializationException serializationException)
                {
                    System.Windows.MessageBox.Show(
                        serializationException.Message,
                        k_errorMessage,
                        System.Windows.MessageBoxButton.OK);
                }
                catch (System.Xml.XmlException)
                {
                    /* Ignore. */
                }
            }
            return deserializedObject;
        }
    }
}

