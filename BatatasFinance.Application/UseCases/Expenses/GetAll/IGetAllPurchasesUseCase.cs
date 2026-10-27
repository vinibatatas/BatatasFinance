namespace BatatasFinance.Application.UseCases.Expenses.GetAll;

public interface IGetAllPurchasesUseCase
{
    Task<IEnumerable<ExpenseResponse?>> ExecuteAsync(Guid userId);
}