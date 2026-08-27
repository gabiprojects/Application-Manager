using System.ComponentModel.DataAnnotations;
using ApplicationManagerAPI.Domain.Enums;

namespace ApplicationManagerAPI.Contracts.JobApplications;

public class UpdateJobApplicationStatusRequest
{
    [Required]
    public ApplicationStatus Status { get; set; }

    public string? Notes { get; set; }
}
