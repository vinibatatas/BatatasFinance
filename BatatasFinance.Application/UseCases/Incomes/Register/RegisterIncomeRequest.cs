using System.ComponentModel.DataAnnotations;

namespace BatatasFinance.Application.UseCases.Incomes.Register;

public record RegisterIncomeRequest
{
    [Required]
    public string Description { get; init; }
    [Required]
    public decimal Amount{ get; init; }
    [Required]
    public bool IsRecurring { get; init; }
    [Required]
    public Guid UserId { get; init; }
};