using Microsoft.Extensions.DependencyInjection;
using BatatasFinance.Application.UseCases;
using BatatasFinance.Application.UseCases.Accounts.GetAll;
using BatatasFinance.Application.UseCases.Accounts.Register;
using BatatasFinance.Application.UseCases.CreditCards.GetAll;
using BatatasFinance.Application.UseCases.CreditCards.GetCreditCardExpense;
using BatatasFinance.Application.UseCases.CreditCards.GetSummary;
using BatatasFinance.Application.UseCases.CreditCards.Register;
using BatatasFinance.Application.UseCases.Expenses.GetAll;
using BatatasFinance.Application.UseCases.Expenses.GetFixedExpenses;
using BatatasFinance.Application.UseCases.Expenses.GetTotalFixedExpenses;
using BatatasFinance.Application.UseCases.Expenses.Register;
using BatatasFinance.Application.UseCases.Incomes.GetAll;
using BatatasFinance.Application.UseCases.Incomes.Register;
using BatatasFinance.Application.UseCases.Incomes.Update;
using BatatasFinance.Application.UseCases.Users.Register;
using GetFixedExpensesUseCase = BatatasFinance.Application.UseCases.Expenses.GetFixedExpenses.GetFixedExpensesUseCase;

namespace BatatasFinance.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IRegisterExpenseUseCase, RegisterExpensesUseCase>();
        services.AddScoped<IGetAllPurchasesUseCase, GetAllExpensesUseCase>();
        services.AddScoped<IRegisterIncomeUseCase, RegisterIncomeUseCase>();
        services.AddScoped<IGetAllIncomesUseCase, GetAllIncomesUseCase>();
        services.AddScoped<IRegisterCreditCardUseCase, RegisterCreditCardUseCase>();
        services.AddScoped<IGetAllCreditCards, GetAllCreditCards>();
        services.AddScoped<IRegisterUserCase, RegisterUserCase>();
        services.AddScoped<IGetCreditCardSummaryUseCase, GetCreditCardSummaryUseCase>();
        services.AddScoped<IGetCreditCardExpenses, GetCreditCardExpensesUseCase>();
        services.AddScoped<IUpdateIncomeUseCase, UpdateIncomeUseCase>();
        services.AddScoped<IGetAllAccountUseCase, GetAllAccountUseCase>();
        services.AddScoped<IRegisterAccountUseCase, RegisterAccountUseCase>();
        services.AddScoped<IGetFixedExpensesUseCase, GetFixedExpensesUseCase>();
        services.AddScoped<IGetTotalFixedExpenses, GetTotalFixedExpensesUseCase>();
        return services;
    }
}