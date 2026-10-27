using BatatasFinance.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace BatatasFinance.Application.UseCases.CreditCards.GetSummary;

public class GetCreditCardSummaryUseCase(IExpenseRepository expenseRepository,
    ICreditCardRepository creditCardRepository, ILogger<GetCreditCardSummaryUseCase> logger) : IGetCreditCardSummaryUseCase
{
    public async Task<CreditCardSummaryResponse> ExecuteAsync(Guid creditCardId, Guid userId)
    {
        logger.LogInformation($"Executando GetCreditCardSummaryUseCase");
        var card = await creditCardRepository.GetByIdAsync(creditCardId);
        logger.LogInformation($"{card} encontrado com sucesso");
        if (card is null || card.UserId != userId)
            throw new ArgumentException("Cartão não encontrado");
        var invoiceAmount = await expenseRepository.GetInvoiceAsync(creditCardId, userId);
        logger.LogInformation($"{invoiceAmount} encontrado com sucesso");
        var availableLimit = card.CreditLimit -  invoiceAmount;
        return new CreditCardSummaryResponse{
            CreditCardId = creditCardId,
            UserId = userId,
            TotalLimit = card.CreditLimit,
            AvailableLimit = availableLimit,
            CurrentInvoiceAmount = invoiceAmount
        };
    }
}