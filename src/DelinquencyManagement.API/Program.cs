using Asp.Versioning;
using DelinquencyManagement.API.Extensions;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenTelemetry()
    .ConfigureResource(resource => resource
        .AddService(
            serviceName: "delinquency-management",
            serviceVersion: "1.0.0",
            serviceInstanceId: Environment.MachineName)
        .AddAttributes(new Dictionary<string, object>
        {
            ["deployment.environment"] = "development"
        }))
    .WithTracing(tracing => tracing
        .AddAspNetCoreInstrumentation()
        .AddHttpClientInstrumentation()
        .AddOtlpExporter(opt =>
        {
            opt.Endpoint = new Uri("http://localhost:4317");
            opt.Protocol = OpenTelemetry.Exporter.OtlpExportProtocol.Grpc;
        })
    );

// Add services to the container.
builder.AddApiLayer();

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    var descriptions = app.DescribeApiVersions();

    foreach (var desc in descriptions)
    {
        var url = $"/swagger/{desc.GroupName}/swagger.json";
        options.SwaggerEndpoint(url, desc.GroupName.ToUpperInvariant());
    }

    options.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
