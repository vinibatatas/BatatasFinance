using BatatasFinance.Application.UseCases.Incomes.GetAll;

namespace BatatasFinance.Application.UseCases.Incomes.SummaryResponse;

public record IncomeSummaryResponse
(
        IEnumerable<IncomeResponse> Incomes,
        decimal TotalAmount 
);