using ApplicationManagerAPI.Contracts.ContactPersons;
using ApplicationManagerAPI.Domain.Entities;
using ApplicationManagerAPI.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApplicationManagerAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContactPersonController : ControllerBase
{
    private readonly ApplicationDbContext _db;

    public ContactPersonController(ApplicationDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<ContactPersonDto>>> GetAll([FromQuery] Guid? companyId, CancellationToken cancellationToken)
    {
        var query = _db.ContactPersons.AsNoTracking().AsQueryable();

        if (companyId.HasValue)
        {
            query = query.Where(cp => cp.CompanyId == companyId.Value);
        }

        var contactPersons = await query
            .Select(cp => new ContactPersonDto
            {
                Id = cp.Id,
                CompanyId = cp.CompanyId,
                FullName = cp.FullName,
                Email = cp.Email,
                Phone = cp.Phone,
                Role = cp.Role,
                Notes = cp.Notes,
            })
            .ToListAsync(cancellationToken);

        return Ok(contactPersons);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ContactPersonDto>> Create(CreateContactPersonRequest request, CancellationToken cancellationToken)
    {
        var companyExists = await _db.Companies.AnyAsync(c => c.Id == request.CompanyId, cancellationToken);
        if (!companyExists)
        {
            return NotFound($"Company '{request.CompanyId}' was not found.");
        }

        var contactPerson = new ContactPerson
        {
            Id = Guid.NewGuid(),
            CompanyId = request.CompanyId,
            FullName = request.FullName,
            Email = request.Email,
            Phone = request.Phone,
            Role = request.Role,
            Notes = request.Notes,
        };

        _db.ContactPersons.Add(contactPerson);
        await _db.SaveChangesAsync(cancellationToken);

        var dto = new ContactPersonDto
        {
            Id = contactPerson.Id,
            CompanyId = contactPerson.CompanyId,
            FullName = contactPerson.FullName,
            Email = contactPerson.Email,
            Phone = contactPerson.Phone,
            Role = contactPerson.Role,
            Notes = contactPerson.Notes,
        };

        return CreatedAtAction(nameof(GetAll), new { companyId = dto.CompanyId }, dto);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, UpdateContactPersonRequest request, CancellationToken cancellationToken)
    {
        var contactPerson = await _db.ContactPersons.FirstOrDefaultAsync(cp => cp.Id == id, cancellationToken);
        if (contactPerson is null)
        {
            return NotFound();
        }

        contactPerson.CompanyId = request.CompanyId;
        contactPerson.FullName = request.FullName;
        contactPerson.Email = request.Email;
        contactPerson.Phone = request.Phone;
        contactPerson.Role = request.Role;
        contactPerson.Notes = request.Notes;

        await _db.SaveChangesAsync(cancellationToken);

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var contactPerson = await _db.ContactPersons.FirstOrDefaultAsync(cp => cp.Id == id, cancellationToken);
        if (contactPerson is null)
        {
            return NotFound();
        }

        _db.ContactPersons.Remove(contactPerson);
        await _db.SaveChangesAsync(cancellationToken);

        return NoContent();
    }
}
