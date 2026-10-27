using BatatasFinance.Application.UseCases.Accounts.GetAll;
using BatatasFinance.Application.UseCases.Accounts.Register;
using BatatasFinance.Application.UseCases.CreditCards.Register;
using Microsoft.AspNetCore.Mvc;

namespace BatatasFinance.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AccountController (IRegisterAccountUseCase registerAccountUseCase,IGetAllAccountUseCase getAllAccountUseCase) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterAccountRequest request)
    {
        await registerAccountUseCase.ExecuteAsync(request.AccountName, request.BankName, request.Balance,
            request.UserId);
        return Created();
    }
  [HttpGet("users/{userId}")]
   public async Task<IActionResult> GetAll([FromRoute] Guid userId)
   {
       var response = await getAllAccountUseCase.ExecuteAsync(userId);
       if (!response.Any())
       {
           return NoContent();
       }
       return Ok(response);
   }
}