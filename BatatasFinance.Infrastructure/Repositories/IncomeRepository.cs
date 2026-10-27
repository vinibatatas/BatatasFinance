using System.Data;
using BatatasFinance.Domain.Entities;
using BatatasFinance.Domain.Repositories;
using BatatasFinance.Infrastructure.Data;
using Dapper;
using Microsoft.EntityFrameworkCore;

namespace BatatasFinance.Infrastructure.Repositories;

public class IncomeRepository(IDbConnection connection) : IIncomeRepository
{
    public async Task AddAsync(Income income)
    {
      const string sql =@" INSERT INTO Incomes (Id, Description, Amount, IsRecurring, UserId, CreatedAt) VALUES (@Id, @Description, @Amount, @IsRecurring, @UserId, @CreatedAt)";
      await connection.ExecuteAsync(sql, income);
    }
    public async Task<IEnumerable<Income>> GetAllAsync(Guid userId)
    {
        const string sql = @"SELECT * FROM Incomes WHERE UserId = @UserId";
        return await connection.QueryAsync<Income>(sql, new { UserId = userId });
    }
    public async Task<Income?> GetByIdAsync(Guid incomeId, Guid userId)
    {
        const string sql = @"SELECT * FROM Incomes WHERE Id = @incomeId AND UserId = @UserId";
        return await connection.QueryFirstOrDefaultAsync<Income>(sql, new { IncomeId = incomeId, UserId = userId });
    }
    public async Task UpdateAsync(Income income)
    {
        const string sql = @"UPDATE Incomes SET Description = @Description, Amount = @Amount WHERE Id = @Id";
        await connection.ExecuteAsync(sql, income);
    }
}