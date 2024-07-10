# Swagger Boost  

This package provides an easy way to add versioning to Swagger using ASP.NET with few modifications.
![Sem título](https://github.com/daybson/swaggerboost/assets/3179898/4d626058-0a09-441c-a234-715b746eae4d)


## Usage

Add a reference to this project.

In your API appsettings.json, add the following section and set the values you desire:

```csharp
"SwaggerConfig": {
 "Title": "My API",
 "Version": "1",
 "Description": "The most awesome API that does everything!",
 "ObsoleteDescription": " Obsolete version.",
 "Contact": {
   "Name": "My name",
   "Email": "myemail@domain.com",
   "Url": "https://mywebsite.com"
 },
 "License": {
   "Name": "MIT License",
   "Url": "www.mylicense.com"
 },
 "UseJwtAuthentication": false
},
```

If you desire to implement Jwt bearer authentication in Swagger, set

`"UseJwtAuthentication": true`

then it will generate the login box for you (you still need to implement the authentication in your API).
![Sem título2](https://github.com/daybson/swaggerboost/assets/3179898/4c8fd862-bf5f-44d3-9c7f-a7d49782065b)


In your Program.cs, add a reference to:

```csharp
using SwaggerBoost.Configuration;
using Swashbuckle.AspNetCore.SwaggerGen;
```

Then add the configuration of Swagger:

```csharp
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers(); 

//...
builder.Services.AddSwaggerConfig(builder.Configuration);
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
//...

var app = builder.Build();
//... middlewares

//...
var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
app.UseSwaggerConfig(apiVersionDescriptionProvider);
//...

app.Run();
```

In your controllers, add the version annotations as you desire. For example, HelloController has two versions:

```csharp
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
namespace Api.Controllers.V1;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class HelloController : ControllerBase
{
   [HttpGet("hello")]
   public string Hello() => "Hello world!";
}
```

And version 2:

```csharp
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
namespace Api.Controllers.V2;

[ApiVersion("2.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class HelloController : ControllerBase
{
   [HttpGet("hello")]
   public string Hello() => "Hello world 2!";
}
```

That's  all you need to do.
