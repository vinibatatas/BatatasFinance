using BatatasFinance.Application.UseCases.Users.Register;
using Microsoft.AspNetCore.Mvc;

namespace BatatasFinance.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class UsersController (IRegisterUserCase registerUserCase) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterUserRequest request)
    {
        try
        {
            var user = await registerUserCase.ExecuteAsync(request.UserName);
            return Created("Perfil Criado", new {id =user , name = request.UserName } );
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}