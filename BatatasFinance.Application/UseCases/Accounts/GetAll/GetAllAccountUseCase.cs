using BatatasFinance.Domain.Repositories;

namespace BatatasFinance.Application.UseCases.Accounts.GetAll;

public class GetAllAccountUseCase (IAccountRepository repository) : IGetAllAccountUseCase
{
    public async Task<IEnumerable<AccountResponse?>> ExecuteAsync(Guid userId)
    {
        var accounts = await repository.GetAllAsync(userId);
        return accounts.Select(a => new AccountResponse(
            a.Id,
            a.AccountName,
            a.BankName,
            a.Balance,
            a.CreatedAt,
            a.UserId));
    }
}