using LibCMS.Base;
using LibCMS.Database.Interfaces;

namespace LibCMS.Database;

public class DbContext : IDbContext
{
    public IEntityManager<T> Of<T>() where T : BaseEntity
    {
        return new EntityManager<T>();
    }
}