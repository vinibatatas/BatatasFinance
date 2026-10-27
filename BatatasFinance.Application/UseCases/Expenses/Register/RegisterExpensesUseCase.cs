using System.Transactions;
using BatatasFinance.Domain.Entities;
using BatatasFinance.Domain.Enums;
using BatatasFinance.Domain.Repositories;
using Microsoft.Extensions.Logging;
namespace BatatasFinance.Application.UseCases.Expenses.Register;
public class RegisterExpensesUseCase(IExpenseRepository expensRepository,IAccountRepository accountRepository, ILogger<RegisterExpensesUseCase> logger) : IRegisterExpenseUseCase
{
    public async Task ExecuteAsync(RegisterExpenseRequest request)
    {
      bool isCredit = request.PaymentMethod == PaymentMethod.CreditCard;
          if (isCredit ? request.CreditCardId.GetValueOrDefault() == Guid.Empty : request.AccountId.GetValueOrDefault() == Guid.Empty)
              throw new ArgumentException(isCredit 
                  ? "O identificador do cartão de crédito é obrigatório para compras na modalidade crédito." 
                  : "O identificador da conta é obrigatório para o método de pagamento selecionado.");
          if (isCredit ? request.AccountId.HasValue : request.CreditCardId.HasValue) 
              throw new ArgumentException(isCredit 
                  ? "Compras no cartão de crédito não podem ser vinculadas diretamente a uma conta bancária de débito." 
                  : "Pagamentos à vista não podem ser vinculados a um cartão de crédito.");
          Account? account = null;
          if (!isCredit)
          {
              account = await accountRepository.GetByIdAsync(request.AccountId.Value);
              if (account == null)
                  throw new ArgumentException("A conta bancaria não existe");
              account.Debit(request.Amount);
          }
        logger.LogInformation($"Register expense {request.Description}");
        var purchase = new Expense(request.Description,request.Amount,request.Category,request.PaymentMethod,request.IsFixed, request.UserId,request.CreditCardId,request.AccountId, request.Installments);
        using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        await expensRepository.AddAsync(purchase);
        if (account != null)
        {
            await accountRepository.UpdateAsync(account);
        }
        transaction.Complete(); 
        logger.LogInformation($"Expenses register sucess {request.Description}");
    }
}