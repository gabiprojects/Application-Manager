using System.ComponentModel.DataAnnotations;
using ApplicationManagerAPI.Domain.Enums;

namespace ApplicationManagerAPI.Contracts.Offers;

public class UpdateOfferRequest
{
    [Required]
    public Guid JobApplicationId { get; set; }

    public OfferStatus Status { get; set; } = OfferStatus.Pending;
    public decimal? AnnualSalary { get; set; }
    public string? Currency { get; set; }
    public string? Notes { get; set; }
}
