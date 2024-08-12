using Library_project.Core.IEntities;

namespace Library_project.Core.Entities;

public class Loan : IEntity<int>
{
    public int Id { get; set; }
    private static int _id;
    public Book Book { get; set; }
    public Renter Renter { get; set; }
    public HashSet<Guid>? BookIds;
    public HashSet<int>? RenterIds;
    public DateTime LoanDate { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public Loan(Book book, Renter renter, DateTime loanDate, DateTime dueDate)
    {
        Id = _id++;
        Book = book;
        Renter = renter;
        LoanDate = loanDate;
        DueDate = dueDate;
        ReturnDate = new DateTime();
        BookIds = new HashSet<Guid>();
        RenterIds = new HashSet<int>();
    }
    public override string ToString()
    {
        return $"Id {Id} | Book {Book} | Renter {Renter} | BookIds {string.Join(",", BookIds)} | RenterIds {string.Join(",", RenterIds)} | LoanDate {LoanDate} | DueDate {DueDate} | ReturnDate {ReturnDate}";
    }
}