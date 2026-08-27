using ApplicationManagerAPI.Domain.Enums;

namespace ApplicationManagerAPI.Contracts.Offers;

public class OfferDto
{
    public Guid Id { get; set; }
    public Guid JobApplicationId { get; set; }
    public OfferStatus Status { get; set; }
    public decimal? AnnualSalary { get; set; }
    public string? Currency { get; set; }
    public string? Notes { get; set; }
}
