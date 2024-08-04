using Library_project.Core.IEntities;

namespace Library_project.Core.Entities;

public class Loan : IEntity<int>
{
    public int Id { get; set; }
    private static int _id;
    public Guid BookId { get; set; }
    public Guid RenterId { get; set; }
    public DateTime LoanDate { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public Loan(Guid bookId, Guid renterId, DateTime loanDate, DateTime dueDate, DateTime? returnDate)
    {
        Id = _id++;
        BookId = bookId;
        RenterId = renterId;
        LoanDate = loanDate;
        DueDate = dueDate;
        ReturnDate = returnDate;
    }
}