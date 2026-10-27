namespace BatatasFinance.Application.UseCases.Users.Register;

public interface IRegisterUserCase
{
    Task<Guid> ExecuteAsync(
        string userName
        );
}