using BatatasFinance.Domain.Entities;

namespace BatatasFinance.Domain.Repositories;

public interface IAccountRepository
{
    Task AddAsync(Account account);
    Task <IEnumerable<Account>> GetAllAsync(Guid userId); 
    Task UpdateAsync(Account account); 
    Task <Account?> GetByIdAsync(Guid accountId);
    // 
   
}