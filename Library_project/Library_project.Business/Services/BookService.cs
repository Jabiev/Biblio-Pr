using Library_project.Business.Exceptions;
using Library_project.Business.Interfaces;
using Library_project.Core.Entities;
using static Library_project.DataAccess.Repositories.DataContext;

namespace Library_project.Business.Services;

public class BookService : IBookService
{
    private AuthorService _authorService;
    private GenreService _genreService;
    private RenterService _renterService;

    public BookService(AuthorService authorService, GenreService genreService, RenterService renterService)
    {
        _authorService = authorService;
        _genreService = genreService;
        _renterService = renterService;
    }

    public void Create(string? name, DateTime publishTime, int count, HashSet<int> authorIds, HashSet<int> genreIds)
    {
        if (string.IsNullOrEmpty(name))
            throw new NullorEmptyException("The value is null or empty");
        if (count < 0)
            throw new InvalidDataException("Invalid operation");
        foreach (var id in authorIds)
            _authorService.GetById(id);
        foreach (var id in genreIds)
            _genreService.GetById(id);
        Book book = new(name, publishTime, count, authorIds, genreIds);
        Books?.Add(book);
    }

    public void Delete(Guid id)
    {
        Book? book = Books?.Find(b => b.Id == id);
        if (book is null)
            throw new NotFoundException("The value doesn't exist");
        Books?.Remove(book);
    }

    public List<Book> GetAll()
    {
        return Books;
    }

    public List<Book> GetByAuthor(int id)
    {
        Author author = _authorService.GetById(id);
        return Books.FindAll(b => b.AuthorIds.Contains(author.Id));
    }

    public List<Book> GetByGenre(int id)
    {
        Genre genre = _genreService.GetById(id);
        return Books.FindAll(b => b.GenreIds.Contains(genre.Id));
    }

    public Book GetById(Guid id)
    {
        Book? book = Books?.Find(b => b.Id == id);
        if (book is null)
            throw new NotFoundException("The value doesn't exist");
        return book;
    }

    public List<Book> SearchByName(string? search)
    {
        if (string.IsNullOrEmpty(search))
            throw new NullorEmptyException("The value is null or empty");
        return Books.FindAll(b => b.Name.Contains(search));
    }

    public void Update(Guid id, string? newName, DateTime newPublishTime, int newCount, HashSet<int> newAuthorIds, HashSet<int> newGenreIds)
    {
        Book? book = Books?.Find(b => b.Id == id);
        if (book is null)
            throw new NotFoundException("The value doesn't exist");
        if (string.IsNullOrEmpty(newName))
            throw new NullorEmptyException("The value is null or empty");
        if (Books?.Find(a => a.Name == newName) is not null)
            throw new AlreadyExistException($"The object which involving the {newName} is already exist");
        if (newCount < 0)
            throw new InvalidDataException("Invalid operation");
        foreach (var aId in newAuthorIds)
            _authorService.GetById(aId);
        foreach (var gId in newGenreIds)
            _genreService.GetById(gId);
        book.Name = newName;
        book.PublishTime = newPublishTime;
        book.Count = newCount;
        book.AuthorIds = newAuthorIds;
        book.GenreIds = newGenreIds;
    }
}