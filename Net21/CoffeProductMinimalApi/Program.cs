using CoffeProductMinimalApi;
using CoffeProductMinimalApi.DbStuff;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CoffeProductDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
builder.Services.AddDbContext<CoffeProductDbContext>(x => x.UseSqlServer(connectionString));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<CoffeService>();

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

app.MapGet("/", () => "Start Page");

app.MapGet("/createCoffe/{name}/{url}", (string name, string url, CoffeService service) =>
{
    var id = service.CreateCoffe(name, url);
    return id;
});

app.MapGet("/GetUrlsCoffee", (CoffeProductDbContext dbContext) =>
{
    return dbContext.CoffeProducts.Select(x => x.Url).ToList();


});

app.MapGet("/Product", (CoffeService serviceCoffe) =>
{
    return serviceCoffe.GetNameProduct();
});

app.MapGet("/GetNameCoffe", (CoffeProductDbContext dbContext) =>
{
    return dbContext.CoffeProducts.ToList();
});





app.UseSwagger();
app.UseSwaggerUI();

app.Run();
