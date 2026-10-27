using BatatasFinance.Application.UseCases.Expenses.GetAll;
using BatatasFinance.Domain.Repositories;

namespace BatatasFinance.Application.UseCases.Expenses.GetFixedExpenses;

public class GetFixedExpensesUseCase(IExpenseRepository repository) : IGetFixedExpensesUseCase
{
    public async Task<IEnumerable<ExpenseResponse?>> ExecuteAsync(Guid userId)
    {
        var expenses = await repository.GetFixedExpensesAsync(userId);
        return expenses.Select(p => new ExpenseResponse(
            p.Id,
            p.Description,
            p.Amount,
            p.Category,
            p.PaymentMethod,
            p.IsFixed,
            p.UserId,
            p.CreditCardId,
            p.Installments,
            p.CreatedAt
        ));
    }
}