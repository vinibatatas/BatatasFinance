using BatatasFinance.Domain.Enums;

namespace BatatasFinance.Domain.Entities;

public class Expense
{
    public Guid Id { get; init; }
    public string Description { get; private set; }
    public decimal Amount { get; private set; }
    public ExpenseCategory Category { get; init; }
    public PaymentMethod PaymentMethod { get; init; }
    public bool IsFixed { get; init; }
    public Guid UserId { get; set; }
    public Guid? CreditCardId { get; init; }
    public Guid? AccountId { get; init; }
    public int Installments { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
    public Expense(
        string description,
        decimal amount,
        ExpenseCategory category,
        PaymentMethod paymentMethod,
        bool isFixed,
        Guid userId,
        Guid? creditCardId,
        Guid? accountId,
        int installments
        )
    {
        if(userId == Guid.Empty)
            throw new ArgumentException("Invalid user.");
        if (string.IsNullOrWhiteSpace(description)) 
            throw new ArgumentException("Invalid description.");
        if (amount <= 0) 
            throw new ArgumentException("Invalid amount.");
        if (paymentMethod == PaymentMethod.CreditCard && creditCardId == null)
            throw new ArgumentException("Invalid credit card id.");
        if (userId == Guid.Empty)
            throw new ArgumentException("Invalid user.");
        if (installments < 1 || installments > 12 )
            throw new ArgumentException("invalid number installments.");
        Id = Guid.NewGuid();
        Description = description;
        Amount = amount;
        Category = category;
        PaymentMethod = paymentMethod;
        IsFixed = isFixed;
        UserId = userId;
        CreditCardId = creditCardId;
        AccountId = accountId;
        Installments = installments;
        CreatedAt = DateTimeOffset.UtcNow;
    }
    private Expense(){}

}