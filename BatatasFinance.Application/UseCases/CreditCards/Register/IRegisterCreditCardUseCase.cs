using BatatasFinance.Domain.Enums;

namespace BatatasFinance.Application.UseCases.CreditCards.Register;

public interface IRegisterCreditCardUseCase
{
    Task ExecuteAsync(
        string name,
        decimal creditLimit,
        int closingDay,
        int dueDay,
        Guid userId,
        CreditFlag creditFlag
       );
}