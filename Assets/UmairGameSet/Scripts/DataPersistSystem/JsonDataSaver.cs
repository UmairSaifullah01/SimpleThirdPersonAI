using System.IO;
using UMDataManagement;
using UnityEngine;


namespace UMDataManagement
{


	public class JsonDataSaver : IDataSaver
	{

		private string persistentDataPath;
		private string jsonData;

		public JsonDataSaver(string path = "")
		{
			persistentDataPath = string.IsNullOrEmpty(path) ? Application.persistentDataPath : path;
		}

		public bool Contains(string key)
		{
			return File.Exists($"{persistentDataPath}/{key}.json");
		}

		public void Save<T>(string key, T dataObject)
		{
			string dataString = JsonUtility.ToJson(dataObject);
			var    stream     = new StreamWriter($"{persistentDataPath}/{key}.json");
			stream.Write(dataString);
			stream.Close();
		}

		public T Get<T>(string key)
		{
			var    stream = File.OpenText($"{persistentDataPath}/{key}.json");
			string data   = stream.ReadToEnd();
			stream.Close();
			return JsonUtility.FromJson<T>(data);
		}

		public bool CanGet<T>(string key, out T dataObject)
		{
			if (Contains(key))
			{
				var    stream = File.OpenText($"{persistentDataPath}/{key}.json");
				string data   = stream.ReadToEnd();
				stream.Close();
				dataObject = JsonUtility.FromJson<T>(data);
				return true;
			}
			else
			{
				dataObject = default(T);
				return false;
			}
		}

		public void Delete(string key)
		{
			File.Delete($"{persistentDataPath}/{key}.data");
		}

	}


}