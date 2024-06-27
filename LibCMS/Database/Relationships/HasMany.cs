using LibCMS.Base;

namespace LibCMS.Database.Relationships;

public class HasMany<TTarget, TRelated>(TTarget target, string foreignKey) : Relationship<TTarget, TRelated>(target) where TTarget : BaseEntity where TRelated : BaseEntity
{
    public override (string, List<TRelated>) AccessDataStore()
    {
        (string entityName, List<TRelated> entities) = EntityDataStore.GetEntityStore();

        entities = entities.Where(entity =>
        {
            Guid? foreignKeyValue = GetEntityKeyValue<Guid, TRelated>(entity, foreignKey);

            return foreignKeyValue is not null && Target.Guid == foreignKeyValue;
        }).ToList();

        return (entityName, entities);
    }

    public override TRelated Store(TRelated entity)
    {
        SetEntityKeyValue(entity, foreignKey, Target.Guid);

        return base.Store(entity);
    }
}
