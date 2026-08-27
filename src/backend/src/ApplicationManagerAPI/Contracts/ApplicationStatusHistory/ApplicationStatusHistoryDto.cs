using ApplicationManagerAPI.Domain.Enums;

namespace ApplicationManagerAPI.Contracts.ApplicationStatusHistory;

public class ApplicationStatusHistoryDto
{
    public Guid Id { get; set; }
    public Guid JobApplicationId { get; set; }
    public ApplicationStatus Status { get; set; }
    public DateTime ChangedAtUtc { get; set; }
    public string? Notes { get; set; }
}
