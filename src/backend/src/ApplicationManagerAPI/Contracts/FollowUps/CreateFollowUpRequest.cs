using System.ComponentModel.DataAnnotations;

namespace ApplicationManagerAPI.Contracts.FollowUps;

public class CreateFollowUpRequest
{
    [Required]
    public Guid JobApplicationId { get; set; }

    public DateTime? DueDateUtc { get; set; }
    public string? Notes { get; set; }
    public bool Completed { get; set; }
}
