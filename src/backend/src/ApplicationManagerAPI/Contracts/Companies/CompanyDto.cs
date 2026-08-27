namespace ApplicationManagerAPI.Contracts.Companies;

public class CompanyDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Industry { get; set; }
    public string? Location { get; set; }
    public string? WebsiteUrl { get; set; }
    public string? Notes { get; set; }
    public int ApplicationCount { get; set; }
    public int ContactPersonCount { get; set; }
    public DateTime CreatedAtUtc { get; set; }
    public DateTime UpdatedAtUtc { get; set; }
}
