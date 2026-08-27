using System.ComponentModel.DataAnnotations;

namespace ApplicationManagerAPI.Contracts.Companies;

public class UpdateCompanyRequest
{
    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    public string? Industry { get; set; }
    public string? Location { get; set; }
    public string? WebsiteUrl { get; set; }
    public string? Notes { get; set; }
}
