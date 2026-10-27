using BatatasFinance.Application.UseCases.Expenses.GetAll;
using BatatasFinance.Application.UseCases.Expenses.GetFixedExpenses;
using BatatasFinance.Application.UseCases.Expenses.GetTotalFixedExpenses;
using BatatasFinance.Application.UseCases.Expenses.Register;
using Microsoft.AspNetCore.Mvc;

namespace BatatasFinance.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExpensesController(
    IRegisterExpenseUseCase registerExpenseUseCase,
    IGetAllPurchasesUseCase getAllPurchasesUseCase,
    IGetFixedExpensesUseCase getFixedExpensesUseCase,
    IGetTotalFixedExpenses getTotalFixedExpenses) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterExpenseRequest request)
    {
        try
        {
            await registerExpenseUseCase.ExecuteAsync(request);
            return Created();
        }
        catch(Exception e)
        {
            return BadRequest(new { message = e.Message });
        } 
    }
    [HttpGet ("{userId:guid}")]
    public async Task<IActionResult> GetAll([FromRoute] Guid userId)
    {
        var response = await getAllPurchasesUseCase.ExecuteAsync(userId);
        if (!response.Any())
        {
            return NoContent();
        }
        return Ok(response);
    }
    [HttpGet("fixed/{userId:guid}")]
    public async Task<IActionResult> GetFixed([FromRoute] Guid userId)
    {
        var response = await getFixedExpensesUseCase.ExecuteAsync(userId);
        if (response is null)
        {
            return NoContent();
        }
        return Ok(response);
    }
    [HttpGet("fixed/total/{userId:guid}")]
    public async Task<IActionResult> GetTotal([FromRoute] Guid userId)
    {
        var response = await getTotalFixedExpenses.ExecuteAsync(userId);
        if (response <= 0)
        {
            return NoContent();
        }
        return Ok(response);
    }
}