using LibCMS.Base;

namespace LibCMS.Database;

public class DataStore<T> where T : BaseEntity
{
    private static DataStore<T>? _instance;

    private readonly List<T> _dataStore = [];

    private DataStore()
    {
        //
    }

    public (string, List<T>) GetEntityStore()
    {
        return (nameof(T), _dataStore);
    }

    public void AddEntityToStore(T entity)
    {
        _dataStore.Add(entity);
    }

    public void RemoveEntityFromStore(Guid guid)
    {
        _dataStore.RemoveAll(entityToRemove => entityToRemove.Guid == guid);
    }

    public static DataStore<T> GetInstance()
    {
        if (_instance is not null) return _instance;

        _instance = new DataStore<T>();

        return _instance;
    }
}