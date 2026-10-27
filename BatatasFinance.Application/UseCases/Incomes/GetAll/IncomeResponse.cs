namespace BatatasFinance.Application.UseCases.Incomes.GetAll;

public record IncomeResponse(Guid Id, string Description, decimal Amount, bool IsRecurring,Guid UserId , DateTimeOffset CreatedAt);