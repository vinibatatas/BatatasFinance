namespace BatatasFinance.Application.UseCases.CreditCards.GetSummary;

public interface IGetCreditCardSummaryUseCase
{
    Task<CreditCardSummaryResponse> ExecuteAsync(
        Guid creditCardId,
        Guid userId );
}