using System.Data;
using BatatasFinance.Domain.Entities;
using BatatasFinance.Domain.Repositories;
using BatatasFinance.Infrastructure.Data;
using Dapper;
using Microsoft.EntityFrameworkCore;

namespace BatatasFinance.Infrastructure.Repositories;

public class AccountRepository(IDbConnection connection) : IAccountRepository 
{
    public async Task AddAsync(Account account)
    {
        const string sql = @" INSERT INTO Accounts (Id, BankName, Balance, UserId) 
            VALUES (@Id, @BankName, @Balance, @UserId)";
        await connection.ExecuteAsync(sql, account);
    }
    public async Task<IEnumerable<Account?>> GetAllAsync(Guid userId)
    {
       const string sql =  "SELECT * FROM Accounts WHERE UserId = @UserId";
       return await connection.QueryAsync<Account>(sql, new { UserId = userId });
    }
    public Task<Account?> GetByIdAsync(Guid accountId)
    {
        const string sql = "SELECT * FROM Accounts WHERE Id = @Id";
        return connection.QueryFirstOrDefaultAsync<Account>(sql, new { Id = accountId });
    }

    public async Task UpdateAsync(Account account)
    {
        const string sql = "UPDATE Accounts SET Balance = @Balance WHERE Id = @Id";
           await connection.ExecuteAsync(sql, new {account.Balance, account.Id});
    }
}