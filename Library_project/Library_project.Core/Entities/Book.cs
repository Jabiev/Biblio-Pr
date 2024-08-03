using Library_project.Core.IEntities;

namespace Library_project.Core.Entities;

public class Book : IEntity<Guid>
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public List<int> AuthorId { get; set; }
    public List<int> GenreId { get; set; }
    public DateTime PublishTime { get; set; }
    public int Count { get; }
    public Book(string? name, int count, DateTime publishTime)
    {
        Id = Guid.NewGuid();
        Name = name;
        AuthorId = new List<int>();
        GenreId = new List<int>();
        PublishTime = publishTime;
        Count = count;
    }
}
