using Microsoft.EntityFrameworkCore;
using RentalCarsMinimalApi;
using RentalCarsMinimalApi.DBContext;
using RentalCarsMinimalApi.DBContext.Models;
using static System.Net.Mime.MediaTypeNames;

var builder = WebApplication.CreateBuilder(args);
var connectionString = "Data Source=(localdb)\\MSSQLLocalDB; Initial Catalog = RentalCarsDB; Integrated Security = True; Connect Timeout = 30; Encrypt = False; Trust Server Certificate=False; Application Intent = ReadWrite; Multi Subnet Failover=False";

builder.Services.AddDbContext<RentalCarDBContext>(x => x.UseSqlServer(connectionString));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<RentCarsService>();

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

var app = builder.Build();

app.UseCors();

app.MapGet("/GetCarsNames", (RentCarsService service) =>
{
    return service.GetCarsNames();
});

app.MapGet("/GetUrls", (RentCarsService service) =>
{
    return service.GetUrls();
});

app.MapGet("/GetRentalCars", (RentCarsService service) =>
{
    return service.GetRentalCars();
});

app.MapPost("/createRentalCars", (RentalCar request, RentCarsService service) =>
{
    var id = service.CreateRentalCar(request.Name, request.Description, request.Url, request.Price);
    return id;
});


app.UseSwagger();
app.UseSwaggerUI();

app.Run();
