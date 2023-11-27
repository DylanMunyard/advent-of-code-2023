using aspire_aoc.AppHost;

var builder = DistributedApplication.CreateBuilder(args);

var redis = builder.AddRedisContainer("cache");

var seq = builder.AddSeqContainer("seq");

var puzzles = builder
    .AddProject<Projects.aspire_aoc_Puzzles>("puzzles")
    .WithReference(seq)
    .WithReference(redis);

builder.AddProject<Projects.aspire_aoc_Web>("webfrontend")
    .WithReference(puzzles);

builder.Build().Run();
