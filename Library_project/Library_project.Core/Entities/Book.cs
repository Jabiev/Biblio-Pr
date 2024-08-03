using Library_project.Core.IEntities;

namespace Library_project.Core.Entities;

public class Book : IEntity<Guid>
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public HashSet<int> AuthorIds { get; set; }
    public HashSet<int> GenreIds { get; set; }
    public DateTime PublishTime { get; set; }
    public int Count { get; set; }
    public Book(string? name, DateTime publishTime, int count, HashSet<int> authorids, HashSet<int> genreids)
    {
        Id = Guid.NewGuid();
        Name = name;
        AuthorIds = authorids;
        GenreIds = genreids;
        PublishTime = publishTime;
        Count = count;
    }
}