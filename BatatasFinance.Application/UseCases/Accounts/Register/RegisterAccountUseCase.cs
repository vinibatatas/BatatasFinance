using BatatasFinance.Domain.Entities;
using BatatasFinance.Domain.Repositories;

namespace BatatasFinance.Application.UseCases.Accounts.Register;

public class RegisterAccountUseCase(IUserRepository userRepository, IAccountRepository accountRepository) : IRegisterAccountUseCase
{
    public async Task ExecuteAsync(string accountName, string bankName, decimal balance, Guid userId)
    {
        var user = await userRepository.GetByIdAsync(userId);
        if (user == null)
            throw new Exception("User not found");
        var account = new Account(accountName, bankName, balance, userId);
        await accountRepository.AddAsync(account);
    }
}