using Library_project.Business.Services;
using Library_project.Core.Entities;

namespace Library_project.Business.Interfaces;

public interface IAuthorService
{
    void Create(string? name, string? surname);
    void Update(int id, string? newName, string? newSurname);
    void Delete(int id, BookService bookService);
    List<Author> GetAll();
    Author GetById(int id);
    List<Author> SearchByName(string? search);
}