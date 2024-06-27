using LibCMS.Base;
using LibCMS.Database.Interfaces;

namespace LibCMS.Database;

public class EntityManager<T> : IEntityManager<T> where T : BaseEntity
{
    protected readonly DataStore<T> EntityDataStore = DataStore<T>.GetInstance();

    public virtual (string, List<T>) AccessDataStore()
    {
        return EntityDataStore.GetEntityStore();
    }

    public virtual T Store(T entity)
    {
        EntityDataStore.AddEntityToStore(entity);

        return entity;
    }

    public virtual void Delete(Guid guid)
    {
        EntityDataStore.RemoveEntityFromStore(guid);
    }

    public virtual T Update(Guid guid, Func<T, T> updateDelegate)
    {
        (string entityName, List<T> entities) = AccessDataStore();

        T? foundEntity = entities.FirstOrDefault(entity => entity.Guid == guid);

        if (foundEntity is null) throw new Exception($"{entityName} entity with identifier {guid} not found");

        return updateDelegate(foundEntity);
    }

    public virtual T GetOne(Guid guid)
    {
        (string entityName, List<T> entities) = AccessDataStore();

        T? foundEntity = entities.FirstOrDefault(entity => entity.Guid == guid);

        if (foundEntity is not null) return foundEntity;

        throw new Exception($"{entityName} entity with guid {guid} not found!");
    }

    public virtual List<T> Get()
    {
        (string _, List<T> entities) = AccessDataStore();

        return entities;
    }
}