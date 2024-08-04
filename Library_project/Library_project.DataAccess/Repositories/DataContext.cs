using Library_project.Core.Entities;

namespace Library_project.DataAccess.Repositories;

public static class DataContext
{
    static DataContext()
    {
        Authors = new List<Author>();
        Books = new List<Book>();
        Genres = new List<Genre>();
        Renters = new List<Renter>();
        Loans = new List<Loan>();
    }
    public static List<Author>? Authors;
    public static List<Book>? Books;
    public static List<Genre>? Genres;
    public static List<Renter> Renters;
    public static List<Loan> Loans;
}