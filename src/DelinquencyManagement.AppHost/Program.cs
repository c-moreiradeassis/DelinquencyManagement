using Projects;

var builder = DistributedApplication.CreateBuilder(args);

#region Delinquency Management API

IResourceBuilder<ProjectResource> api = builder
    .AddProject<DelinquencyManagement_API>("delinquency-management-api");

#endregion

builder.Build().Run();
