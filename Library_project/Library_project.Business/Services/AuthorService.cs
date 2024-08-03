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
            throw new NotFoundException("The value doesn't exist");
        if (Authors?.Find(a => a.Name == name) is not null)
            throw new AlreadyExistException("The Value already exists");
        Author author = new(name, surname);
        Authors?.Add(author);
    }

    public void Delete(int id)
    {
        Author? author = Authors?.Find(a => a.Id == id);
        if (author is null)
            throw new NotFoundException("The value doesn't exist");
        //Check Books that depending on Author(s)
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
        return Authors.FindAll(a => a.Name.Contains(search));
    }

    public void Update(int id, string? name, string? surname)
    {
        Author? author = Authors?.Find(a => a.Id == id);
        if (author is null)
            throw new NotFoundException("The value doesn't exist");
        if (string.IsNullOrEmpty(name))
            throw new NullorEmptyException("The value is null or empty");
        if (Authors?.Find(a => a.Name == name) is not null)
            throw new AlreadyExistException($"The object which involving the {name} is already exist");
        author.Name = name;
        author.Surname = surname;
    }
}