var builder = DistributedApplication.CreateBuilder(args);

var sql = builder
    .AddSqlServer("sql");

var sqldb = sql.AddDatabase("applicationmanagerdb");

// Applies pending EF Core migrations against "applicationmanagerdb" before the API starts.
// Runs "dotnet ef database update" in the API project directory; the connection string is
// injected via WithReference(sqldb) and resolved through the local dotnet tool manifest
// (.config/dotnet-tools.json) so it works regardless of the invoking shell's PATH.
var migrator = builder
    .AddExecutable("db-migrator", "dotnet", "../ApplicationManagerAPI", "ef", "database", "update")
    .WithReference(sqldb)
    .WaitFor(sqldb);

var api = builder
    .AddProject<Projects.ApplicationManagerAPI>("applicationmanagerapi")
    .WithReference(sqldb)
    .WaitForCompletion(migrator);

var frontend = builder
    .AddNodeApp("frontend", "../../../frontend", "src/main.ts")
    .WithNpm()
    .WithRunScript("start")
    .WithHttpEndpoint(env: "PORT")
    .WithExternalHttpEndpoints()
    .WaitFor(api);

builder.Build().Run();