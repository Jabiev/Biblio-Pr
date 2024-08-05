using Library_project.Core.IEntities;

namespace Library_project.Core.Entities;

public class Author : IEntity<int>
{
    public int Id { get; set; }
    private static int _id;
    public string? Name { get; set; } = null!;
    public string? Surname { get; set; }
    public Author(string? name, string? surname)
    {
        Id = _id++;
        Name = name;
        Surname = surname;
    }
    public override string ToString()
    {
        return $"Id {Id} | Name {Name} | Surname {Surname}";
    }
}