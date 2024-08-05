using Library_project.Core.IEntities;

namespace Library_project.Core.Entities;

public class Genre : IEntity<int>
{
    public int Id { get; set; }
    private static int _id;
    public string? Name { get; set; } = null!;
    public Genre(string? name)
    {
        Id = _id++;
        Name = name;
    }
    public override string ToString()
    {
        return $"Id {Id} | Name {Name}";
    }
}