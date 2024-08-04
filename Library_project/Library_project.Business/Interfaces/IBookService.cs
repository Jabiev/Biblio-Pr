using Library_project.Core.Entities;

namespace Library_project.Business.Interfaces;

public interface IBookService
{
    void Create(string? name, DateTime publishTime, int count, HashSet<int> authorIds, HashSet<int> genreIds);
    void Update(Guid id, string? newName, DateTime newPublishTime, int newCount, HashSet<int> newAuthorIds, HashSet<int> newGenreIds);
    void Delete(Guid id);
    List<Book> GetAll();
    Book GetById(Guid id);
    List<Book> GetByAuthor(int id);
    List<Book> GetByGenre(int id);
    public List<Book> GetByRenter(Guid id);
    List<Book> SearchByName(string? search);
}