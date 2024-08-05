using Library_project.Core.IEntities;

namespace Library_project.Core.Entities;

public class Renter : IEntity<Guid>
{
    /// <summary>
    /// Identification
    /// </summary>
    public Guid Id { get; set; }
    public string? Name { get; set; } = null!;
    public string? Surname { get; set; }

    public Renter(string? name, string? surname)
    {
        Id = Guid.NewGuid();
        Name = name;
        Surname = surname;
    }
    public override string ToString()
    {
        return $"Id {Id} | Name {Name} | Surname {Surname}";
    }
}