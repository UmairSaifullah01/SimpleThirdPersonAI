using UnityEngine;


namespace UMDataManagement
{


	public class UnityDataSaver : IDataSaver
	{

		public bool Contains(string key)
		{
			return PlayerPrefs.HasKey(key);
		}

		public void Save<T>(string key, T dataObject)
		{
			var dataString = JsonUtility.ToJson(dataObject);
			PlayerPrefs.SetString(key, dataString);
			PlayerPrefs.Save();
		}

		public T Get<T>(string key)
		{
			var dataString = PlayerPrefs.GetString(key);
			return JsonUtility.FromJson<T>(dataString);
		}

		public bool CanGet<T>(string key, out T dataObject)
		{
			if (Contains(key))
			{
				var dataString = PlayerPrefs.GetString(key);
				dataObject = JsonUtility.FromJson<T>(dataString);
				return true;
			}

			dataObject = default(T);
			return false;
		}

		public void Delete(string key)
		{
			PlayerPrefs.DeleteKey(key);
		}

	}


}