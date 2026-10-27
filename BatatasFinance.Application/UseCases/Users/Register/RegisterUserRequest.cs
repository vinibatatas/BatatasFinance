namespace BatatasFinance.Application.UseCases.Users.Register;

public record RegisterUserRequest
{
    public required string UserName { get; init; }
};