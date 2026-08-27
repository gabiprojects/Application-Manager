using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApplicationManagerAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedMockData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Companies
            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Name", "Industry", "Location", "WebsiteUrl", "Notes", "CreatedAtUtc", "UpdatedAtUtc" },
                values: new object[,]
                {
                    { G("10000000-0000-0000-0000-000000000001"), "Microsoft Switzerland", "Technology", "Zurich, Switzerland", "https://www.microsoft.com/de-ch", "Enterprise software and cloud services.", D(2025, 9, 2), D(2026, 5, 12) },
                    { G("10000000-0000-0000-0000-000000000002"), "Swiss Life", "Insurance and Financial Services", "Zurich, Switzerland", "https://www.swisslife.ch", "Swiss life insurance and pension provider.", D(2025, 9, 10), D(2026, 4, 18) },
                    { G("10000000-0000-0000-0000-000000000003"), "Digitec Galaxus", "E-commerce", "Zurich, Switzerland", "https://www.galaxus.ch", "Swiss online retailer with a large engineering organization.", D(2025, 10, 1), D(2026, 6, 3) },
                    { G("10000000-0000-0000-0000-000000000004"), "Swisscom", "Telecommunications", "Bern, Switzerland", "https://www.swisscom.ch", "Telecommunications and digital services provider.", D(2025, 10, 15), D(2026, 7, 8) },
                    { G("10000000-0000-0000-0000-000000000005"), "UBS", "Banking", "Zurich, Switzerland", "https://www.ubs.com", "Global banking and financial services group.", D(2025, 11, 3), D(2026, 3, 27) },
                    { G("10000000-0000-0000-0000-000000000006"), "Zurich Insurance", "Insurance", "Zurich, Switzerland", "https://www.zurich.com", "International multiline insurer.", D(2025, 11, 20), D(2026, 2, 16) },
                    { G("10000000-0000-0000-0000-000000000007"), "Google Switzerland", "Technology", "Zurich, Switzerland", "https://about.google/locations/zurich", "Engineering site focused on search, cloud, and infrastructure.", D(2025, 12, 4), D(2026, 6, 24) },
                    { G("10000000-0000-0000-0000-000000000008"), "Sunrise", "Telecommunications", "Opfikon, Switzerland", "https://www.sunrise.ch", "Telecommunications provider serving private and business customers.", D(2026, 1, 8), D(2026, 5, 5) },
                    { G("10000000-0000-0000-0000-000000000009"), "Avaloq", "Financial Technology", "Zurich, Switzerland", "https://www.avaloq.com", "Core banking software and digital banking solutions.", D(2026, 1, 22), D(2026, 7, 17) },
                    { G("10000000-0000-0000-0000-000000000010"), "Beekeeper", "Workplace Technology", "Zurich, Switzerland", "https://www.beekeeper.io", "Digital workplace platform for frontline organizations.", D(2026, 2, 2), D(2026, 8, 4) },
                    { G("10000000-0000-0000-0000-000000000011"), "Sonar", "Developer Tools", "Geneva, Switzerland", "https://www.sonarsource.com", "Code quality and application security tooling.", D(2026, 2, 19), D(2026, 5, 29) },
                    { G("10000000-0000-0000-0000-000000000012"), "SBB CFF FFS", "Transportation", "Bern, Switzerland", "https://www.sbb.ch", "National railway operator with extensive digital products.", D(2026, 3, 5), D(2026, 8, 25) }
                });

            // Contact Persons
            migrationBuilder.InsertData(
                table: "ContactPersons",
                columns: new[] { "Id", "CompanyId", "FullName", "Email", "Phone", "Role", "Notes" },
                values: new object[,]
                {
                    { G("30000000-0000-0000-0000-000000000001"), G("10000000-0000-0000-0000-000000000001"), "Nina Keller", "nina.keller@example.test", "+41 44 555 01 01", "Technical Recruiter", "Met at the Zurich developer conference." },
                    { G("30000000-0000-0000-0000-000000000002"), G("10000000-0000-0000-0000-000000000002"), "Marco Frei", "marco.frei@example.test", "+41 43 555 01 02", "Talent Acquisition Partner", "Primary contact for engineering roles." },
                    { G("30000000-0000-0000-0000-000000000003"), G("10000000-0000-0000-0000-000000000003"), "Lea Baumann", "lea.baumann@example.test", null, "Engineering Recruiter", "Prefers email communication." },
                    { G("30000000-0000-0000-0000-000000000004"), G("10000000-0000-0000-0000-000000000004"), "David Roth", "david.roth@example.test", "+41 58 555 01 04", "Hiring Manager", "Leads the cloud platform team." },
                    { G("30000000-0000-0000-0000-000000000005"), G("10000000-0000-0000-0000-000000000005"), "Sara Meier", "sara.meier@example.test", null, "Campus Recruiter", "Contacted through LinkedIn." },
                    { G("30000000-0000-0000-0000-000000000006"), G("10000000-0000-0000-0000-000000000007"), "Jonas Schmid", "jonas.schmid@example.test", null, "Recruiter", "Coordinated the video interviews." },
                    { G("30000000-0000-0000-0000-000000000007"), G("10000000-0000-0000-0000-000000000009"), "Elena Rossi", "elena.rossi@example.test", "+41 44 555 01 07", "People Partner", "Recruiting contact for backend engineering." },
                    { G("30000000-0000-0000-0000-000000000008"), G("10000000-0000-0000-0000-000000000010"), "Lukas Widmer", "lukas.widmer@example.test", null, "VP Engineering", "Introduced by a former colleague." },
                    { G("30000000-0000-0000-0000-000000000009"), G("10000000-0000-0000-0000-000000000011"), "Camille Dubois", "camille.dubois@example.test", "+41 22 555 01 09", "Talent Partner", "Contact for the Geneva engineering office." },
                    { G("30000000-0000-0000-0000-000000000010"), G("10000000-0000-0000-0000-000000000012"), "Anna Müller", "anna.mueller@example.test", null, "IT Recruiter", "Sent details about the technical assessment." }
                });

            // Job Applications
            migrationBuilder.InsertData(
                table: "JobApplications",
                columns: new[] { "Id", "CompanyId", "PositionTitle", "Status", "AppliedOn", "JobPostingUrl", "Location", "EmploymentType", "WorkModel", "SalaryMin", "SalaryMax", "SalaryCurrency", "Source", "Notes", "CreatedAtUtc", "UpdatedAtUtc" },
                values: new object[,]
                {
                    { J(1), C(1), "Cloud Software Engineer", "Offer", D(2026, 5, 3), "https://careers.example.test/microsoft/cloud-software-engineer", "Zurich", "FullTime", "Hybrid", 120000m, 140000m, "CHF", "Company careers page", "Azure platform team; strong C# focus.", D(2026, 5, 2), D(2026, 5, 27) },
                    { J(2), C(1), "Junior .NET Developer", "Rejected", D(2025, 10, 6), "https://careers.example.test/microsoft/junior-dotnet", "Zurich", "FullTime", "Hybrid", 90000m, 105000m, "CHF", "LinkedIn", "Graduate developer role.", D(2025, 10, 5), D(2025, 10, 24) },
                    { J(3), C(1), "DevOps Engineer", "Reviewing", D(2026, 8, 21), "https://careers.example.test/microsoft/devops", "Zurich", "FullTime", "Remote", 115000m, 135000m, "CHF", "Referral", "Recently submitted referral.", D(2026, 8, 20), D(2026, 8, 25) },
                    { J(4), C(2), "C# Developer", "Interviewing", D(2026, 6, 9), "https://careers.example.test/swisslife/csharp", "Zurich", "FullTime", "Hybrid", 105000m, 125000m, "CHF", "Jobs.ch", "Policy platform modernization.", D(2026, 6, 8), D(2026, 6, 23) },
                    { J(5), C(2), "Full Stack Developer", "Rejected", D(2025, 11, 17), null, "Zurich", "FullTime", "Hybrid", 100000m, 120000m, "CHF", "Recruiter", "React and .NET role.", D(2025, 11, 16), D(2025, 12, 4) },
                    { J(6), C(3), "Backend Developer", "Hired", D(2026, 1, 12), "https://careers.example.test/galaxus/backend", "Zurich", "FullTime", "Hybrid", 110000m, 130000m, "CHF", "Tech meetup", "Commerce platform team.", D(2026, 1, 10), D(2026, 2, 20) },
                    { J(7), C(3), "Web Developer", "Applied", D(2026, 8, 26), "https://careers.example.test/galaxus/web", "Zurich", "FullTime", "Hybrid", 95000m, 115000m, "CHF", "Company careers page", "Frontend-heavy product role.", D(2026, 8, 25), D(2026, 8, 26) },
                    { J(8), C(3), "Software Engineer - Search", "Rejected", D(2026, 3, 2), null, "Zurich", "FullTime", "OnSite", 115000m, 135000m, "CHF", "LinkedIn", "Search relevance team.", D(2026, 3, 1), D(2026, 3, 19) },
                    { J(9), C(4), "Cloud Engineer", "Interviewing", D(2026, 7, 6), "https://careers.example.test/swisscom/cloud", "Bern", "FullTime", "Hybrid", 110000m, 130000m, "CHF", "Jobs.ch", "Kubernetes platform engineering.", D(2026, 7, 5), D(2026, 7, 29) },
                    { J(10), C(4), "Angular Developer", "Rejected", D(2025, 9, 15), null, "Bern", "Contract", "Remote", 950m, 1100m, "CHF/day", "Agency", "Six-month customer portal contract.", D(2025, 9, 14), D(2025, 10, 2) },
                    { J(11), C(5), "Software Engineer", "Reviewing", D(2026, 8, 11), "https://careers.example.test/ubs/software-engineer", "Zurich", "FullTime", "Hybrid", 115000m, 135000m, "CHF", "Company careers page", "Wealth management engineering.", D(2026, 8, 10), D(2026, 8, 18) },
                    { J(12), C(5), "Backend .NET Engineer", "Rejected", D(2026, 2, 3), null, "Zurich", "FullTime", "OnSite", 110000m, 130000m, "CHF", "Referral", "Risk technology team.", D(2026, 2, 2), D(2026, 2, 25) },
                    { J(13), C(6), "API Developer", "Draft", D(2026, 8, 28), "https://careers.example.test/zurich/api-developer", "Zurich", "FullTime", "Hybrid", 105000m, 125000m, "CHF", "Company careers page", "CV still needs tailoring before submission.", D(2026, 8, 27), D(2026, 8, 27) },
                    { J(14), C(6), "Integration Engineer", "Rejected", D(2025, 12, 8), null, "Zurich", "FullTime", "Hybrid", 105000m, 120000m, "CHF", "Recruiter", "Enterprise integration role.", D(2025, 12, 7), D(2026, 1, 6) },
                    { J(15), C(7), "Site Reliability Engineer", "Interviewing", D(2026, 4, 13), "https://careers.example.test/google/sre", "Zurich", "FullTime", "Hybrid", 130000m, 160000m, "CHF", "Company careers page", "Infrastructure reliability role.", D(2026, 4, 11), D(2026, 5, 7) },
                    { J(16), C(7), "Software Engineer - Cloud", "Rejected", D(2025, 9, 22), null, "Zurich", "FullTime", "OnSite", 125000m, 155000m, "CHF", "Referral", "Cloud storage team.", D(2025, 9, 20), D(2025, 10, 17) },
                    { J(17), C(8), "Full Stack Developer", "Applied", D(2026, 8, 24), "https://careers.example.test/sunrise/fullstack", "Opfikon", "FullTime", "Hybrid", 100000m, 120000m, "CHF", "LinkedIn", "Customer self-service applications.", D(2026, 8, 23), D(2026, 8, 24) },
                    { J(18), C(8), "Platform Engineer", "Rejected", D(2026, 1, 26), null, "Opfikon", "FullTime", "OnSite", 110000m, 128000m, "CHF", "Jobs.ch", "Internal developer platform.", D(2026, 1, 25), D(2026, 2, 10) },
                    { J(19), C(9), "Senior C# Developer", "Offer", D(2026, 3, 9), "https://careers.example.test/avaloq/csharp", "Zurich", "FullTime", "Hybrid", 125000m, 145000m, "CHF", "Recruiter", "Core banking services.", D(2026, 3, 8), D(2026, 4, 17) },
                    { J(20), C(9), "DevOps Engineer", "Reviewing", D(2026, 7, 20), null, "Zurich", "FullTime", "Remote", 115000m, 135000m, "CHF", "LinkedIn", "CI/CD and observability platform.", D(2026, 7, 19), D(2026, 7, 25) },
                    { J(21), C(10), "Backend Developer", "Interviewing", D(2026, 5, 18), "https://careers.example.test/beekeeper/backend", "Zurich", "FullTime", "Hybrid", 110000m, 130000m, "CHF", "Former colleague", "Messaging services team.", D(2026, 5, 17), D(2026, 6, 10) },
                    { J(22), C(10), "Junior Software Engineer", "Rejected", D(2025, 12, 1), null, "Zurich", "FullTime", "Remote", 85000m, 100000m, "CHF", "University job board", "Early-career product engineering role.", D(2025, 11, 30), D(2025, 12, 15) },
                    { J(23), C(11), ".NET Platform Engineer", "Offer", D(2026, 2, 16), "https://careers.example.test/sonar/dotnet-platform", "Geneva", "FullTime", "Hybrid", 120000m, 140000m, "CHF", "LinkedIn", "Developer tooling and static analysis.", D(2026, 2, 15), D(2026, 3, 31) },
                    { J(24), C(12), "Software Engineer - Passenger Systems", "Reviewing", D(2026, 8, 4), "https://careers.example.test/sbb/passenger-systems", "Bern", "FullTime", "Hybrid", 105000m, 125000m, "CHF", "Company careers page", "Journey planning services.", D(2026, 8, 3), D(2026, 8, 12) },
                    { J(25), C(12), "Cloud Native Developer", "Applied", D(2026, 8, 27), "https://careers.example.test/sbb/cloud-native", "Bern", "PartTime", "Hybrid", 90000m, 110000m, "CHF", "Jobs.ch", "Eighty-percent cloud-native role.", D(2026, 8, 26), D(2026, 8, 27) }
                });

            // Application Status History
            migrationBuilder.InsertData(
                table: "ApplicationStatusHistories",
                columns: new[] { "Id", "JobApplicationId", "Status", "ChangedAtUtc", "Notes" },
                values: new object[,]
                {
                    { H(1), J(1), "Applied", D(2026,5,3,9), "Application submitted." }, { H(2), J(1), "Reviewing", D(2026,5,8,14), "Recruiter screening completed." }, { H(3), J(1), "Interviewing", D(2026,5,15,10), "First technical interview scheduled." }, { H(4), J(1), "Interviewing", D(2026,5,22,13), "Advanced to second interview." }, { H(5), J(1), "Offer", D(2026,5,27,16), "Written offer received." },
                    { H(6), J(2), "Applied", D(2025,10,6,8), null }, { H(7), J(2), "Reviewing", D(2025,10,10,11), null }, { H(8), J(2), "Rejected", D(2025,10,24,15), "Position filled by another candidate." },
                    { H(9), J(3), "Applied", D(2026,8,21,10), null }, { H(10), J(3), "Reviewing", D(2026,8,25,9), "Application is with the hiring team." },
                    { H(11), J(4), "Applied", D(2026,6,9,9), null }, { H(12), J(4), "Reviewing", D(2026,6,13,12), null }, { H(13), J(4), "Interviewing", D(2026,6,23,15), "Invited to technical interview." },
                    { H(14), J(5), "Applied", D(2025,11,17,10), null }, { H(15), J(5), "Rejected", D(2025,12,4,16), null },
                    { H(16), J(6), "Applied", D(2026,1,12,8), null }, { H(17), J(6), "Reviewing", D(2026,1,16,13), null }, { H(18), J(6), "Interviewing", D(2026,1,23,10), null }, { H(19), J(6), "Offer", D(2026,2,12,14), "Offer received after final interview." }, { H(20), J(6), "Hired", D(2026,2,20,9), "Accepted offer and signed contract." },
                    { H(21), J(7), "Applied", D(2026,8,26,11), "Application confirmation received." },
                    { H(22), J(8), "Applied", D(2026,3,2,9), null }, { H(23), J(8), "Reviewing", D(2026,3,6,13), null }, { H(24), J(8), "Rejected", D(2026,3,19,16), null },
                    { H(25), J(9), "Applied", D(2026,7,6,8), null }, { H(26), J(9), "Reviewing", D(2026,7,10,10), null }, { H(27), J(9), "Interviewing", D(2026,7,18,14), "Phone screen completed." }, { H(28), J(9), "Interviewing", D(2026,7,29,11), "Second technical round scheduled." },
                    { H(29), J(10), "Applied", D(2025,9,15,9), null }, { H(30), J(10), "Rejected", D(2025,10,2,15), null },
                    { H(31), J(11), "Applied", D(2026,8,11,10), null }, { H(32), J(11), "Reviewing", D(2026,8,18,9), null },
                    { H(33), J(12), "Applied", D(2026,2,3,8), null }, { H(34), J(12), "Reviewing", D(2026,2,9,12), null }, { H(35), J(12), "Interviewing", D(2026,2,17,10), null }, { H(36), J(12), "Rejected", D(2026,2,25,17), "Hiring team selected another profile." },
                    { H(37), J(14), "Applied", D(2025,12,8,9), null }, { H(38), J(14), "Reviewing", D(2025,12,15,13), null }, { H(39), J(14), "Rejected", D(2026,1,6,10), null },
                    { H(40), J(15), "Applied", D(2026,4,13,8), null }, { H(41), J(15), "Reviewing", D(2026,4,17,15), null }, { H(42), J(15), "Interviewing", D(2026,4,28,10), "Recruiter screen passed." }, { H(43), J(15), "Interviewing", D(2026,5,7,14), "On-site loop completed; awaiting decision." },
                    { H(44), J(16), "Applied", D(2025,9,22,8), null }, { H(45), J(16), "Reviewing", D(2025,9,29,12), null }, { H(46), J(16), "Rejected", D(2025,10,17,16), null },
                    { H(47), J(17), "Applied", D(2026,8,24,10), null },
                    { H(48), J(18), "Applied", D(2026,1,26,9), null }, { H(49), J(18), "Rejected", D(2026,2,10,14), null },
                    { H(50), J(19), "Applied", D(2026,3,9,8), null }, { H(51), J(19), "Reviewing", D(2026,3,13,11), null }, { H(52), J(19), "Interviewing", D(2026,3,20,9), null }, { H(53), J(19), "Interviewing", D(2026,4,2,13), "Final panel completed." }, { H(54), J(19), "Offer", D(2026,4,17,15), "Offer is under consideration." },
                    { H(55), J(20), "Applied", D(2026,7,20,10), null }, { H(56), J(20), "Reviewing", D(2026,7,25,11), null },
                    { H(57), J(21), "Applied", D(2026,5,18,9), null }, { H(58), J(21), "Reviewing", D(2026,5,22,13), null }, { H(59), J(21), "Interviewing", D(2026,5,29,10), null }, { H(60), J(21), "Interviewing", D(2026,6,10,14), "Pair-programming round completed." },
                    { H(61), J(22), "Applied", D(2025,12,1,8), null }, { H(62), J(22), "Rejected", D(2025,12,15,16), null },
                    { H(63), J(23), "Applied", D(2026,2,16,9), null }, { H(64), J(23), "Reviewing", D(2026,2,20,12), null }, { H(65), J(23), "Interviewing", D(2026,3,2,10), null }, { H(66), J(23), "Interviewing", D(2026,3,16,13), "Second interview with engineering director." }, { H(67), J(23), "Offer", D(2026,3,31,15), "Pending response." },
                    { H(68), J(24), "Applied", D(2026,8,4,9), null }, { H(69), J(24), "Reviewing", D(2026,8,12,11), "Technical assessment requested." },
                    { H(70), J(25), "Applied", D(2026,8,27,10), null }
                });

            // Interviews
            migrationBuilder.InsertData(
                table: "Interviews",
                columns: new[] { "Id", "JobApplicationId", "InterviewType", "Status", "ScheduledForUtc", "Location", "Notes" },
                values: new object[,]
                {
                    { I(1), J(1), "Video", "Completed", D(2026,5,15,10), "Microsoft Teams", "Technical screening with two engineers." }, { I(2), J(1), "OnSite", "Completed", D(2026,5,22,13), "Zurich office", "System design and team fit." },
                    { I(3), J(4), "Video", "Scheduled", D(2026,9,2,9), "Microsoft Teams", "Ninety-minute coding interview." },
                    { I(4), J(6), "Video", "Completed", D(2026,1,23,10), "Google Meet", "Backend architecture discussion." }, { I(5), J(6), "OnSite", "Completed", D(2026,2,5,13), "Zurich office", "Final interview loop." },
                    { I(6), J(9), "Phone", "Completed", D(2026,7,18,14), null, "Recruiter and hiring-manager screen." }, { I(7), J(9), "Video", "Scheduled", D(2026,9,3,10), "Webex", "Kubernetes troubleshooting exercise." },
                    { I(8), J(15), "Video", "Completed", D(2026,4,28,10), "Google Meet", "Technical phone screen." }, { I(9), J(15), "OnSite", "Completed", D(2026,5,7,14), "Zurich office", "On-site interview loop." },
                    { I(10), J(19), "OnSite", "Completed", D(2026,4,2,13), "Zurich office", "Architecture and leadership panel." },
                    { I(11), J(21), "Video", "Completed", D(2026,6,10,14), "Zoom", "Pair-programming exercise." },
                    { I(12), J(23), "OnSite", "Completed", D(2026,3,16,13), "Geneva office", "Final engineering and culture interviews." }
                });

            // Follow Ups
            migrationBuilder.InsertData(
                table: "FollowUps",
                columns: new[] { "Id", "JobApplicationId", "DueDateUtc", "Notes", "Completed" },
                values: new object[,]
                {
                    { F(1), J(1), D(2026,5,16,9), "Send thank-you note after technical interview.", true }, { F(2), J(3), D(2026,9,1,9), "Ask recruiter about review timeline.", false },
                    { F(3), J(4), D(2026,9,3,9), "Send code samples after interview.", false }, { F(4), J(9), D(2026,9,4,9), "Follow up after second technical round.", false },
                    { F(5), J(11), D(2026,8,31,9), "Check whether the team needs additional documents.", false }, { F(6), J(15), D(2026,5,8,9), "Thank interview panel.", true },
                    { F(7), J(19), D(2026,4,22,9), "Discuss offer details with recruiter.", true }, { F(8), J(20), D(2026,9,2,9), "Request an update from talent acquisition.", false },
                    { F(9), J(21), D(2026,6,11,9), "Send pair-programming follow-up.", true }, { F(10), J(24), D(2026,8,30,9), "Complete and submit technical assessment.", false }
                });

            // Offers
            migrationBuilder.InsertData(
                table: "Offers",
                columns: new[] { "Id", "JobApplicationId", "Status", "AnnualSalary", "Currency", "Notes" },
                values: new object[,]
                {
                    { O(1), J(1), "Pending", 136000m, "CHF", "Includes annual bonus and stock award." },
                    { O(2), J(6), "Accepted", 126000m, "CHF", "Accepted with a September start date." },
                    { O(3), J(19), "Declined", 132000m, "CHF", "Declined after comparing role scope and commute." },
                    { O(4), J(23), "Pending", 134000m, "CHF", "Decision requested within ten business days." }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Dependents are removed before their parent records.
            migrationBuilder.DeleteData("ApplicationStatusHistories", "Id", Enumerable.Range(1, 70).Select(H).Cast<object>().ToArray());
            migrationBuilder.DeleteData("Interviews", "Id", Enumerable.Range(1, 12).Select(I).Cast<object>().ToArray());
            migrationBuilder.DeleteData("FollowUps", "Id", Enumerable.Range(1, 10).Select(F).Cast<object>().ToArray());
            migrationBuilder.DeleteData("Offers", "Id", Enumerable.Range(1, 4).Select(O).Cast<object>().ToArray());
            migrationBuilder.DeleteData("JobApplications", "Id", Enumerable.Range(1, 25).Select(J).Cast<object>().ToArray());
            migrationBuilder.DeleteData("ContactPersons", "Id", Enumerable.Range(1, 10).Select(P).Cast<object>().ToArray());
            migrationBuilder.DeleteData("Companies", "Id", Enumerable.Range(1, 12).Select(C).Cast<object>().ToArray());
        }

        private static Guid G(string value) => Guid.Parse(value);
        private static Guid C(int id) => G($"10000000-0000-0000-0000-{id:D12}");
        private static Guid J(int id) => G($"20000000-0000-0000-0000-{id:D12}");
        private static Guid P(int id) => G($"30000000-0000-0000-0000-{id:D12}");
        private static Guid H(int id) => G($"40000000-0000-0000-0000-{id:D12}");
        private static Guid I(int id) => G($"50000000-0000-0000-0000-{id:D12}");
        private static Guid F(int id) => G($"60000000-0000-0000-0000-{id:D12}");
        private static Guid O(int id) => G($"70000000-0000-0000-0000-{id:D12}");
        private static DateTime D(int year, int month, int day, int hour = 0) => new(year, month, day, hour, 0, 0, DateTimeKind.Utc);
    }
}
