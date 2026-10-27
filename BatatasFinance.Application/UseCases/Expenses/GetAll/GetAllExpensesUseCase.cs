using BatatasFinance.Domain.Repositories;

namespace BatatasFinance.Application.UseCases.Expenses.GetAll;

public class GetAllExpensesUseCase(IExpenseRepository repository) : IGetAllPurchasesUseCase
{
    public async Task<IEnumerable<ExpenseResponse?>> ExecuteAsync(Guid userId)
    {
        var expenses = await repository.GetAllAsync(userId);
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