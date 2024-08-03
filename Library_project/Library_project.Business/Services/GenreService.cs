using Library_project.Business.Exceptions;
using Library_project.Business.Interfaces;
using Library_project.Core.Entities;
using static Library_project.DataAccess.Repositories.DataContext;

namespace Library_project.Business.Services;

internal class GenreService : IGenreService
{
    public void Create(string? name)
    {
        if (string.IsNullOrEmpty(name))
            throw new NotFoundException("The value doesn't exist");
        Genre? genre = Genres?.Find(g => g.Name == name);
        if (genre is not null)
            throw new AlreadyExistException("The Value already exists");
        Genres?.Add(genre);
    }

    public void Delete(int id)
    {
        Genre? genre = Genres?.Find(g => g.Id == id);
        if (genre is null)
            throw new NotFoundException("The value doesn't exist");
        //Check Books that depending on Author(s)
        Genres?.Remove(genre);
    }

    public List<Genre> GetAll()
    {
        return Genres;
    }

    public Genre GetById(int id)
    {
        Genre? genre = Genres?.Find(g => g.Id == id);
        if (genre is null)
            throw new NotFoundException("The value doesn't exist");
        return genre;
    }

    public List<Genre> SearchByName(string? search)
    {
        if (string.IsNullOrEmpty(search))
            throw new NullorEmptyException("The value is null or empty");
        return Genres.FindAll(g => g.Name.Contains(search));
    }

    public void Update(int id, string? name)
    {
        if (Genres?.Find(g => g.Id == id) is null)
            throw new NotFoundException("The value doesn't exist");
        if (string.IsNullOrEmpty(name))
            throw new NullorEmptyException("The value is null or empty");
        Genre? genre = Genres?.Find(g => g.Name == name);
        if (genre is not null)
            throw new AlreadyExistException($"The object which involving the {name} is already exist");
        genre.Name = name;
    }
}
