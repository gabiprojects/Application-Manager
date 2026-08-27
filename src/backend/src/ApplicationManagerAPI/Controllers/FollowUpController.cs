using ApplicationManagerAPI.Contracts.FollowUps;
using ApplicationManagerAPI.Domain.Entities;
using ApplicationManagerAPI.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApplicationManagerAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FollowUpController : ControllerBase
{
    private readonly ApplicationDbContext _db;

    public FollowUpController(ApplicationDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<FollowUpDto>>> GetAll([FromQuery] Guid? jobApplicationId, CancellationToken cancellationToken)
    {
        var query = _db.FollowUps.AsNoTracking().AsQueryable();

        if (jobApplicationId.HasValue)
        {
            query = query.Where(f => f.JobApplicationId == jobApplicationId.Value);
        }

        var followUps = await query
            .Select(f => new FollowUpDto
            {
                Id = f.Id,
                JobApplicationId = f.JobApplicationId,
                DueDateUtc = f.DueDateUtc,
                Notes = f.Notes,
                Completed = f.Completed,
            })
            .ToListAsync(cancellationToken);

        return Ok(followUps);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<FollowUpDto>> Create(CreateFollowUpRequest request, CancellationToken cancellationToken)
    {
        var applicationExists = await _db.JobApplications.AnyAsync(ja => ja.Id == request.JobApplicationId, cancellationToken);
        if (!applicationExists)
        {
            return NotFound($"Job application '{request.JobApplicationId}' was not found.");
        }

        var followUp = new FollowUp
        {
            Id = Guid.NewGuid(),
            JobApplicationId = request.JobApplicationId,
            DueDateUtc = request.DueDateUtc,
            Notes = request.Notes,
            Completed = request.Completed,
        };

        _db.FollowUps.Add(followUp);
        await _db.SaveChangesAsync(cancellationToken);

        var dto = new FollowUpDto
        {
            Id = followUp.Id,
            JobApplicationId = followUp.JobApplicationId,
            DueDateUtc = followUp.DueDateUtc,
            Notes = followUp.Notes,
            Completed = followUp.Completed,
        };

        return CreatedAtAction(nameof(GetAll), new { jobApplicationId = dto.JobApplicationId }, dto);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, UpdateFollowUpRequest request, CancellationToken cancellationToken)
    {
        var followUp = await _db.FollowUps.FirstOrDefaultAsync(f => f.Id == id, cancellationToken);
        if (followUp is null)
        {
            return NotFound();
        }

        followUp.JobApplicationId = request.JobApplicationId;
        followUp.DueDateUtc = request.DueDateUtc;
        followUp.Notes = request.Notes;
        followUp.Completed = request.Completed;

        await _db.SaveChangesAsync(cancellationToken);

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var followUp = await _db.FollowUps.FirstOrDefaultAsync(f => f.Id == id, cancellationToken);
        if (followUp is null)
        {
            return NotFound();
        }

        _db.FollowUps.Remove(followUp);
        await _db.SaveChangesAsync(cancellationToken);

        return NoContent();
    }
}
