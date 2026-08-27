using System.ComponentModel.DataAnnotations;
using ApplicationManagerAPI.Domain.Enums;

namespace ApplicationManagerAPI.Contracts.JobApplications;

public class UpdateJobApplicationRequest
{
    [Required]
    public Guid CompanyId { get; set; }

    [Required]
    [MaxLength(200)]
    public string PositionTitle { get; set; } = string.Empty;

    public ApplicationStatus Status { get; set; } = ApplicationStatus.Draft;

    [Required]
    public DateTime AppliedOn { get; set; }

    public string? JobPostingUrl { get; set; }
    public string? Location { get; set; }
    public EmploymentType? EmploymentType { get; set; }
    public WorkModel? WorkModel { get; set; }
    public decimal? SalaryMin { get; set; }
    public decimal? SalaryMax { get; set; }
    public string? SalaryCurrency { get; set; }
    public string? Source { get; set; }
    public string? Notes { get; set; }
}
