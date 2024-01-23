using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ResturantManagement_Core.EntityFramework.Context;
using Serilog;
using System.Reflection;
using Microsoft.Extensions.Options;
using System.Configuration;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Hosting;
using ResturantManagement_Core.IRepository;
using ResturantManagement_Infra.Repository;
using Asp.Versioning;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<ICustomerRepository,CustomerRepository>();
builder.Services.AddScoped<IEmployeRepository,EmployeRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderItemRepository, OrderItemRepository>();
builder.Services.AddScoped<IMenuRepository, MenuRepository>();
builder.Services.AddScoped<ITableRepository, TableRepository>();
builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
});
builder.Services.AddSwaggerGen(c =>
{
c.SwaggerDoc("v1", new OpenApiInfo
{
    Title = "Resturant Mangment Api",
    Version = "v1",
    TermsOfService = new Uri("https://example.com/terms"),
    Contact = new OpenApiContact
    {
        Name = "Rania taweel",
        Email = "raniataweel@gmail.com",
      
    }
}); 
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

builder.Services.AddDbContext<RestrantDbContext>(cnn => cnn.UseSqlServer(builder.Configuration.GetConnectionString("sqlconnect")));
var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
Serilog.Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).
                WriteTo.File(configuration.GetValue<string>("LoggerFilePath")
                , rollingInterval: RollingInterval.Day).
                MinimumLevel.Debug().
                CreateLogger();
Serilog.Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).
                WriteTo.File(configuration.GetValue<string>("LoggerErrorFilePath")
                , rollingInterval: RollingInterval.Day).MinimumLevel.Error().
                CreateLogger();

    var app = builder.Build();

app.UseStaticFiles();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
try
{
    Log.Information("Starting web host");
    //CreateHostBuilder(args).Build().Run();
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
