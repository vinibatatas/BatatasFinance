namespace BatatasFinance.Domain.Entities;

public class Income
{
    public Guid Id { get;  init; }
    public string Description { get;  set; }
    public decimal Amount { get;  set; }
    public bool IsRecurring { get;  set; }
    public DateTimeOffset CreatedAt { get;  private set; }
    public Guid UserId { get;  init; }
    public Income(string description, decimal amount, bool isRecurring, Guid userId)
    {
        if (string.IsNullOrWhiteSpace(description)) throw new ArgumentNullException("Invalid description.");
        if (amount <= 0) throw new ArgumentException("Invalid amount.");
        if (userId == Guid.Empty) throw new ArgumentNullException("Invalid user.");
        Id = Guid.NewGuid();
        Description = description;
        Amount = amount;
        IsRecurring = isRecurring;
        UserId = userId;
        CreatedAt = DateTimeOffset.UtcNow;
    }
    private Income(){}
    public void UpdateIncome (string description, decimal amount, bool isRecurring, Guid userId)
    {
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Description is invalid");
        if (amount <= 0)
            throw new ArgumentException("Amount must be greater than zero");
        Description = description;
        Amount = amount;
        IsRecurring = isRecurring;
    }
        
}