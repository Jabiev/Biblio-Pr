using Library_project.Core.Entities;

namespace Library_project.DataAccess.Repositories;

public static class DataContext
{
    public static List<Author>? Authors;
    public static List<Book>? Books;
    public static List<Genre>? Genres;
}
