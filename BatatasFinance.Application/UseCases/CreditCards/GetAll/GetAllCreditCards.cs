using BatatasFinance.Domain.Repositories;
namespace BatatasFinance.Application.UseCases.CreditCards.GetAll;
public class GetAllCreditCards(ICreditCardRepository repository) : IGetAllCreditCards
{
    public async Task<IEnumerable<CreditCardResponse?>> ExecuteAsync(Guid userId)
    {
        var creditCards = await repository.GetAllAsync(userId);
        return creditCards.Select(c => new CreditCardResponse(
            c.Id,
            c.Name,
            c.CreditLimit,
            c.ClosingDay,
            c.DueDay,
            c.UserId,
            c.CreditFlag,
            c.CreatedAt
        ));
    }
}