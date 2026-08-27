namespace ApplicationManagerAPI.Contracts.FollowUps;

public class FollowUpDto
{
    public Guid Id { get; set; }
    public Guid JobApplicationId { get; set; }
    public DateTime? DueDateUtc { get; set; }
    public string? Notes { get; set; }
    public bool Completed { get; set; }
}
