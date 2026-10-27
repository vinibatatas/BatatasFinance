namespace BatatasFinance.Application.UseCases.Accounts.GetAll;

public interface IGetAllAccountUseCase
{
    Task<IEnumerable<AccountResponse?>> ExecuteAsync(Guid userId);
}