using BatatasFinance.Application.UseCases.Expenses.GetAll;

namespace BatatasFinance.Application.UseCases.Expenses.GetFixedExpenses;

public interface IGetFixedExpensesUseCase
{
    Task <IEnumerable<ExpenseResponse?>> ExecuteAsync( Guid userId);
}