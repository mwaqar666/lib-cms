using LibCMS.Base;
using LibCMS.Database.Interfaces;

namespace LibCMS.Database.Relationships;

public class BelongsToMany<TTarget, TRelated>(TTarget target, string targetKey, string foreignKey) : Relationship<TTarget, TRelated>(target) where TTarget : BaseEntity where TRelated : BaseEntity
{
    public override (string, List<TRelated>) AccessDataStore()
    {
        List<Guid>? foreignKeyValue = GetEntityKeyValue<List<Guid>, TTarget>(Target, targetKey);

        (string entityName, List<TRelated> entities) = EntityDataStore.GetEntityStore();

        entities = entities.Where(entity => foreignKeyValue is not null && foreignKeyValue.Contains(entity.Guid)).ToList();

        return (entityName, entities);
    }

    public override TRelated Store(TRelated entity)
    {
        List<Guid>? relatedForeignKeys = GetEntityKeyValue<List<Guid>, TRelated>(entity, foreignKey);

        if (relatedForeignKeys is null) relatedForeignKeys = [Target.Guid];

        else if (relatedForeignKeys.Contains(Target.Guid)) return entity;

        else relatedForeignKeys.Add(Target.Guid);

        SetEntityKeyValue(entity, foreignKey, relatedForeignKeys);

        TRelated storedEntity = base.Store(entity);

        List<Guid>? relatedTargetKeys = GetEntityKeyValue<List<Guid>, TTarget>(Target, targetKey);

        if (relatedTargetKeys is null) relatedTargetKeys = [entity.Guid];

        else if (relatedTargetKeys.Contains(entity.Guid)) return storedEntity;

        else relatedTargetKeys.Add(Target.Guid);

        SetEntityKeyValue(Target, targetKey, relatedTargetKeys);

        return storedEntity;
    }

    public override void Delete(Guid guid)
    {
        TRelated relatedEntity = GetOne(guid);

        List<Guid>? targetForeignKeys = GetEntityKeyValue<List<Guid>, TRelated>(relatedEntity, foreignKey);

        foreach (Guid targetForeignKey in targetForeignKeys ?? [])
        {
            IEntityManager<TTarget> entityManager = new DbContext().Of<TTarget>();

            TTarget targetEntity = entityManager.GetOne(targetForeignKey);

            List<Guid>? relatedKeys = GetEntityKeyValue<List<Guid>, TTarget>(targetEntity, foreignKey);

            if (relatedKeys is null) continue;

            relatedKeys.RemoveAll(relatedKey => relatedKey == guid);

            SetEntityKeyValue(targetEntity, foreignKey, relatedKeys);
        }

        base.Delete(guid);
    }

    public void Attach(Guid guid)
    {
        TRelated entity = GetOne(guid);

        Attach(entity);
    }

    public TRelated Attach(TRelated entity)
    {
        List<Guid>? targetKeys = GetEntityKeyValue<List<Guid>, TRelated>(entity, foreignKey);

        if (targetKeys is null) targetKeys = [Target.Guid];

        else targetKeys.Add(Target.Guid);

        SetEntityKeyValue(entity, foreignKey, targetKeys);

        List<Guid>? foreignKeys = GetEntityKeyValue<List<Guid>, TTarget>(Target, targetKey);

        if (foreignKeys is null) foreignKeys = [entity.Guid];

        else foreignKeys.Add(entity.Guid);

        SetEntityKeyValue(Target, targetKey, foreignKeys);

        return entity;
    }

    public void Detach(Guid guid)
    {
        TRelated entity = GetOne(guid);

        Detach(entity);
    }

    public TRelated Detach(TRelated entity)
    {
        List<Guid>? targetKeys = GetEntityKeyValue<List<Guid>, TRelated>(entity, foreignKey);

        if (targetKeys is not null) targetKeys.RemoveAll(guid => guid == Target.Guid);

        SetEntityKeyValue(entity, foreignKey, targetKeys);

        List<Guid>? foreignKeys = GetEntityKeyValue<List<Guid>, TTarget>(Target, targetKey);

        if (foreignKeys is not null) foreignKeys.RemoveAll(guid => guid == entity.Guid);

        SetEntityKeyValue(Target, targetKey, foreignKeys);

        return entity;
    }
}
