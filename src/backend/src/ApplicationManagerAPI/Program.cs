using System.Text.Json.Serialization;
using ApplicationManagerAPI.Infrastructure.Persistence;
using ApplicationManagerAPI.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// The real connection string is injected by Aspire (via WithReference in AppHost.cs) as the
// "ConnectionStrings__applicationmanagerdb" environment variable. The fallback below is only
// used for design-time tooling (e.g. `dotnet ef migrations add`) and build-time OpenAPI
// generation, where no actual database connection is required.
var connectionString = builder.Configuration.GetConnectionString("applicationmanagerdb")
    ?? "Server=localhost,1433;Database=ApplicationManagerDb;User Id=sa;Password=Design_Time_Only1;TrustServerCertificate=True;Encrypt=False";

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddOpenApi();

builder.Services.AddOpenApiDocument(options =>
{
    options.DocumentName = "v1";
    options.Title = "Application Manager API";
});

var app = builder.Build();

// Database migrations are applied by the "db-migrator" executable resource in AppHost.cs
// before this API is started (the API resource WaitForCompletion()s the migrator), so no
// migration logic runs here.

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseOpenApi();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
