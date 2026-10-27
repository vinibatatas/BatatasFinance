using BatatasFinance.Domain.Entities;
using BatatasFinance.Domain.Enums;
using BatatasFinance.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace BatatasFinance.Application.UseCases.CreditCards.Register;

public class RegisterCreditCardUseCase (ICreditCardRepository repository, ILogger<RegisterCreditCardUseCase> logger) : IRegisterCreditCardUseCase
{
    public async Task ExecuteAsync(
        string name,
        decimal creditLimit,
        int closingDay, 
        int dueDay,
        Guid userId,
        CreditFlag creditFlag
        )
    {
        logger.LogInformation($"Starting Registering creditcard {creditFlag}");
        var creditCard = new CreditCard(name, creditLimit, closingDay, dueDay, userId, creditFlag);
        await repository.AddAsync(creditCard);
        logger.LogInformation($"Card {name} successfully registered Id {creditCard.Id}");
    }
}