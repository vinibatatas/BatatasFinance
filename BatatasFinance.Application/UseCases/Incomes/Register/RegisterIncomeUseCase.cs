using BatatasFinance.Domain.Entities;
using BatatasFinance.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace BatatasFinance.Application.UseCases.Incomes.Register;

public class RegisterIncomeUseCase(IUserRepository userRepository, IIncomeRepository incomeRepository,  ILogger<RegisterIncomeUseCase> logger) : IRegisterIncomeUseCase
{
    public async Task ExecuteAsync(string description, decimal amount, bool isRecurring, Guid userId)
    {
            logger.LogInformation("Searching Income");
            var user = await userRepository.GetByIdAsync(userId);
            if (user == null) 
                throw new Exception("user not found");
            var income = new Income(description, amount, isRecurring, userId);
            await incomeRepository.AddAsync(income);
            logger.LogInformation("Returning Income");
    }
}