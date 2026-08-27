using ApplicationManagerAPI.Contracts.ContactPersons;

namespace ApplicationManagerAPI.Contracts.Companies;

public class CompanyDetailDto : CompanyDto
{
    public List<ContactPersonDto> ContactPersons { get; set; } = [];
}
