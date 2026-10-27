using BatatasFinance.Application.UseCases.Incomes.SummaryResponse;

namespace BatatasFinance.Application.UseCases.Incomes.GetAll;

public interface IGetAllIncomesUseCase
{
    Task<IEnumerable<IncomeResponse?>> ExecuteAsync(Guid userId);
}