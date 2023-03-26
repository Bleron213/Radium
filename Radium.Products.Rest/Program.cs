using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Radium.Products.Rest.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureServices();

builder.Services.AddApiVersioning(config =>
{
	config.AssumeDefaultVersionWhenUnspecified = true;
	config.ApiVersionReader = new UrlSegmentApiVersionReader();
	config.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
	config.ReportApiVersions = true;
});

builder.Services.AddVersionedApiExplorer(options => 
{ 
	options.GroupNameFormat = "'v'VVV";
	options.SubstituteApiVersionInUrl = true; 
	options.AssumeDefaultVersionWhenUnspecified = true; 
}); 

var app = builder.Build();

// Configure the HTTP request pipeline.

var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
	foreach (var group in provider.ApiVersionDescriptions.Select(x => x.GroupName))
	{
		c.SwaggerEndpoint($"/swagger/{group}/swagger.json", $"Radium Products Rest API Version {group.ToUpperInvariant()}");
	}

	c.OAuthAppName("Radium Products Rest API - Swagger");
});


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
