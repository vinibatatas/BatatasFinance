namespace BatatasFinance.Application.UseCases.Incomes.Update;

public interface IUpdateIncomeUseCase
{
    Task ExecuteAsync(Guid incomeId, Guid userId, UpdateIncomeRequest request);
}