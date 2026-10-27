namespace BatatasFinance.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    // public string Username { get; set; }
    // public string Password { get; set; }
    public string UserName { get; init; }
    public IEnumerable<Income> Incomes { get; set; }
    public IEnumerable<Expense> Expenses { get; set; }
    public IEnumerable<Account> Accounts { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public User(string userName)
    {
        if (string.IsNullOrWhiteSpace(userName))
            throw new ArgumentException("Name is required");
        Id = Guid.NewGuid();
        CreatedAt = DateTimeOffset.UtcNow;
        UserName = userName;
    }
    private User(){}
}