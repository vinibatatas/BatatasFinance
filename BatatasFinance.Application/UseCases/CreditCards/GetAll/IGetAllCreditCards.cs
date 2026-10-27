namespace BatatasFinance.Application.UseCases.CreditCards.GetAll;
public interface IGetAllCreditCards
{
    Task <IEnumerable<CreditCardResponse>> ExecuteAsync(Guid userId);
}