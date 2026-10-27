using BatatasFinance.Domain.Enums;

namespace BatatasFinance.Application.UseCases.Expenses.Register;

public interface IRegisterExpenseUseCase
{
    Task ExecuteAsync(
        RegisterExpenseRequest request
         );
}