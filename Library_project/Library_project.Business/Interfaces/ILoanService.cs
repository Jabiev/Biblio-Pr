using Library_project.Core.Entities;

namespace Library_project.Business.Interfaces;

public interface ILoanService
{
    void LoanBook(Guid bookId, Guid renterId, DateTime loanDate, double loanPeriod);
    void ReturnBook(Guid bookId, Guid renterId, DateTime returnDate);
    List<Loan> GetAll();
    List<Loan> GetOverdueLoans(DateTime today);
    List<Book> GetByRenter(Guid renterId);
}