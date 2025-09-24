using IdolMinimalApi;
using IdolMinimalApi.DbStuff;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=IdolDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
builder.Services.AddDbContext<IdolDbContext>(x => x.UseSqlServer(connectionString));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IdolService>();

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

app.MapGet("/", () => "Smile");

app.MapGet("/createIdol/{name}/{url}", (string name, string url, IdolService service) =>
{
    var id = service.CreateIdol(name, url);
    return id;
});

app.MapGet("/GetUrls", (IdolDbContext dbContext) =>
{
    return dbContext.Idols.Select(x => x.Url).ToList();
});

app.MapGet("/Name", (IdolService service) =>
{
    return service.GetNames();
});

app.MapGet("/GetIdols", (IdolDbContext dbContext) =>
{
    return dbContext.Idols.ToList();
});

app.UseSwagger();
app.UseSwaggerUI();

app.Run();
