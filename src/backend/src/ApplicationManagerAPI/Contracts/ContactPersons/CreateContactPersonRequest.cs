using System.ComponentModel.DataAnnotations;

namespace ApplicationManagerAPI.Contracts.ContactPersons;

public class CreateContactPersonRequest
{
    [Required]
    public Guid CompanyId { get; set; }

    public string? FullName { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Role { get; set; }
    public string? Notes { get; set; }
}
