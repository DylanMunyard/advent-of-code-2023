var builder = DistributedApplication.CreateBuilder(args);

var apiservice = builder.AddProject<Projects.aspire_aoc_Puzzles>("puzzles");

builder.AddProject<Projects.aspire_aoc_Web>("webfrontend")
    .WithReference(apiservice);

builder.Build().Run();
