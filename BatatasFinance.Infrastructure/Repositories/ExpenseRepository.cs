using System.Data;
using Dapper;
using Microsoft.EntityFrameworkCore;
namespace BatatasFinance.Infrastructure.Repositories;
using BatatasFinance.Domain.Entities;
using BatatasFinance.Domain.Repositories;
using BatatasFinance.Infrastructure.Data;
public class ExpenseRepository(IDbConnection connection) : IExpenseRepository
{
    public async Task AddAsync(Expense expense)
    {
        const string sql = @"INSERT INTO Expenses (Id,Description,Amount,Category,PaymentMethod,IsFixed, UserId,CreditCardId,AccountId, Installments,CreatedAt) 
                            VALUES (@Id, @Description, @Amount, @Category, @PaymentMethod, @IsFixed, @UserId, @CreditCardId,@AccountId, @Installments, @CreatedAt ) ";
        await connection.ExecuteAsync(sql,expense);
    }
    public async Task<IEnumerable<Expense?>> GetAllAsync(Guid userId)
    {
        const string sql = @"SELECT * FROM Expenses Where UserId = @userId";
        return await connection.QueryAsync<Expense>(sql, new { userId });
    }
    public async Task<decimal> GetInvoiceAsync(Guid creditCardId, Guid userId)
    {
        const string sql = @"SELECT COALESCE(SUM(Amount), 0) FROM Expenses WHERE CreditCardId = @CreditCardId AND UserId = @userId";
        return await connection.ExecuteScalarAsync<decimal>(sql, new { CreditCardId = creditCardId, userId });
    }
    public async Task<IEnumerable<Expense?>> GetExpensesCardAsync(Guid creditCardId, Guid userId, DateTimeOffset  startDate, DateTimeOffset endDate)
    {
        const string sql = @"SELECT * FROM Expenses WHERE CreditCardId = @CreditCardId AND UserId = @userId AND CreatedAt >= @startDate AND CreatedAt <= @endDate";
        return await connection.QueryAsync<Expense>(sql, new { CreditCardId = creditCardId, userId, startDate, endDate });
    }
    public async Task<IEnumerable<Expense?>> GetFixedExpensesAsync(Guid userId)
    {
        const string sql =
            @"SELECT * FROM Expenses WHERE UserId = @userId AND IsFixed = true ";
        return await connection.QueryAsync<Expense>(sql, new { userId });
    }
    public async Task<IEnumerable<decimal>> GetFixedAmountAsync(Guid userId)
    {
        const string sql = "SELECT Amount FROM Expenses WHERE UserId = @userId AND IsFixed = true";
        return await connection.QueryAsync<decimal>(sql, new { userId });
    }
}