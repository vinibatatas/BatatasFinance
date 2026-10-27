using System.Data;
using BatatasFinance.Domain.Entities;
using BatatasFinance.Domain.Repositories;
using BatatasFinance.Infrastructure.Data;
using Dapper;

namespace BatatasFinance.Infrastructure.Repositories;

public class UserRepository (IDbConnection connection) : IUserRepository
{
    public async Task AddAsync(User user)
    {
        const string sql = @"INSERT INTO Users (Id, UserName, CreatedAt) VALUES (@Id, @UserName, @CreatedAt);";
        await connection.ExecuteAsync(sql, user);
    }
    public async Task<User?> UpdateAsync(User user)
    {
      const string sql = @"UPDATE Users SET UserName = @UserName, CreatedAt = @CreatedAt;";
      await connection.ExecuteAsync(sql, user);
      return user;
    }
    public async Task<User?> GetByIdAsync(Guid userId)
    {
       const string sql = "SELECT * FROM Users WHERE Id = @Id;";
       return await connection.QueryFirstOrDefaultAsync<User>(sql, new { Id = userId});
    }
    
}