using BatatasFinance.Domain.Enums;

namespace BatatasFinance.Domain.Entities;

public class CreditCard
{
    public Guid Id {get; init; }
    public string Name { get; private set; }
    public decimal CreditLimit { get ;  set; }
    public int ClosingDay { get ; set ; } 
    public int DueDay { get ; set ; }
    public Guid UserId { get ; set ; }
    public CreditFlag CreditFlag { get ; set; }
    public DateTimeOffset CreatedAt { get ; set; }
    public CreditCard(string name, decimal creditLimit, int closingDay, int dueDay, Guid userId, CreditFlag creditFlag)
    {
        if (userId == Guid.Empty)
            throw new ArgumentException("User must have an ID");
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Name cannot be null or empty");
        if (creditLimit < 0)
            throw new ArgumentException("Credit Limit cannot be negative");
        if (closingDay < 1 || closingDay > 31 )
            throw new ArgumentException("Closing day must be between 1 and 31.");
        if (dueDay < 1 || dueDay > 31)
            throw new ArgumentException("Due day must be between 1 and 31.");
        Id = Guid.NewGuid();
        Name = name;
        CreditLimit = creditLimit;
        ClosingDay = closingDay;
        DueDay = dueDay;
        UserId = userId;
        CreditFlag = creditFlag;
        CreatedAt = DateTimeOffset.UtcNow;
    }
    private CreditCard(Guid id, string name, decimal creditlimit, int closingday, int dueday, Guid userid, CreditFlag creditflag, DateTime createdat)
    {
        Id = id;
        Name = name;
        CreditLimit = creditlimit;
        ClosingDay = closingday;
        DueDay = dueday;
        UserId = userid;
        CreditFlag = creditflag; 
        CreatedAt = new DateTimeOffset(createdat, TimeSpan.Zero);
    }
    public void UpdateLimit(decimal newcreditLimit)
    {
        if (CreditLimit < 0)
            throw new ArgumentException("Credit Limit cannot be negative");
        CreditLimit = newcreditLimit;
    }
    public void UpdateDueDay(int newdueDay)
    {
        if (DueDay < 1 || DueDay > 31)
            throw new ArgumentException("Credit Limit must be between 1 and 31");
        DueDay = newdueDay;
    }
    public void UpdateClosingDay(int newclosingDay)
    {
        if (ClosingDay < 1 || ClosingDay > 31 )
            throw new ArgumentException("Credit Limit must be between 1 and 31");
        ClosingDay = newclosingDay;
    }
}