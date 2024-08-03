using Library_project.Core.Entities;

namespace Library_project.Business.Interfaces;

public interface IAuthorService
{
    void Create(string? name);
    void Update(int id, string? name, string? surname);
    void Delete(int id);
    List<Author> GetAll();
    Author GetById(int id);
    List<Author> SearchByName(string? search);
}
