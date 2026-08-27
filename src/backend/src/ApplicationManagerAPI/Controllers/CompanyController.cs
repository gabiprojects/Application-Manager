using ApplicationManagerAPI.Contracts.Companies;
using ApplicationManagerAPI.Contracts.ContactPersons;
using ApplicationManagerAPI.Domain.Entities;
using ApplicationManagerAPI.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApplicationManagerAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CompanyController : ControllerBase
{
    private readonly ApplicationDbContext _db;

    public CompanyController(ApplicationDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<CompanyDto>>> GetAll(CancellationToken cancellationToken)
    {
        var companies = await _db.Companies
            .AsNoTracking()
            .Select(c => new CompanyDto
            {
                Id = c.Id,
                Name = c.Name,
                Industry = c.Industry,
                Location = c.Location,
                WebsiteUrl = c.WebsiteUrl,
                Notes = c.Notes,
                ApplicationCount = c.JobApplications.Count,
                ContactPersonCount = c.ContactPersons.Count,
                CreatedAtUtc = c.CreatedAtUtc,
                UpdatedAtUtc = c.UpdatedAtUtc,
            })
            .ToListAsync(cancellationToken);

        return Ok(companies);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CompanyDetailDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var company = await _db.Companies
            .AsNoTracking()
            .Include(c => c.ContactPersons)
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

        if (company is null)
        {
            return NotFound();
        }

        var dto = new CompanyDetailDto
        {
            Id = company.Id,
            Name = company.Name,
            Industry = company.Industry,
            Location = company.Location,
            WebsiteUrl = company.WebsiteUrl,
            Notes = company.Notes,
            ApplicationCount = await _db.JobApplications.CountAsync(ja => ja.CompanyId == id, cancellationToken),
            ContactPersonCount = company.ContactPersons.Count,
            CreatedAtUtc = company.CreatedAtUtc,
            UpdatedAtUtc = company.UpdatedAtUtc,
            ContactPersons = company.ContactPersons.Select(cp => new ContactPersonDto
            {
                Id = cp.Id,
                CompanyId = cp.CompanyId,
                FullName = cp.FullName,
                Email = cp.Email,
                Phone = cp.Phone,
                Role = cp.Role,
                Notes = cp.Notes,
            }).ToList(),
        };

        return Ok(dto);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CompanyDto>> Create(CreateCompanyRequest request, CancellationToken cancellationToken)
    {
        var now = DateTime.UtcNow;
        var company = new Company
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Industry = request.Industry,
            Location = request.Location,
            WebsiteUrl = request.WebsiteUrl,
            Notes = request.Notes,
            CreatedAtUtc = now,
            UpdatedAtUtc = now,
        };

        _db.Companies.Add(company);
        await _db.SaveChangesAsync(cancellationToken);

        var dto = new CompanyDto
        {
            Id = company.Id,
            Name = company.Name,
            Industry = company.Industry,
            Location = company.Location,
            WebsiteUrl = company.WebsiteUrl,
            Notes = company.Notes,
            ApplicationCount = 0,
            ContactPersonCount = 0,
            CreatedAtUtc = company.CreatedAtUtc,
            UpdatedAtUtc = company.UpdatedAtUtc,
        };

        return CreatedAtAction(nameof(GetById), new { id = company.Id }, dto);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, UpdateCompanyRequest request, CancellationToken cancellationToken)
    {
        var company = await _db.Companies.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        if (company is null)
        {
            return NotFound();
        }

        company.Name = request.Name;
        company.Industry = request.Industry;
        company.Location = request.Location;
        company.WebsiteUrl = request.WebsiteUrl;
        company.Notes = request.Notes;
        company.UpdatedAtUtc = DateTime.UtcNow;

        await _db.SaveChangesAsync(cancellationToken);

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var company = await _db.Companies.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        if (company is null)
        {
            return NotFound();
        }

        _db.Companies.Remove(company);
        await _db.SaveChangesAsync(cancellationToken);

        return NoContent();
    }
}
