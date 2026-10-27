using BatatasFinance.Domain.Enums;

namespace BatatasFinance.Application.UseCases.Expenses.GetAll;

public record ExpenseResponse
    (
    Guid Id ,
    string Description ,
    decimal Amount ,
    ExpenseCategory Category ,
    PaymentMethod PaymentMethod ,
    bool IsFixed ,
    Guid UserId ,
    Guid? CreditCardId,
    int  Installments,
    DateTimeOffset CreatedAt
    );