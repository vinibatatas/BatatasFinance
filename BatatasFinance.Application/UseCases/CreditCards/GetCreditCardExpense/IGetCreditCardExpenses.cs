namespace BatatasFinance.Application.UseCases.CreditCards.GetCreditCardExpense;

public interface IGetCreditCardExpenses
{
    Task<IEnumerable<CreditCardSummaryExpensesResponse?>> GetExpensesCardAsync(Guid creditCardId, Guid userId);
}