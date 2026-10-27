namespace BatatasFinance.Application.UseCases.Accounts.GetAll;

public record AccountResponse(
    Guid Id,
    string AccountName,
    string BankName,
    decimal Balance,
    DateTimeOffset DateCreated,
    Guid UserId);