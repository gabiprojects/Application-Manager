using ApplicationManagerAPI.Contracts.Offers;
using ApplicationManagerAPI.Domain.Entities;
using ApplicationManagerAPI.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApplicationManagerAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OfferController : ControllerBase
{
    private readonly ApplicationDbContext _db;

    public OfferController(ApplicationDbContext db)
    {
        _db = db;
    }

    [HttpGet("{jobApplicationId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<OfferDto>> GetByJobApplicationId(Guid jobApplicationId, CancellationToken cancellationToken)
    {
        var offer = await _db.Offers
            .AsNoTracking()
            .FirstOrDefaultAsync(o => o.JobApplicationId == jobApplicationId, cancellationToken);

        if (offer is null)
        {
            return NotFound();
        }

        var dto = new OfferDto
        {
            Id = offer.Id,
            JobApplicationId = offer.JobApplicationId,
            Status = offer.Status,
            AnnualSalary = offer.AnnualSalary,
            Currency = offer.Currency,
            Notes = offer.Notes,
        };

        return Ok(dto);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<OfferDto>> Create(CreateOfferRequest request, CancellationToken cancellationToken)
    {
        var applicationExists = await _db.JobApplications.AnyAsync(ja => ja.Id == request.JobApplicationId, cancellationToken);
        if (!applicationExists)
        {
            return NotFound($"Job application '{request.JobApplicationId}' was not found.");
        }

        var alreadyExists = await _db.Offers.AnyAsync(o => o.JobApplicationId == request.JobApplicationId, cancellationToken);
        if (alreadyExists)
        {
            return Conflict($"An offer already exists for job application '{request.JobApplicationId}'.");
        }

        var offer = new Offer
        {
            Id = Guid.NewGuid(),
            JobApplicationId = request.JobApplicationId,
            Status = request.Status,
            AnnualSalary = request.AnnualSalary,
            Currency = request.Currency,
            Notes = request.Notes,
        };

        _db.Offers.Add(offer);
        await _db.SaveChangesAsync(cancellationToken);

        var dto = new OfferDto
        {
            Id = offer.Id,
            JobApplicationId = offer.JobApplicationId,
            Status = offer.Status,
            AnnualSalary = offer.AnnualSalary,
            Currency = offer.Currency,
            Notes = offer.Notes,
        };

        return CreatedAtAction(nameof(GetByJobApplicationId), new { jobApplicationId = dto.JobApplicationId }, dto);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, UpdateOfferRequest request, CancellationToken cancellationToken)
    {
        var offer = await _db.Offers.FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
        if (offer is null)
        {
            return NotFound();
        }

        offer.Status = request.Status;
        offer.AnnualSalary = request.AnnualSalary;
        offer.Currency = request.Currency;
        offer.Notes = request.Notes;

        await _db.SaveChangesAsync(cancellationToken);

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var offer = await _db.Offers.FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
        if (offer is null)
        {
            return NotFound();
        }

        _db.Offers.Remove(offer);
        await _db.SaveChangesAsync(cancellationToken);

        return NoContent();
    }
}
