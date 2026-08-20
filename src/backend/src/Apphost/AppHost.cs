var builder = DistributedApplication.CreateBuilder(args);

var api = builder
    .AddProject<Projects.ApplicationManagerAPI>("applicationmanagerapi");

var frontend = builder
    .AddNodeApp("frontend", "../../../frontend", "src/main.ts")
    .WithNpm()
    .WithRunScript("start")
    .WithHttpEndpoint(env: "PORT")
    .WithExternalHttpEndpoints()
    .WaitFor(api);

builder.Build().Run();