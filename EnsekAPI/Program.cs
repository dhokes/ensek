using EnsekAPI.DataRepository;
using EnsekAPI.Helpers;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();

// Swagger docs
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Meter Readings API",
        Version = "v1",
        Description = "A Web API to upload meter readings."
    });

    var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
    var commentsFileName = Assembly.GetEntryAssembly().GetName().Name + ".xml";
    var commentsFile = Path.Combine(baseDirectory, commentsFileName);
    c.IncludeXmlComments(commentsFile);
});

builder.Services.AddScoped<IMeterReadingSqlContext, MeterReadingSqlContext>();
builder.Services.AddScoped<ICsvImporter, CsvImporter>();
builder.Services.AddScoped<IValidationHelper, ValidationHelper>();

// Database context
builder.Services.AddDbContext<DatabaseContext>(options =>
{
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection")));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=UploadMeterReading}/{action=Index}/{id?}");

app.Run();

