using ApplicationManagerAPI.Domain.Enums;

namespace ApplicationManagerAPI.Domain.Entities;

public class Offer
{
    public Guid Id { get; set; }
    public Guid JobApplicationId { get; set; }
    public OfferStatus Status { get; set; } = OfferStatus.Pending;
    public decimal? AnnualSalary { get; set; }
    public string? Currency { get; set; }
    public string? Notes { get; set; }

    public JobApplication? JobApplication { get; set; }
}
