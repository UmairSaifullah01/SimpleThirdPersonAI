using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace UMDataManagement
{
	public class DataEntity : MonoBehaviour, IDataEntity
	{

		public string key { get;  }

		public void SaveData(Data data)
		{
			if (DataPersistSystem.Instance != null) DataPersistSystem.Instance.Add(key, data);
		}

		public Data LoadData()
		{
			if (DataPersistSystem.Instance != null)
			{
				return DataPersistSystem.Instance.Get<Data>(key);
			}

			return null;
		}

	}


}