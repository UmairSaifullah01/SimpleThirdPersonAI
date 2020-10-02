using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace UMDataManagement
{
    public class BinaryDataSaver : IDataSaver
    {
        /// <summary>
        /// holds data path 
        /// </summary>
        private string persistentDataPath;
        SurrogateSelector surrogateSelector;
        public BinaryDataSaver(string path = "")
        {
            persistentDataPath = string.IsNullOrEmpty(path) ? Application.persistentDataPath : path;
            Vector3SerializationSurrogate vector3SS = new Vector3SerializationSurrogate();
            surrogateSelector=new SurrogateSelector();
            surrogateSelector.AddSurrogate(typeof(Vector3), new StreamingContext(StreamingContextStates.All), vector3SS);
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
                using (var stream = new FileStream($"{persistentDataPath}/{key}.data", FileMode.Open))
                {
                    var formatter = new BinaryFormatter();
                    formatter.SurrogateSelector = surrogateSelector;
                    dataObject = (T) formatter.Deserialize(stream);
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
            return File.Exists($"{persistentDataPath}/{key}.data");
        }

        public void Delete(string key)
        {
            File.Delete($"{persistentDataPath}/{key}.data");
        }

        public T Get<T>(string key)
        {
            using (var stream = new FileStream($"{persistentDataPath}/{key}.data", FileMode.Open))
            {
                var formatter = new BinaryFormatter();
                formatter.SurrogateSelector = surrogateSelector;
                return (T) formatter.Deserialize(stream);
            }
        }

        public void Save<T>(string key, T dataObject)
        {
            using (var stream = new FileStream($"{persistentDataPath}/{key}.data", FileMode.Create))
            {
                var formatter = new BinaryFormatter();
                formatter.SurrogateSelector = surrogateSelector;
                formatter.Serialize(stream, dataObject);
            }
        }
    }


    public class Vector3SerializationSurrogate : ISerializationSurrogate
    {

        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
        {
            Vector3 v3 = (Vector3)obj;
            info.AddValue("x", v3.x);
            info.AddValue("y", v3.y);
            info.AddValue("z", v3.z);
        }

        public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
        {
            Vector3 v3 = (Vector3)obj;
            v3.x = (float)info.GetValue("x", typeof(float));
            v3.y = (float)info.GetValue("y", typeof(float));
            v3.z = (float)info.GetValue("z", typeof(float));
            obj  = v3;
            return obj;
        }

    }
}