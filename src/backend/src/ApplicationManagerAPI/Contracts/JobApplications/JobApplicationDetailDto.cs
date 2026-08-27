using ApplicationManagerAPI.Contracts.Companies;
using ApplicationManagerAPI.Contracts.FollowUps;
using ApplicationManagerAPI.Contracts.Interviews;
using ApplicationManagerAPI.Contracts.Offers;

namespace ApplicationManagerAPI.Contracts.JobApplications;

public class JobApplicationDetailDto : JobApplicationDto
{
    public CompanyDto? Company { get; set; }
    public List<InterviewDto> Interviews { get; set; } = [];
    public List<FollowUpDto> FollowUps { get; set; } = [];
    public OfferDto? Offer { get; set; }
}
