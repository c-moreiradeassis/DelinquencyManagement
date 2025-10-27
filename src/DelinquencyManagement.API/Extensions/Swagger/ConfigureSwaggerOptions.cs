using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace DelinquencyManagement.API.Extensions.Swagger
{
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) => _provider = provider;

        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
            }
        }

        private OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
        {
            var info = new OpenApiInfo()
            {
                Title = "Delinquency Management API",
                Version = description.ApiVersion.ToString(),
                Description = "The Delinquency Management is the application responsible for answering questions about delinquency in StoneCo",
                Contact = new OpenApiContact { Name = "Controllership", Email = "tech.controladoria@stone.com.br" }
            };

            if (description.IsDeprecated)
            {
                info.Description += "This API version is deprecated.";
            }

            return info;
        }
    }
}
