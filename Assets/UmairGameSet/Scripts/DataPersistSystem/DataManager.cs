using System.Collections.Generic;
using System.Linq;
using Logger = UMGS.Log.Logger;


namespace UMDataManagement
{


	public static class DataManager
	{

		private static bool                       isInitialized;
		private static Dictionary<string, object> data = new Dictionary<string, object>();
		private static IDataSaver                 dataSaver;
		static         string                     key = "gamedata";

		/// <summary>
		/// initialize data system
		/// </summary>
		static void Initialize()
		{
			Logger.Log("Data Initialized with BinaryDataSaver");
			dataSaver    = new BinaryDataSaver();
			isInitialized = true;
			Load();
		}

		/// <summary>
		/// initialize data system with desired IDataSaver type data saver
		/// </summary>
		/// <param name="dataSaver"></param>
		public static void Initialize(IDataSaver dataSaver)
		{
			DataManager.dataSaver    = dataSaver;
			isInitialized = true;
		}

		/// <summary>
		/// CanGet is used for get data if data exists
		/// </summary>
		/// <param name="key">data saved as named</param>
		/// <param name="dataObject">carries data</param>
		/// <typeparam name="T">type of data</typeparam>
		/// <returns>Return true if it finds data</returns>
		public static bool CanGet<T>(string key, out T dataObject)
		{
			if (!isInitialized)
				Initialize();
			if (data.ContainsKey(key))
			{
				dataObject = (T) data[key];
				return true;
			}
			else
			{
				dataObject = default(T);
				return false;
			}
		}

		/// <summary>
		/// Contains used to check is data exist with name of key
		/// </summary>
		/// <param name="key">data saved as named</param>
		/// <returns></returns>
		public static bool Contains(string key)
		{
			if (!isInitialized)
				Initialize();
			return data.ContainsKey(key);
		}

		/// <summary>
		/// Delete saved data by key(data saved name)
		/// </summary>
		/// <param name="key"></param>
		public static void Delete(string key)
		{
			if (!isInitialized)
				Initialize();
			data.Remove(key);
		}

		/// <summary>
		/// Used to get data by key
		/// </summary>
		/// <param name="key"></param>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static T Get<T>(string key)
		{
			if (!isInitialized)
				Initialize();
			if (data.ContainsKey(key))
				return (T) data[key];
			else return default(T);
		}

		/// <summary>
		/// Used to saved data by key 
		/// </summary>
		/// <param name="key"></param>
		/// <param name="dataObject"></param>
		/// <typeparam name="T"></typeparam>
		public static void Save<T>(string key, T dataObject)
		{
			if (!isInitialized)
				Initialize();
			if (data.ContainsKey(key))
			{
				data[key] = dataObject;
			}
			else
			{
				data.Add(key, dataObject);
			}
		}

		public static void SaveToFile()
		{
			var serializedData = new List<Entry>(data.Count);
			serializedData.AddRange(data.Keys.Select(key => new Entry(key, data[key])));
			dataSaver.Save(key, serializedData);
		}

		public static void Load()
		{
			if (!dataSaver.Contains(key))
				return;
			var serializedData = dataSaver.Get<List<Entry>>(key);
			foreach (Entry entry in serializedData)
			{
				data[entry.Key] = entry.Value;
			}
		}

	}

	[System.Serializable]
	public class Entry
	{

		public string Key;
		public object Value;

		public Entry()
		{
		}

		public Entry(string key, object value)
		{
			Key   = key;
			Value = value;
		}

	}


}