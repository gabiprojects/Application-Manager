namespace ApplicationManagerAPI.Domain.Entities;

public class Company
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Industry { get; set; }
    public string? Location { get; set; }
    public string? WebsiteUrl { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAtUtc { get; set; }
    public DateTime UpdatedAtUtc { get; set; }

    public ICollection<ContactPerson> ContactPersons { get; set; } = new List<ContactPerson>();
    public ICollection<JobApplication> JobApplications { get; set; } = new List<JobApplication>();
}
