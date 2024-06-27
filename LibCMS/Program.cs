using LibCMS.Database;
using LibCMS.Entities;

DbContext dbContext = new();

Tag tag = new("Tag");
Post post = new("Content");
Author author = new("Muhammad");

tag = dbContext.Of<Tag>().Store(tag);
author = dbContext.Of<Author>().Store(author);

post = author.Posts().Store(post);
post.Tags().Attach(tag);

List<Author> authors = dbContext.Of<Author>().Get();

foreach (Author fetchedAuthor in authors)
{
    Console.WriteLine($"Author name: {fetchedAuthor.Name}");

    List<Post> posts = fetchedAuthor.Posts().Get();

    foreach (Post fetchedPost in posts)
    {
        Console.WriteLine($"Post content: {fetchedPost.Content}");

        List<Tag> tags = fetchedPost.Tags().Get();

        foreach (Tag fetchedTag in tags)
        {
            Console.WriteLine($"Tag name: {fetchedTag.Name}");
        }
    }
}
