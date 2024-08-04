using Library_project.Business.Exceptions;
using Library_project.Business.Interfaces;
using Library_project.Core.Entities;
using static Library_project.DataAccess.Repositories.DataContext;

namespace Library_project.Business.Services;

public class LoanService : ILoanService
{
    private BookService _bookService;
    private RenterService _renterService;

    public LoanService(BookService bookService, RenterService renterService)
    {
        _bookService = bookService;
        _renterService = renterService;
    }

    public void LoanBook(Guid bookId, Guid renterId, DateTime loanDate, double loanPeriod)
    {
        Book book = _bookService.GetById(bookId);
        Renter renter = _renterService.GetById(renterId);
        if (book is null || renter is null)
            throw new NotFoundException($"The Book or The Renter object isn't found");
        if (book.Count <= 0)
            throw new InvalidOperationException("The book count used up");
        if (loanDate < DateTime.Now || loanPeriod > 7)
            throw new Exception("Invaild entry or exceedingly period day");
        book.Count--;
        Loan loan = new(book, renter, loanDate, loanDate.AddDays(loanPeriod));
        loan.BookIds.Add(bookId);
        loan.RenterIds.Add(renterId);
        Loans.Add(loan);
    }

    public void ReturnBook(Guid bookId, Guid renterId, DateTime returnDate)
    {
        Loan loan = Loans.Find(l => l.Book.Id == bookId && l.Renter.Id == renterId);
        if (loan is null)
            throw new NotFoundException("The loan object isn't found");
        if (returnDate < loan.LoanDate)
            throw new InvalidOperationException($"Invalid returnDate {returnDate}");
        loan.ReturnDate = returnDate;
        loan.Book.Count++;
        loan.BookIds.Remove(bookId);
        loan.RenterIds.Remove(renterId);
        Loans.Remove(loan);
    }
    public List<Loan> GetOverdueLoans(DateTime today)
    {
        if (today < DateTime.Now)
            throw new InvalidOperationException("Invalid today");
        return Loans.FindAll(l => l.DueDate <= today);
    }

    public List<Book> GetByRenter(Guid renterId)
    {
        if (_renterService.GetById(renterId) is null)
            throw new NotFoundException("The object isn't found");
        Loan loan = Loans.Find(l => l.RenterIds.Contains(renterId));
        if (loan is null)
            throw new NotFoundException("The object isn't found");
        if (loan.BookIds is null)
            throw new NotFoundException("The set isn't found");
        List<Book> books = new List<Book>();
        foreach (var id in loan.BookIds)
        {
            Book? book = Books?.Find(b => b.Id == id);
            if (book is not null)
                books.Add(book);
        }
        return books;
    }
}
