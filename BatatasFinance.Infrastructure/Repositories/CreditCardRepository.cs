using System.Data;
using BatatasFinance.Domain.Entities;
using BatatasFinance.Domain.Repositories;
using BatatasFinance.Infrastructure.Data;
using Dapper;
using Microsoft.EntityFrameworkCore;

namespace BatatasFinance.Infrastructure.Repositories;

public class CreditCardRepository(IDbConnection connection) : ICreditCardRepository
{
    public async Task AddAsync(CreditCard creditCard)
    {
       const string sql =
           @"INSERT INTO CreditCards(Id,Name,CreditLimit,ClosingDay,DueDay,UserId, CreditFlag,CreatedAt) VALUES(@Id,@Name,@CreditLimit,@ClosingDay,@DueDay,@UserId,@CreditFlag,@CreatedAt)";
       await connection.ExecuteAsync(sql, creditCard);
    }
    public async Task<IEnumerable<CreditCard?>> GetAllAsync(Guid userId)
    {
        const string sql = "SELECT * FROM CreditCards  WHERE UserId = @userId";
        return await connection.QueryAsync<CreditCard>(sql, new { userId });
    }
    public async Task<CreditCard?> GetByIdAsync(Guid creditCardId)
    {
        const string sql = "SELECT * FROM CreditCards WHERE Id = @creditCardId";
        return await connection.QueryFirstOrDefaultAsync<CreditCard>(sql, new { CreditCardId = creditCardId });
    }
}