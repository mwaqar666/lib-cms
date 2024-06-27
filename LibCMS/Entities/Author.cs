using LibCMS.Database.Relationships;

namespace LibCMS.Entities;

public class Author(string name) : User(name)
{
    public List<Guid> ReaderIds { get; set; } = [];

    public HasMany<Author, Post> Posts()
    {
        return new HasMany<Author, Post>(this, "AuthorId");
    }

    public BelongsToMany<Author, Reader> InterestedReaders()
    {
        return new BelongsToMany<Author, Reader>(this, "ReaderIds", "FollowedAuthorIds");
    }
}