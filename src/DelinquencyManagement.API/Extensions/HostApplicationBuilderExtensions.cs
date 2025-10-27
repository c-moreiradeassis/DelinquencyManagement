using Asp.Versioning;
using DelinquencyManagement.API.Extensions.Swagger;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace DelinquencyManagement.API.Extensions
{
    public static class HostApplicationBuilderExtensions
    {
        public static IHostApplicationBuilder AddApiLayer(this IHostApplicationBuilder builder)
        {
            builder.Services
                .AddApiVersioning(config =>
                {
                    config.DefaultApiVersion = new ApiVersion(1, 0);
                    config.AssumeDefaultVersionWhenUnspecified = true;
                    config.ReportApiVersions = true;
                    config.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(),
                        new HeaderApiVersionReader("x-api-version"),
                        new MediaTypeApiVersionReader("x-api-version"));
                }).AddApiExplorer(options =>
                {
                    options.GroupNameFormat = "'v'VVV";
                    options.SubstituteApiVersionInUrl = true;
                });

            builder.Services
                .AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>()
                .AddSwaggerGen(options =>
                {
                    options.OperationFilter<SwaggerDefaultValues>();

                    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                    options.IncludeXmlComments(xmlPath);
                });

            return builder;
        }
    }
}
