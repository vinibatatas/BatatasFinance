using BatatasFinance.Domain.Enums;

namespace BatatasFinance.Application.UseCases.Expenses.Register;

public record RegisterExpenseRequest
{
    public string Description { get; init; }
    public decimal Amount { get; init; }
    public ExpenseCategory Category { get; init; }
    public bool IsFixed { get; init; }
    public Guid UserId { get; init; }
    public Guid? CreditCardId { get; init; }
    public Guid? AccountId { get; init; }
    public int Installments  { get; init; }
    public PaymentMethod PaymentMethod { get; init; }
}