using BatatasFinance.Domain.Repositories;

namespace BatatasFinance.Application.UseCases.CreditCards.GetCreditCardExpense;

public class GetCreditCardExpensesUseCase (IExpenseRepository repository, ICreditCardRepository creditCardRepository) : IGetCreditCardExpenses
{
    public async Task<IEnumerable<CreditCardSummaryExpensesResponse?>> GetExpensesCardAsync(Guid creditCardId, Guid userId)
    {
        var card = await creditCardRepository.GetByIdAsync(creditCardId);
        if (card == null)
            return Enumerable.Empty<CreditCardSummaryExpensesResponse>();
        DateTimeOffset referenceDate = DateTimeOffset.UtcNow;
        DateTimeOffset currentMonthClosing = new DateTimeOffset(referenceDate.Year, referenceDate.Month, card.ClosingDay, 23, 59, 59, DateTimeOffset.UtcNow.Offset);
        DateTimeOffset startDate;
        DateTimeOffset endDate;
        if (referenceDate <= currentMonthClosing)
        {
            endDate = currentMonthClosing;
            startDate = currentMonthClosing.AddMonths(-1).AddDays(-1).Date;
        }
        else
        {
            startDate = currentMonthClosing.AddDays(1).Date;
            endDate = currentMonthClosing.AddMonths(1).AddDays(-1);
        }
        var utcStartDate = startDate.ToUniversalTime();
        var utcEndDate = endDate.ToUniversalTime();
        var expenses = await repository.GetExpensesCardAsync(creditCardId, userId, utcStartDate, utcEndDate);
        return expenses.Select(e => new CreditCardSummaryExpensesResponse( 
            e.Id,
            e.Description,
            e.Amount,
            e.Category,
            e.PaymentMethod,
            e.IsFixed,
            e.UserId,
            e.CreditCardId,
            e.Installments,
            e.CreatedAt));
    }
}