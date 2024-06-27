using LibCMS.Base;

namespace LibCMS.Database.Interfaces;

public interface IEntityManager<T> where T : BaseEntity
{
    (string, List<T>) AccessDataStore();

    T Store(T entity);

    void Delete(Guid guid);

    T Update(Guid guid, Func<T, T> updateDelegate);

    T GetOne(Guid guid);

    List<T> Get();
}