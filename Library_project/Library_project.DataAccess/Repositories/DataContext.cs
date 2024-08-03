using Library_project.Core.Entities;

namespace Library_project.DataAccess.Repositories;

public static class DataContext
{
    static DataContext()
    {
        Authors = new List<Author>();
        Books = new List<Book>();
        Genres = new List<Genre>();
    }
    public static List<Author>? Authors;
    public static List<Book>? Books;
    public static List<Genre>? Genres;
}
