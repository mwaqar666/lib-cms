using LibCMS.Base;
using LibCMS.Database.Relationships;

namespace LibCMS.Entities;

public class Post(string content) : BaseEntity
{
    public Guid AuthorId { get; set; }

    public List<Guid> TagIds { get; set; } = [];

    public string Content { get; set; } = content;

    public DateTime CreationDate { get; } = DateTime.Now;

    public DateTime? PublicationDate { get; private set; }

    public bool IsDraft => PublicationDate is null;

    public bool IsPublished => PublicationDate is not null;

    public BelongsTo<Post, Author> Author()
    {
        return new BelongsTo<Post, Author>(this, "AuthorId");
    }

    public BelongsToMany<Post, Tag> Tags()
    {
        return new BelongsToMany<Post, Tag>(this, "TagIds", "PostIds");
    }

    public void Publish()
    {
        PublicationDate = DateTime.Now;
    }
}