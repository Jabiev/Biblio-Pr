using Library_project.Business.Services;
using Library_project.Core.Entities;

namespace Library_project.Business.Interfaces;

public interface IGenreService
{
    void Create(string? name);
    void Update(int id, string? name);
    void Delete(int id, BookService bookService);
    List<Genre> GetAll();
    Genre GetById(int id);
    List<Genre> SearchByName(string? search);
}