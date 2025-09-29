using Microsoft.EntityFrameworkCore;
using SpaceNewsMinApi;
using SpaceNewsMinApi.DbStuff;

var builder = WebApplication.CreateBuilder(args);

var connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SpaceNewsDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
builder.Services.AddDbContext<SpaceNewsDbContext>(x => x.UseSqlServer(connectionString));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<SpaceNewsService>();

builder.Services.AddCors(o =>
{
    o.AddDefaultPolicy(p =>
    {
        p.AllowAnyHeader();
        p.AllowAnyMethod();
        p.WithOrigins("https://localhost:7210");
        p.AllowCredentials();
    });
});

var app = builder.Build();

app.UseCors();

app.MapGet("/AddNews", (string title, string content, string imageUrl, SpaceNewsService service) =>
{
    var id = service.AddSpaceNews(title, content, imageUrl);
    return id;
});

app.MapGet("/GetSpaceNews", (SpaceNewsDbContext dbContext) =>
{
    return dbContext.SpaceNews.ToList();
});

app.UseSwagger();
app.UseSwaggerUI();

app.Run();
