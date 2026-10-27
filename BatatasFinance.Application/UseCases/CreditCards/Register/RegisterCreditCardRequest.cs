using BatatasFinance.Domain.Enums;

namespace BatatasFinance.Application.UseCases.CreditCards.Register;

public record RegisterCreditCardRequest
{
        public required string Name { get; init; }
        public decimal CreditLimit { get ;  set; }
        public int ClosingDay { get ; set ; } 
        public int DueDay { get ; set ; }
        public Guid UserId { get ; set ; }
        public CreditFlag CreditFlag { get ; init; }
};