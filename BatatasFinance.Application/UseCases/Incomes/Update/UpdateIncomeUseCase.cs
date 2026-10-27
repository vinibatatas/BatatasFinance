using BatatasFinance.Domain.Repositories;

namespace BatatasFinance.Application.UseCases.Incomes.Update;

public class UpdateIncomeUseCase(IIncomeRepository repository) : IUpdateIncomeUseCase
{
    public async Task ExecuteAsync(Guid incomeId, Guid userId, UpdateIncomeRequest request)
    {
        var income = await repository.GetByIdAsync(incomeId, userId);
        if (income == null)
            throw new ArgumentException("Income not found");
        income?.UpdateIncome(request.Description, request.Amount, request.IsRecurring, request.UserId);
        if (income != null) await repository.UpdateAsync(income);
    }
}