using BatatasFinance.Application.UseCases.Incomes.GetAll;
using BatatasFinance.Application.UseCases.Incomes.Register;
using BatatasFinance.Application.UseCases.Incomes.Update;
using BatatasFinance.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BatatasFinance.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IncomesController(IRegisterIncomeUseCase registeUseCase, IGetAllIncomesUseCase getAllIncomesUseCase, ILogger<IncomesController> logger, IUpdateIncomeUseCase updateIncomeUse) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterIncomeRequest request)
    {
        await registeUseCase.ExecuteAsync(request.Description, request.Amount, request.IsRecurring, request.UserId);
        return Created();
    }
    [HttpGet ("users/{userId}")]
    public async Task<IActionResult> GetAll([FromRoute] Guid userId)
    {
        var response = await getAllIncomesUseCase.ExecuteAsync(userId);
        logger.LogInformation("response encontrada"+ response);
        if (response == null)
        {
            return NotFound();
        }
        logger.LogInformation("response encontrada");
        return Ok(response);
    }
    [HttpPut("users/{userId:guid}/incomes/{incomeId:guid}")]
    public async Task<IActionResult> Update(
        [FromRoute] Guid userId,
        [FromRoute] Guid incomeId,
        [FromBody] UpdateIncomeRequest request)
    {
        await updateIncomeUse.ExecuteAsync(incomeId, userId, request);
        return Ok();
    }
}