using Library_project.Business.Exceptions;
using Library_project.Business.Interfaces;
using Library_project.Core.Entities;
using static Library_project.DataAccess.Repositories.DataContext;

namespace Library_project.Business.Services;

public class RenterService : IRenterService
{
    public void Create(string? name, string? surname)
    {
        if (string.IsNullOrEmpty(name))
            throw new NotFoundException("The value doesn't exist");
        if (Renters?.Find(r => r.Name == name) is not null)
            throw new AlreadyExistException("The Value already exists");
        Renter renter = new(name, surname);
        Renters?.Add(renter);
    }

    public void Delete(Guid id, LoanService loanService)
    {
        Renter? renter = Renters?.Find(r => r.Id == id);
        if (renter is null)
            throw new NotFoundException("The value doesn't exist");
        if (loanService.GetByRenter(id) is not null)
            throw new NotRemovedbyContainSomeItemsException("Not removed the value by depending on some other values");
        Renters?.Remove(renter);
    }

    public List<Renter> GetAll()
    {
        return Renters;
    }

    public Renter GetById(Guid id)
    {
        Renter? renter = Renters?.Find(r => r.Id == id);
        if (renter is null)
            throw new NotFoundException("The value doesn't exist");
        return renter;
    }

    public List<Renter> SearchByBook(string? search)
    {
        if (string.IsNullOrEmpty(search))
            throw new NullorEmptyException("The value is null or empty");
        Book? book = Books?.Find(b => b.Name.Contains(search));
        if (book is null)
            throw new NotFoundException("The object(ex: Book) doesn't exist");
        Loan loan = Loans.Find(l => l.Book == book);
        if (loan is null)
            throw new NotFoundException("The object isn't found");
        if (loan.RenterIds is null)
            throw new NotFoundException("The object isn't found");
        List<Renter> renters = new List<Renter>();
        foreach (var id in loan.RenterIds)
        {
            Renter? renter = Renters.Find(r => r.Id == id);
            if (renter is not null)
                renters.Add(renter);
        }
        return renters;
    }

    public void Update(Guid id, string? name, string? surname)
    {
        Renter? renter = Renters?.Find(r => r.Id == id);
        if (renter is null)
            throw new NotFoundException("The value doesn't exist");
        if (string.IsNullOrEmpty(name))
            throw new NullorEmptyException("The value is null or empty");
        if (Renters?.Find(r => r.Name == name) is not null)
            throw new AlreadyExistException($"The object which involving the {name} is already exist");
        renter.Name = name;
        renter.Surname = surname;
    }
}
