using System.Reflection;
using LibCMS.Base;

namespace LibCMS.Database.Relationships;

public abstract class Relationship<TTarget, TRelated>(TTarget target) : EntityManager<TRelated> where TTarget : BaseEntity where TRelated : BaseEntity
{
    protected readonly TTarget Target = target;

    protected TKey? GetEntityKeyValue<TKey, T>(T entity, string foreignKey) where T : BaseEntity
    {
        Type entityType = entity.GetType();

        PropertyInfo propertyInfo = EnsurePropertyPresence(entityType, foreignKey);

        EnsurePropertyValidType<TKey>(propertyInfo, foreignKey);

        return (TKey?)propertyInfo.GetValue(entity);
    }

    protected void SetEntityKeyValue<TKey, T>(T entity, string foreignKey, TKey foreignKeyValue) where T : BaseEntity
    {
        Type entityType = entity.GetType();

        PropertyInfo propertyInfo = EnsurePropertyPresence(entityType, foreignKey);

        EnsurePropertyValidType<TKey>(propertyInfo, foreignKey);

        propertyInfo.SetValue(entity, foreignKeyValue);
    }

    private PropertyInfo EnsurePropertyPresence(Type entityType, string foreignKey)
    {
        PropertyInfo? propertyInfo = entityType.GetProperty(foreignKey);

        if (propertyInfo is not null) return propertyInfo;

        throw new Exception($"""Invalid foreign key "{foreignKey}" defined for "{nameof(TRelated)}" in "{nameof(Target)}"!""");
    }

    private void EnsurePropertyValidType<TKey>(PropertyInfo propertyInfo, string foreignKey)
    {
        if (propertyInfo.PropertyType == typeof(TKey)) return;

        throw new Exception($"""Property "{foreignKey}" in "{nameof(Target)}" is not of type "{nameof(TKey)}"!""");
    }
}
