namespace Library_project.Core.IEntities;

public interface IEntity<Type>
{
    public Type Id { get; set; }
    public string? Name { get; set; }
}
