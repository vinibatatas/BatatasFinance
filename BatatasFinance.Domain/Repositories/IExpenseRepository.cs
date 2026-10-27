using BatatasFinance.Domain.Entities;

namespace BatatasFinance.Domain.Repositories;

public interface IExpenseRepository
{
    Task AddAsync(Expense expense);
    Task<IEnumerable<Expense?>> GetAllAsync(Guid userId);
    Task<decimal> GetInvoiceAsync(Guid creditCardId,  Guid userId);
    Task<IEnumerable<Expense?>> GetExpensesCardAsync(Guid creditCardId, Guid userId,  DateTimeOffset startDate, DateTimeOffset endDate);
    Task<IEnumerable<Expense?>> GetFixedExpensesAsync(Guid userId);
    Task<IEnumerable<decimal>> GetFixedAmountAsync( Guid userId);
}