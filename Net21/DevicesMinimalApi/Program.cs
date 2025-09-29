using DevicesMinimalApi.DbStuff;
using DevicesMinimalApi.DbStuff.Repository;
using DevicesMinimalApi.Service;
using DevicesMinimalApi.ViewModels;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSqlServer<DeviceDbContext>(builder.Configuration.GetConnectionString("DeviceDbConnection"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(o =>
{
    o.AddDefaultPolicy(p =>
    {
        p.AllowAnyHeader();
        p.AllowAnyMethod();
        p.SetIsOriginAllowed(x => true);
        p.AllowCredentials();
    });
});

// Register Services
builder.Services.AddScoped<DeviceService>();

// Register Repositories
builder.Services.AddScoped<DeviceRepository>();

var app = builder.Build();

app.UseCors();

app.MapGet("/", () => "Device api");

//HELP
//HELP
//HELP
//HELP
app.MapPost("/createDevice", ([FromServices] DeviceViewModel device, [FromBody] DeviceService service) =>
{
    var id = service.CreateDevice(device);
    return id;
});

app.MapGet("/GetDevice", (DeviceRepository repository) =>
{
    return repository.GetAll();
});

app.UseSwagger();
app.UseSwaggerUI();

app.Run();
