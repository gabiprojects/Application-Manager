using ApplicationManagerAPI.Domain.Enums;

namespace ApplicationManagerAPI.Contracts.Interviews;

public class InterviewDto
{
    public Guid Id { get; set; }
    public Guid JobApplicationId { get; set; }
    public InterviewType InterviewType { get; set; }
    public InterviewStatus Status { get; set; }
    public DateTime? ScheduledForUtc { get; set; }
    public string? Location { get; set; }
    public string? Notes { get; set; }
}
