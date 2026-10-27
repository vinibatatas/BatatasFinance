namespace BatatasFinance.Application.UseCases.Accounts.Register;

public interface IRegisterAccountUseCase
{
    Task ExecuteAsync(string accountName, string bankName, decimal balance, Guid userId);
}