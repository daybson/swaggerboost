using Asp.Versioning.ApiExplorer;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

using SwaggerBoost.Model;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace SwaggerBoost.Configuration;

public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
{
    protected readonly IApiVersionDescriptionProvider _provider;
    protected readonly SwaggerApiSettings _settings;

    public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider, IOptions<SwaggerApiSettings> options)
    {
        this._provider = provider;
        this._settings = options.Value;
    }

    public void Configure(SwaggerGenOptions options)
    {
        foreach (var description in _provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
        }
    }

    protected OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
    {
        var info = new OpenApiInfo()
        {
            Title = _settings.Title,
            Version = description.ApiVersion.ToString(),
            Description = _settings.Description,
            Contact = new OpenApiContact()
            {
                Name = _settings.Contact.Name,
                Email = _settings.Contact.Email,
                Url = !string.IsNullOrWhiteSpace(_settings.Contact.Url) ? new Uri(_settings.Contact.Url) : null,
            },
            License = new OpenApiLicense()
            {
                Name = _settings.License.Name,
                Url = !string.IsNullOrWhiteSpace(_settings.License.Url) ? new Uri(_settings.License.Url) : null
            }
        };

        if (description.IsDeprecated)
        {
            info.Description += _settings.ObsoleteDescription;
        }

        return info;
    }
}
