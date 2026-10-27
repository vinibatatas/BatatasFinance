using BatatasFinance.Domain.Entities;

namespace BatatasFinance.Domain.Repositories;

public interface IIncomeRepository
{
    Task AddAsync(Income income);
    Task<IEnumerable<Income>> GetAllAsync(Guid userId);
    Task <Income?> GetByIdAsync(Guid incomeId, Guid userId);
    Task UpdateAsync(Income income);
}