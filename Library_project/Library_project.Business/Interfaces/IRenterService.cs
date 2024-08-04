using Library_project.Business.Exceptions;
using Library_project.Business.Services;
using Library_project.Core.Entities;

namespace Library_project.Business.Interfaces;

public interface IRenterService
{
    void Create(string? name, string? surname);
    void Delete(Guid id, BookService bookService);
    void Update(Guid id, string? name, string? surname);
    List<Renter> GetAll();
    List<Renter> SearchByBook(string? search);
    public Renter GetById(Guid id);
}