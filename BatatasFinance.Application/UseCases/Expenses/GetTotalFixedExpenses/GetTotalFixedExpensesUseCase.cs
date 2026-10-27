using BatatasFinance.Application.UseCases.Expenses.GetTotalFixedExpenses;
using BatatasFinance.Domain.Repositories;

namespace BatatasFinance.Application.UseCases.Expenses.GetTotalFixedExpenses;

public class GetTotalFixedExpensesUseCase(IExpenseRepository repository) : IGetTotalFixedExpenses
{
    public async Task<decimal> ExecuteAsync(Guid userId)
    {
        var amounts = await repository.GetFixedAmountAsync(userId);
        decimal total = amounts.Sum();
        return total;
    }
}