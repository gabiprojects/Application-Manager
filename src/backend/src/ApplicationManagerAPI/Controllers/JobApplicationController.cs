using ApplicationManagerAPI.Contracts.Companies;
using ApplicationManagerAPI.Contracts.FollowUps;
using ApplicationManagerAPI.Contracts.Interviews;
using ApplicationManagerAPI.Contracts.JobApplications;
using ApplicationManagerAPI.Contracts.Offers;
using ApplicationManagerAPI.Domain.Entities;
using ApplicationManagerAPI.Domain.Enums;
using ApplicationManagerAPI.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApplicationStatusHistoryEntity = ApplicationManagerAPI.Domain.Entities.ApplicationStatusHistory;

namespace ApplicationManagerAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class JobApplicationController : ControllerBase
{
    private readonly ApplicationDbContext _db;

    public JobApplicationController(ApplicationDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<JobApplicationDto>>> GetAll(
        [FromQuery] ApplicationStatus? status,
        [FromQuery] Guid? companyId,
        CancellationToken cancellationToken)
    {
        var query = _db.JobApplications.AsNoTracking().Include(ja => ja.Company).AsQueryable();

        if (status.HasValue)
        {
            query = query.Where(ja => ja.Status == status.Value);
        }

        if (companyId.HasValue)
        {
            query = query.Where(ja => ja.CompanyId == companyId.Value);
        }

        var applications = await query
            .OrderByDescending(ja => ja.AppliedOn)
            .Select(ja => ToDto(ja))
            .ToListAsync(cancellationToken);

        return Ok(applications);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<JobApplicationDetailDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var application = await _db.JobApplications
            .AsNoTracking()
            .Include(ja => ja.Company)
            .Include(ja => ja.Interviews)
            .Include(ja => ja.FollowUps)
            .Include(ja => ja.Offer)
            .FirstOrDefaultAsync(ja => ja.Id == id, cancellationToken);

        if (application is null)
        {
            return NotFound();
        }

        var dto = new JobApplicationDetailDto
        {
            Id = application.Id,
            CompanyId = application.CompanyId,
            CompanyName = application.Company?.Name ?? string.Empty,
            PositionTitle = application.PositionTitle,
            Status = application.Status,
            AppliedOn = application.AppliedOn,
            JobPostingUrl = application.JobPostingUrl,
            Location = application.Location,
            EmploymentType = application.EmploymentType,
            WorkModel = application.WorkModel,
            SalaryMin = application.SalaryMin,
            SalaryMax = application.SalaryMax,
            SalaryCurrency = application.SalaryCurrency,
            Source = application.Source,
            Notes = application.Notes,
            CreatedAtUtc = application.CreatedAtUtc,
            UpdatedAtUtc = application.UpdatedAtUtc,
            Company = application.Company is null
                ? null
                : new CompanyDto
                {
                    Id = application.Company.Id,
                    Name = application.Company.Name,
                    Industry = application.Company.Industry,
                    Location = application.Company.Location,
                    WebsiteUrl = application.Company.WebsiteUrl,
                    Notes = application.Company.Notes,
                    CreatedAtUtc = application.Company.CreatedAtUtc,
                    UpdatedAtUtc = application.Company.UpdatedAtUtc,
                },
            Interviews = application.Interviews.Select(i => new InterviewDto
            {
                Id = i.Id,
                JobApplicationId = i.JobApplicationId,
                InterviewType = i.InterviewType,
                Status = i.Status,
                ScheduledForUtc = i.ScheduledForUtc,
                Location = i.Location,
                Notes = i.Notes,
            }).ToList(),
            FollowUps = application.FollowUps.Select(f => new FollowUpDto
            {
                Id = f.Id,
                JobApplicationId = f.JobApplicationId,
                DueDateUtc = f.DueDateUtc,
                Notes = f.Notes,
                Completed = f.Completed,
            }).ToList(),
            Offer = application.Offer is null
                ? null
                : new OfferDto
                {
                    Id = application.Offer.Id,
                    JobApplicationId = application.Offer.JobApplicationId,
                    Status = application.Offer.Status,
                    AnnualSalary = application.Offer.AnnualSalary,
                    Currency = application.Offer.Currency,
                    Notes = application.Offer.Notes,
                },
        };

        return Ok(dto);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<JobApplicationDto>> Create(CreateJobApplicationRequest request, CancellationToken cancellationToken)
    {
        var company = await _db.Companies.FirstOrDefaultAsync(c => c.Id == request.CompanyId, cancellationToken);
        if (company is null)
        {
            return NotFound($"Company '{request.CompanyId}' was not found.");
        }

        var now = DateTime.UtcNow;
        var application = new JobApplication
        {
            Id = Guid.NewGuid(),
            CompanyId = request.CompanyId,
            PositionTitle = request.PositionTitle,
            Status = request.Status,
            AppliedOn = request.AppliedOn,
            JobPostingUrl = request.JobPostingUrl,
            Location = request.Location,
            EmploymentType = request.EmploymentType,
            WorkModel = request.WorkModel,
            SalaryMin = request.SalaryMin,
            SalaryMax = request.SalaryMax,
            SalaryCurrency = request.SalaryCurrency,
            Source = request.Source,
            Notes = request.Notes,
            CreatedAtUtc = now,
            UpdatedAtUtc = now,
        };

        _db.JobApplications.Add(application);

        _db.ApplicationStatusHistories.Add(new ApplicationStatusHistoryEntity
        {
            Id = Guid.NewGuid(),
            JobApplicationId = application.Id,
            Status = application.Status,
            ChangedAtUtc = now,
        });

        await _db.SaveChangesAsync(cancellationToken);

        var dto = new JobApplicationDto
        {
            Id = application.Id,
            CompanyId = application.CompanyId,
            CompanyName = company.Name,
            PositionTitle = application.PositionTitle,
            Status = application.Status,
            AppliedOn = application.AppliedOn,
            JobPostingUrl = application.JobPostingUrl,
            Location = application.Location,
            EmploymentType = application.EmploymentType,
            WorkModel = application.WorkModel,
            SalaryMin = application.SalaryMin,
            SalaryMax = application.SalaryMax,
            SalaryCurrency = application.SalaryCurrency,
            Source = application.Source,
            Notes = application.Notes,
            CreatedAtUtc = application.CreatedAtUtc,
            UpdatedAtUtc = application.UpdatedAtUtc,
        };

        return CreatedAtAction(nameof(GetById), new { id = application.Id }, dto);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, UpdateJobApplicationRequest request, CancellationToken cancellationToken)
    {
        var application = await _db.JobApplications.FirstOrDefaultAsync(ja => ja.Id == id, cancellationToken);
        if (application is null)
        {
            return NotFound();
        }

        var companyExists = await _db.Companies.AnyAsync(c => c.Id == request.CompanyId, cancellationToken);
        if (!companyExists)
        {
            return NotFound($"Company '{request.CompanyId}' was not found.");
        }

        application.CompanyId = request.CompanyId;
        application.PositionTitle = request.PositionTitle;
        application.Status = request.Status;
        application.AppliedOn = request.AppliedOn;
        application.JobPostingUrl = request.JobPostingUrl;
        application.Location = request.Location;
        application.EmploymentType = request.EmploymentType;
        application.WorkModel = request.WorkModel;
        application.SalaryMin = request.SalaryMin;
        application.SalaryMax = request.SalaryMax;
        application.SalaryCurrency = request.SalaryCurrency;
        application.Source = request.Source;
        application.Notes = request.Notes;
        application.UpdatedAtUtc = DateTime.UtcNow;

        await _db.SaveChangesAsync(cancellationToken);

        return NoContent();
    }

    [HttpPatch("{id:guid}/status")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ChangeStatus(Guid id, UpdateJobApplicationStatusRequest request, CancellationToken cancellationToken)
    {
        var application = await _db.JobApplications.FirstOrDefaultAsync(ja => ja.Id == id, cancellationToken);
        if (application is null)
        {
            return NotFound();
        }

        var now = DateTime.UtcNow;
        application.Status = request.Status;
        application.UpdatedAtUtc = now;

        _db.ApplicationStatusHistories.Add(new ApplicationStatusHistoryEntity
        {
            Id = Guid.NewGuid(),
            JobApplicationId = application.Id,
            Status = request.Status,
            ChangedAtUtc = now,
            Notes = request.Notes,
        });

        await _db.SaveChangesAsync(cancellationToken);

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var application = await _db.JobApplications.FirstOrDefaultAsync(ja => ja.Id == id, cancellationToken);
        if (application is null)
        {
            return NotFound();
        }

        _db.JobApplications.Remove(application);
        await _db.SaveChangesAsync(cancellationToken);

        return NoContent();
    }

    private static JobApplicationDto ToDto(JobApplication ja) => new()
    {
        Id = ja.Id,
        CompanyId = ja.CompanyId,
        CompanyName = ja.Company != null ? ja.Company.Name : string.Empty,
        PositionTitle = ja.PositionTitle,
        Status = ja.Status,
        AppliedOn = ja.AppliedOn,
        JobPostingUrl = ja.JobPostingUrl,
        Location = ja.Location,
        EmploymentType = ja.EmploymentType,
        WorkModel = ja.WorkModel,
        SalaryMin = ja.SalaryMin,
        SalaryMax = ja.SalaryMax,
        SalaryCurrency = ja.SalaryCurrency,
        Source = ja.Source,
        Notes = ja.Notes,
        CreatedAtUtc = ja.CreatedAtUtc,
        UpdatedAtUtc = ja.UpdatedAtUtc,
    };
}
