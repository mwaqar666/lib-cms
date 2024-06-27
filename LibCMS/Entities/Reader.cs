using LibCMS.Database.Relationships;

namespace LibCMS.Entities;

public class Reader(string name) : User(name)
{
    public List<Guid> FollowedTagIds { get; set; } = [];

    public List<Guid> FollowedAuthorIds { get; set; } = [];

    public BelongsToMany<Reader, Tag> FollowedTags()
    {
        return new BelongsToMany<Reader, Tag>(this, "FollowedTagIds", "ReaderIds");
    }

    public BelongsToMany<Reader, Author> FollowedAuthors()
    {
        return new BelongsToMany<Reader, Author>(this, "FollowedAuthorIds", "ReaderIds");
    }
}