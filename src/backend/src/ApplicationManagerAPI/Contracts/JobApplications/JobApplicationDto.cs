using ApplicationManagerAPI.Domain.Enums;

namespace ApplicationManagerAPI.Contracts.JobApplications;

public class JobApplicationDto
{
    public Guid Id { get; set; }
    public Guid CompanyId { get; set; }
    public string CompanyName { get; set; } = string.Empty;
    public string PositionTitle { get; set; } = string.Empty;
    public ApplicationStatus Status { get; set; }
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
    public DateTime CreatedAtUtc { get; set; }
    public DateTime UpdatedAtUtc { get; set; }
}
