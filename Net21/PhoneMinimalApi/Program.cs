using Microsoft.EntityFrameworkCore;
using ProductServiceApi.Data;
using ProductServiceApi.Models;
using ProductsMinimalApi.DTOs.Models;
using ProductsMinimalApi.Repositories;
using ProductsMinimalApi.Services;

var builder = WebApplication.CreateBuilder(args);

var connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ProductDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
builder.Services.AddDbContext<ProductContext>(x => x.UseSqlServer(connectionString));

builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<ProductRepository>();
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

var app = builder.Build();
app.UseCors();
app.UseSwagger();
app.UseSwaggerUI();
app.MapGet("/", () => "Product Service API");

app.MapGet("/createProduct/{name}/{price}/{category}/{imageUrl}",
    (string name, decimal price, string category, string imageUrl, ProductService service) =>
    {
        var createDto = new CreateProductDto
        {
            Name = name,
            Price = price,
            Category = category,
            ImageUrl = imageUrl,
            Description = ""
        };
        var product = service.AddAsync(createDto).Result;
        return product?.Id ?? -1;
    });

app.MapGet("/getProducts", (ProductContext dbContext) =>
{
    return dbContext.Products.ToList();
});

app.MapGet("/getProductNames", (ProductService service) =>
{
    var products = service.GetAll();
    return products.Select(x => x.Name).ToList();
});

app.MapGet("/getProductUrls", (ProductContext dbContext) =>
{
    return dbContext.Products.Select(x => x.ImageUrl).ToList();
});

app.MapGet("/getProductsByCategory/{category}", (string category, ProductService service) =>
{
    var products = service.GetByCategory(category);
    return products;
});

app.MapGet("/getProduct/{id}", (int id, ProductService service) =>
{
    var product = service.GetById(id);
    return product != null ? Results.Ok(product) : Results.NotFound();
});

app.MapPost("/updateProduct/{id}", (int id, UpdateProductDto updateDto, ProductService service) =>
{
    var updatedProduct = service.Update(id, updateDto);
    return updatedProduct != null ? Results.Ok(updatedProduct) : Results.NotFound();
});

app.MapDelete("/deleteProduct/{id}", (int id, ProductService service) =>
{
    var result = service.Delete(id);
    return result ? Results.Ok("Deleted") : Results.NotFound();
});

app.Run();