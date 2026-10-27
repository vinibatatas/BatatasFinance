namespace BatatasFinance.Application.UseCases.Accounts.Register;

public record RegisterAccountRequest
{
    public string AccountName { get; init; }
    public string BankName { get; init; }
    public decimal Balance { get; init; }
    public decimal? RentableTax { get; init; }
    public Guid UserId { get; init; }
};