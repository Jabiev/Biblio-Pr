using Library_project.Core.Entities;

namespace Library_project.Business.Interfaces;

public interface ILoanService
{
    void LoanBook(Guid bookId, int renterId, DateTime loanDate, double loanPeriod);
    void ReturnBook(Guid bookId, int renterId, DateTime returnDate);
    List<Loan> GetAll();
    List<Loan> GetOverdueLoans(DateTime today);
    List<Book> GetByRenter(int renterId);
}