namespace SwaggerBoost.Model;

public record SwaggerApiSettings
{
    public string Title { get; set; }
    public string Version { get; set; }
    public string Description { get; set; }
    public string ObsoleteDescription { get; set; }
    public Contact Contact { get; set; }
    public License License { get; set; }
    public bool UseJwtAuthentication { get; set; }
}