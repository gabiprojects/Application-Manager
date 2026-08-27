namespace ApplicationManagerAPI.Domain.Entities;

public class FollowUp
{
    public Guid Id { get; set; }
    public Guid JobApplicationId { get; set; }
    public DateTime? DueDateUtc { get; set; }
    public string? Notes { get; set; }
    public bool Completed { get; set; }

    public JobApplication? JobApplication { get; set; }
}
