using BatatasFinance.Domain.Enums;

namespace BatatasFinance.Application.UseCases.CreditCards.GetAll;

public record CreditCardResponse
(
    Guid Id,
    string Name,
    decimal CreditLimit,
    int  ClosingDay,
    int  DueDay,
    Guid UserId,
    CreditFlag CreditFlag,
    DateTimeOffset CreatedAt
    
);