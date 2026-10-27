namespace BatatasFinance.Domain.Entities;

public class Account
{
    public Guid Id {get; init;}
    public string AccountName { get; init; }
    public string BankName { get; init; }
    public decimal Balance { get; set; }
    public decimal? RentableTax {get; set;}
    public bool IsRentable => RentableTax.HasValue;
    public Guid UserId {get; init;}
    public DateTimeOffset CreatedAt {get; init;}

    public Account(string accountName, string bankName, decimal balance, Guid userId)
    {
        if(string.IsNullOrWhiteSpace(accountName))
            throw new ArgumentNullException("invalid account name");
        if(string.IsNullOrWhiteSpace(bankName))
            throw new ArgumentNullException("invalid bank name");
        if(balance < 0)
            throw new ArgumentNullException("invalid balance");
        Id= Guid.NewGuid();
        AccountName = accountName;
        BankName = bankName;
        Balance = balance;
        CreatedAt = DateTimeOffset.UtcNow;
        UserId = userId;
    }

    public void Debit(decimal amount)
    {
        if(amount<=0)
            throw new ArgumentException("amount must be greater than zero");
        Balance -= amount;
    }
    private Account(){}
}