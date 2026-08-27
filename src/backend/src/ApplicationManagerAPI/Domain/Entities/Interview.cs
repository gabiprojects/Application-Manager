using ApplicationManagerAPI.Domain.Enums;

namespace ApplicationManagerAPI.Domain.Entities;

public class Interview
{
    public Guid Id { get; set; }
    public Guid JobApplicationId { get; set; }
    public InterviewType InterviewType { get; set; }
    public InterviewStatus Status { get; set; } = InterviewStatus.Scheduled;
    public DateTime? ScheduledForUtc { get; set; }
    public string? Location { get; set; }
    public string? Notes { get; set; }

    public JobApplication? JobApplication { get; set; }
}
