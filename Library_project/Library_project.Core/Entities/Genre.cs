using Library_project.Core.IEntities;

namespace Library_project.Core.Entities;

public class Genre : IEntity<int>
{
    public int Id { get; set; }
    private int _id;
    public string? Name { get; set; }
    public Genre(string? name)
    {
        Id = _id++;
        Name = name;
    }
}