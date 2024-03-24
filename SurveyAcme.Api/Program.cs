using SurveyAcme.Utilities.DataTreatment;
using FluentValidation.AspNetCore;
using System.Text.Json.Serialization;
using SurveyAcme.Models.Validators;
using SurveyAcme.Models;
using SurveyAcme.Utilities.Jwt;
using SurveyAcme.Utilities.Middleware;
using SurveyAcme.DataAccess.Initializers;
using SurveyAcme.Utilities.Helpers;
using SurveyAcme.Api.Business;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

TimeZoneHelper.TimeZoneId = configuration["TimeZoneId"];

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.IgnoreNullValues = true;
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.PropertyNamingPolicy = SnakeCaseNamingPolicy.Instance;
}).AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<SurveyCreateValidator>());

//App Setting Map
builder.Services.Configure<AppSettings>(configuration);

// Add Data Base Context 
builder.Services.AddCustomDbContexts(configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Dependency
builder.Services.AddScoped<IValidatorInterceptor, CustomValidatorInterceptor>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<SurveyCrudService>();

//JWT Configuration
builder.Services.AddCustomJwtAuthentication(configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var scope = app.Services.CreateScope();

scope.AddAutomaticMigrate(configuration);

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionHandling>();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();