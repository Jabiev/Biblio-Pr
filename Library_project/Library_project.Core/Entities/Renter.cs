using Library_project.Core.IEntities;

namespace Library_project.Core.Entities;

public class Renter : IEntity<int>
{
    /// <summary>
    /// Identification
    /// </summary>
    public int Id { get; set; }
    private static int _id;
    public string? Name { get; set; } = null!;
    public string? Surname { get; set; }

    public Renter(string? name, string? surname)
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