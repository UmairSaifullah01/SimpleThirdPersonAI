using System.IO;
using System.Xml.Serialization;
using UnityEngine;

namespace UMDataManagement
{
    /// <summary>
    /// XmlDataSaver = Simple data presister to file type .xml
    /// </summary>
    public class XmlDataSaver : IDataSaver
    {
        /// <summary>
        /// holds data path 
        /// </summary>
        private string persistentDataPath;

        public XmlDataSaver(string path = "")
        {
            persistentDataPath = string.IsNullOrEmpty(path) ? Application.persistentDataPath : path;
        }

        /// <summary>
        /// CanGet is used for get data if data exists
        /// </summary>
        /// <param name="key">data saved as named</param>
        /// <param name="dataObject">carries data</param>
        /// <typeparam name="T">type of data</typeparam>
        /// <returns>Return true if it finds data</returns>
        public bool CanGet<T>(string key, out T dataObject)
        {
            if (Contains(key))
            {
                using (var stream = new FileStream($"{persistentDataPath}/{key}.xml", FileMode.Open))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));

                    dataObject = (T) xmlSerializer.Deserialize(stream);
                    return true;
                }
            }
            else
            {
                dataObject = default(T);
                return false;
            }
        }

        /// <summary>
        /// Contains used to check is data file exist with name of key
        /// </summary>
        /// <param name="key">data saved as named</param>
        /// <returns></returns>
        public bool Contains(string key)
        {
            return File.Exists($"{persistentDataPath}/{key}.xml");
        }

        public void Delete(string key)
        {
            File.Delete($"{persistentDataPath}/{key}.xml");
        }

        public T Get<T>(string key)
        {
            using (var stream = new FileStream($"{persistentDataPath}/{key}.xml", FileMode.Open))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                return (T) xmlSerializer.Deserialize(stream);
            }
        }

        public void Save<T>(string key, T dataObject)
        {
            using (var stream = new FileStream($"{persistentDataPath}/{key}.xml", FileMode.Create))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                xmlSerializer.Serialize(stream, dataObject);
            }
        }
    }
}