namespace UMDataManagement
{
	public interface IDataEntity
	{

		string key { get;}

		void SaveData(Data data);

		Data LoadData();

	}
}