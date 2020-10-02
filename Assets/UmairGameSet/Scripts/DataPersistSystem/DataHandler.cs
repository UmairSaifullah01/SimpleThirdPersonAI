using System;

namespace UMDataManagement
{
    public static class DataHandler
    {
        private static bool isInitialized;
        private static IDataSaver sdataSaver;
        /// <summary>
        /// initialize data system
        /// </summary>
        public static void Initialize ()
        {
            sdataSaver = new XmlDataSaver ();
            isInitialized = true;
        }
        /// <summary>
        /// initialize data system with desired IDataSaver type data saver
        /// </summary>
        /// <param name="dataSaver"></param>
        public static void Initialize (IDataSaver dataSaver)
        {
            sdataSaver = dataSaver;
            isInitialized = true;
        }
        /// <summary>
        /// CanGet is used for get data if data exists
        /// </summary>
        /// <param name="key">data saved as named</param>
        /// <param name="dataObject">carries data</param>
        /// <typeparam name="T">type of data</typeparam>
        /// <returns>Return true if it finds data</returns>
        public static bool CanGet<T> (string key, out T dataObject)
        {
            if (!isInitialized)
                Initialize ();
            return sdataSaver.CanGet (key, out dataObject);
        }
        /// <summary>
        /// Contains used to check is data exist with name of key
        /// </summary>
        /// <param name="key">data saved as named</param>
        /// <returns></returns>
        public static bool Contains (string key)
        {
            if (!isInitialized)
                Initialize ();
            return sdataSaver.Contains (key);
        }
        /// <summary>
        /// Delete saved data by key(data saved name)
        /// </summary>
        /// <param name="key"></param>
        public static void Delete (string key)
        {
            if (!isInitialized)
                Initialize ();
            sdataSaver.Delete (key);
        }
        /// <summary>
        /// Used to get data by key
        /// </summary>
        /// <param name="key"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Get<T> (string key)
        {
            if (!isInitialized)
                Initialize ();
            return sdataSaver.Get<T> (key);
        }
        /// <summary>
        /// Used to saved data by key 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataObject"></param>
        /// <typeparam name="T"></typeparam>
        public static void Save<T> (string key, T dataObject)
        {
            if (!isInitialized)
                Initialize ();
            sdataSaver.Save (key, dataObject);
        }
    }

    
}