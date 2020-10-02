using System.Collections.Generic;
using System.IO;
using System.Linq;
using UMGS;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;


namespace UMDataManagement
{


    public class DataPersistSystem : SingletonPersistent<DataPersistSystem>
    {

        private Dictionary<string, object> allData;
        private IDataSaver sdataSaver;
        static string key = "UjsavedData";
        static bool saved = false;
        protected override void Awake ()
        {
            base.Awake ();
            Initialize (true);
            print (Application.persistentDataPath);
            //     sdataSaver.Delete("UjsavedData");
        }


        public object this[string key] => allData[key];

        public void Initialize (bool autoLoad)
        {
            allData = new Dictionary<string, object> ();
            sdataSaver = new BinaryDataSaver ();
            if (autoLoad)
                Load ();
        }

        public void Initialize (IDataSaver dataSaver, bool autoLoad)
        {
            allData = new Dictionary<string, object> ();
            sdataSaver = dataSaver;
            if (autoLoad)
                Load ();
        }

        public bool CanGet<T> (string key, out T dataObject)
        {
            if (allData.ContainsKey (key))
            {
                dataObject = (T) allData[key];
                return true;
            }
            else
            {
                dataObject = default (T);
                return false;
            }
        }

        public bool Contains (string key)
        {
            return allData.ContainsKey (key);
        }

        public void Delete (string key)
        {
            allData.Remove (key);
        }

        public T Get<T> (string key)
        {
            return (T) allData[key];
        }

        public void Add<T> (string key, T dataObject)
        {
            if (allData.ContainsKey (key))
            {
                allData[key] = dataObject;
            }
            else
            {
                allData.Add (key, dataObject);
            }
        }

        public void Load ()
        {
            if (!sdataSaver.Contains (key))
                return;
            var serializedData = sdataSaver.Get<List<Entry>> (key);
            foreach (Entry entry in serializedData)
            {
                allData[entry.Key] = entry.Value;
            }
        }

        private void OnApplicationQuit ()
        {
            Save ();
        }

        public void Save ()
        {
            if (saved)
                return;
            var serializedData = new List<Entry> (allData.Count);
            serializedData.AddRange (allData.Keys.Select (key => new Entry (key, allData[key])));
            sdataSaver.Save (key, serializedData);
            saved = true;
        }



        void OnDisable ()
        {
            Save ();
        }
#if UNITY_EDITOR
        [MenuItem ("Tools/DataPersistentSystem/DeleteData")]
        public static void DeleteAllData ()
        {
            File.Delete ($"{Application.persistentDataPath}/{key}.data");
        }
#endif

    }


}