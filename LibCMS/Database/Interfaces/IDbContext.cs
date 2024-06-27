using LibCMS.Base;

namespace LibCMS.Database.Interfaces;

public interface IDbContext
{
    public IEntityManager<T> Of<T>() where T : BaseEntity;
}