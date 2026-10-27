namespace BatatasFinance.Application.UseCases.Incomes.Update;

public record UpdateIncomeRequest
{
    public string Description { get; init; }
    public decimal Amount { get; init; }
    public bool IsRecurring { get; init; }
    public Guid UserId { get; init; }
};