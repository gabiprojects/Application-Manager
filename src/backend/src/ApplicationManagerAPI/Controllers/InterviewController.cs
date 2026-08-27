using ApplicationManagerAPI.Contracts.Interviews;
using ApplicationManagerAPI.Domain.Entities;
using ApplicationManagerAPI.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApplicationManagerAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InterviewController : ControllerBase
{
    private readonly ApplicationDbContext _db;

    public InterviewController(ApplicationDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<InterviewDto>>> GetAll([FromQuery] Guid? jobApplicationId, CancellationToken cancellationToken)
    {
        var query = _db.Interviews.AsNoTracking().AsQueryable();

        if (jobApplicationId.HasValue)
        {
            query = query.Where(i => i.JobApplicationId == jobApplicationId.Value);
        }

        var interviews = await query
            .Select(i => new InterviewDto
            {
                Id = i.Id,
                JobApplicationId = i.JobApplicationId,
                InterviewType = i.InterviewType,
                Status = i.Status,
                ScheduledForUtc = i.ScheduledForUtc,
                Location = i.Location,
                Notes = i.Notes,
            })
            .ToListAsync(cancellationToken);

        return Ok(interviews);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<InterviewDto>> Create(CreateInterviewRequest request, CancellationToken cancellationToken)
    {
        var applicationExists = await _db.JobApplications.AnyAsync(ja => ja.Id == request.JobApplicationId, cancellationToken);
        if (!applicationExists)
        {
            return NotFound($"Job application '{request.JobApplicationId}' was not found.");
        }

        var interview = new Interview
        {
            Id = Guid.NewGuid(),
            JobApplicationId = request.JobApplicationId,
            InterviewType = request.InterviewType,
            Status = request.Status,
            ScheduledForUtc = request.ScheduledForUtc,
            Location = request.Location,
            Notes = request.Notes,
        };

        _db.Interviews.Add(interview);
        await _db.SaveChangesAsync(cancellationToken);

        var dto = new InterviewDto
        {
            Id = interview.Id,
            JobApplicationId = interview.JobApplicationId,
            InterviewType = interview.InterviewType,
            Status = interview.Status,
            ScheduledForUtc = interview.ScheduledForUtc,
            Location = interview.Location,
            Notes = interview.Notes,
        };

        return CreatedAtAction(nameof(GetAll), new { jobApplicationId = dto.JobApplicationId }, dto);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, UpdateInterviewRequest request, CancellationToken cancellationToken)
    {
        var interview = await _db.Interviews.FirstOrDefaultAsync(i => i.Id == id, cancellationToken);
        if (interview is null)
        {
            return NotFound();
        }

        interview.JobApplicationId = request.JobApplicationId;
        interview.InterviewType = request.InterviewType;
        interview.Status = request.Status;
        interview.ScheduledForUtc = request.ScheduledForUtc;
        interview.Location = request.Location;
        interview.Notes = request.Notes;

        await _db.SaveChangesAsync(cancellationToken);

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var interview = await _db.Interviews.FirstOrDefaultAsync(i => i.Id == id, cancellationToken);
        if (interview is null)
        {
            return NotFound();
        }

        _db.Interviews.Remove(interview);
        await _db.SaveChangesAsync(cancellationToken);

        return NoContent();
    }
}
