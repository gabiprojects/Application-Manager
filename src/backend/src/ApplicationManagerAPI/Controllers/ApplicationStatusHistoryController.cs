using ApplicationManagerAPI.Contracts.ApplicationStatusHistory;
using ApplicationManagerAPI.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApplicationManagerAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ApplicationStatusHistoryController : ControllerBase
{
    private readonly ApplicationDbContext _db;

    public ApplicationStatusHistoryController(ApplicationDbContext db)
    {
        _db = db;
    }

    [HttpGet("{jobApplicationId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<ApplicationStatusHistoryDto>>> GetByJobApplicationId(
        Guid jobApplicationId,
        CancellationToken cancellationToken)
    {
        var history = await _db.ApplicationStatusHistories
            .AsNoTracking()
            .Where(h => h.JobApplicationId == jobApplicationId)
            .OrderBy(h => h.ChangedAtUtc)
            .Select(h => new ApplicationStatusHistoryDto
            {
                Id = h.Id,
                JobApplicationId = h.JobApplicationId,
                Status = h.Status,
                ChangedAtUtc = h.ChangedAtUtc,
                Notes = h.Notes,
            })
            .ToListAsync(cancellationToken);

        return Ok(history);
    }
}
