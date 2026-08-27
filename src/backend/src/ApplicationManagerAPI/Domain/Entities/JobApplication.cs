using ApplicationManagerAPI.Domain.Enums;

namespace ApplicationManagerAPI.Domain.Entities;

public class JobApplication
{
    public Guid Id { get; set; }
    public Guid CompanyId { get; set; }
    public string PositionTitle { get; set; } = string.Empty;
    public ApplicationStatus Status { get; set; } = ApplicationStatus.Draft;
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

    public Company? Company { get; set; }
    public ICollection<Interview> Interviews { get; set; } = new List<Interview>();
    public ICollection<FollowUp> FollowUps { get; set; } = new List<FollowUp>();
    public Offer? Offer { get; set; }
    public ICollection<ApplicationStatusHistory> StatusHistory { get; set; } = new List<ApplicationStatusHistory>();
}
