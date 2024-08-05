using Library_project.Business.Exceptions;
using Library_project.Business.Interfaces;
using Library_project.Core.Entities;
using static Library_project.DataAccess.Repositories.DataContext;

namespace Library_project.Business.Services;

public class AuthorService : IAuthorService
{
    public void Create(string? name, string? surname)
    {
        if (string.IsNullOrEmpty(name))
            throw new NotFoundException("The value is null or empty");
        if (Authors?.Find(a => a.Name == name.ToUpper()) is not null && Authors?.Find(a => a.Surname == surname) is not null)
            throw new AlreadyExistException("The Value already exists");
        Author author = new(name.ToUpper(), surname);
        Authors?.Add(author);
    }

    public void Delete(int id, BookService bookService)
    {
        Author? author = Authors?.Find(a => a.Id == id);
        if (author is null)
            throw new NotFoundException("The value doesn't exist");
        if (bookService.GetByAuthor(id).Count > 0)
            throw new NotRemovedbyContainSomeItemsException("Not removed the value by depending on some other values");
        Authors?.Remove(author);
    }

    public List<Author> GetAll()
    {
        return Authors;
    }

    public Author GetById(int id)
    {
        Author? author = Authors?.Find(a => a.Id == id);
        if (author is null)
            throw new NotFoundException("The value doesn't exist");
        return author;
    }

    public List<Author> SearchByName(string? search)
    {
        if (string.IsNullOrEmpty(search))
            throw new NullorEmptyException("The value is null or empty");
        return Authors.FindAll(a => a.Name.Contains(search.ToUpper()));
    }

    public void Update(int id, string? newName, string? newSurname)
    {
        Author? author = Authors?.Find(a => a.Id == id);
        if (author is null)
            throw new NotFoundException("The value doesn't exist");
        if (string.IsNullOrEmpty(newName))
            throw new NullorEmptyException("The value is null or empty");
        if (Authors?.Find(a => a.Name == newName) is not null)
            throw new AlreadyExistException($"The object which involving the {newName} is already exist");
        author.Name = newName;
        author.Surname = newSurname;
    }
}