namespace BatatasFinance.Domain.Repositories;
using BatatasFinance.Domain.Entities;
public interface ICreditCardRepository
{
    Task AddAsync(CreditCard creditCard);
    Task <IEnumerable<CreditCard?>> GetAllAsync(Guid userId);
    Task<CreditCard?> GetByIdAsync(Guid creditCardId);
}