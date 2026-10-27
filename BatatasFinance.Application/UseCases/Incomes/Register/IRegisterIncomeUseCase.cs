namespace BatatasFinance.Application.UseCases.Incomes.Register;

public interface IRegisterIncomeUseCase
{
    Task ExecuteAsync(string description, decimal amount, bool isRecurring, Guid userId);
}