using LibCMS.Base;
using LibCMS.Database.Relationships;

namespace LibCMS.Entities;

public class Tag(string name) : BaseEntity
{
    public string Name { get; set; } = name;

    public List<Guid> PostIds { get; set; } = [];

    public List<Guid> ReaderIds { get; set; } = [];

    public BelongsToMany<Tag, Post> Posts()
    {
        return new BelongsToMany<Tag, Post>(this, "PostIds", "TagIds");
    }

    public BelongsToMany<Tag, Reader> Readers()
    {
        return new BelongsToMany<Tag, Reader>(this, "ReaderIds", "FollowedTagIds");
    }
}
