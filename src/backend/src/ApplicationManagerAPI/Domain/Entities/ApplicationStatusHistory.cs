using ApplicationManagerAPI.Domain.Enums;

namespace ApplicationManagerAPI.Domain.Entities;

public class ApplicationStatusHistory
{
    public Guid Id { get; set; }
    public Guid JobApplicationId { get; set; }
    public ApplicationStatus Status { get; set; }
    public DateTime ChangedAtUtc { get; set; }
    public string? Notes { get; set; }

    public JobApplication? JobApplication { get; set; }
}
