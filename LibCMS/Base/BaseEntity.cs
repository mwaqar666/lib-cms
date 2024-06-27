namespace LibCMS.Base;

public abstract class BaseEntity
{
    public Guid Guid { get; set; } = Guid.NewGuid();
}