using System.ComponentModel.DataAnnotations;
using ApplicationManagerAPI.Domain.Enums;

namespace ApplicationManagerAPI.Contracts.Interviews;

public class CreateInterviewRequest
{
    [Required]
    public Guid JobApplicationId { get; set; }

    [Required]
    public InterviewType InterviewType { get; set; }

    public InterviewStatus Status { get; set; } = InterviewStatus.Scheduled;
    public DateTime? ScheduledForUtc { get; set; }
    public string? Location { get; set; }
    public string? Notes { get; set; }
}
