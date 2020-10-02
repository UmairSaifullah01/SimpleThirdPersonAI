namespace UMDataManagement
{
    public interface IDataSaver
    {
        bool Contains (string key);
        void Save<T> (string key, T dataObject);
        T Get<T> (string key);
        bool CanGet<T> (string key, out T dataObject);
        void Delete (string key);
    }
}