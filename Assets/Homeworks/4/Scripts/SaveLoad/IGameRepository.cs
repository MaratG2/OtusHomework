namespace Homeworks.SaveLoad
{
    public interface IGameRepository
    {
        T GetData<T>(string key);
        bool TryGetData<T>(out T value, string key);
        void SetData<T>(T value, string key);
    }
}