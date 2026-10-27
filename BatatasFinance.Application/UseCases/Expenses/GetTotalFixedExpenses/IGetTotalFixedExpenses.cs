namespace BatatasFinance.Application.UseCases.Expenses.GetTotalFixedExpenses;

public interface IGetTotalFixedExpenses
{
    Task<decimal> ExecuteAsync(Guid userId);
}