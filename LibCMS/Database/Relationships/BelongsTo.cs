using LibCMS.Base;

namespace LibCMS.Database.Relationships;

public class BelongsTo<TTarget, TRelated>(TTarget target, string foreignKey) : Relationship<TTarget, TRelated>(target) where TTarget : BaseEntity where TRelated : BaseEntity
{
    public override (string, List<TRelated>) AccessDataStore()
    {
        Guid? foreignKeyValue = GetEntityKeyValue<Guid, TTarget>(Target, foreignKey);

        (string entityName, List<TRelated> entities) = EntityDataStore.GetEntityStore();

        entities = entities.Where(entity => foreignKeyValue is not null && entity.Guid == foreignKeyValue).ToList();

        return (entityName, entities);
    }
}
