using BatatasFinance.Application.UseCases.Incomes.SummaryResponse;
using BatatasFinance.Domain.Repositories;

namespace BatatasFinance.Application.UseCases.Incomes.GetAll;

public class GetAllIncomesUseCase(IIncomeRepository repository) : IGetAllIncomesUseCase
{
    public async Task<IEnumerable<IncomeResponse?>> ExecuteAsync(Guid userId)
    {
        var incomes = await repository.GetAllAsync(userId);
        return incomes.Select(i => new IncomeResponse(
            i.Id,
            i.Description,
            i.Amount,
            i.IsRecurring,
            i.UserId,
            i.CreatedAt
        ));
       
    }
}