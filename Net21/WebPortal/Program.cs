using Microsoft.EntityFrameworkCore;
using WebPortal.DbStuff;
using WebPortal.DbStuff.Models.CompShop;
using WebPortal.DbStuff.Repositories;
using WebPortal.DbStuff.Repositories.Cdek;
using WebPortal.DbStuff.Repositories.CompShop;
using WebPortal.DbStuff.Repositories.Interfaces;
using WebPortal.DbStuff.Repositories.Interfaces.Marketplace;
using WebPortal.DbStuff.Repositories.Marketplace;
using WebPortal.DbStuff.Repositories.Interfaces.Notes;
using NotesRepositories = WebPortal.DbStuff.Repositories.Notes;
using WebPortal.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpLogging(opt => opt.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.All);
builder.Logging.AddFilter("Microsoft.AspNetCore.HttpLogging", LogLevel.Information);

// Register db context
builder.Services.AddDbContext<WebPortalContext>(
    x => x.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultDbConnection"))
    );
builder.Services.AddDbContext<NotesDbContext>(
    x => x.UseNpgsql(
        builder.Configuration.GetConnectionString("NotesDbConnection"))
    );

// Register Repositories
builder.Services.AddScoped<IUserRepositrory, UserRepositrory>();
builder.Services.AddScoped<IGirlRepository, GirlRepository>();
// Notes
builder.Services.AddScoped<INoteRepository, NotesRepositories.NoteRepository>();
builder.Services.AddScoped<ICategoryRepository, NotesRepositories.CategoryRepository>();
builder.Services.AddScoped<ITagRepository, NotesRepositories.TagRepository>();
//Marketplace
builder.Services.AddScoped<ILaptopRepository, LaptopRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
//CompShop
builder.Services.AddScoped<DeviceRepository>();
builder.Services.AddScoped<NewsRepository>();
builder.Services.AddScoped<CategoryRepository>();
builder.Services.AddScoped<TypeDeviceRepository>(); 

builder.Services.AddScoped<IMotorcycleRepository, MotorcycleRepository>();
builder.Services.AddScoped<IMotorcycleBrandRepositories, MotorcycleBrandRepositories>();
builder.Services.AddScoped<IMotorcycleTypeRepositories, MotorcycleTypeRepositories>();
builder.Services.AddScoped<ICoffeeProductRepository, CoffeeProductRepository>();
builder.Services.AddScoped<IUserCommentRepository, UserCommentRepository>();
builder.Services.AddScoped<ISpaceStationRepository, SpaceStationRepository>();

builder.Services.AddScoped<IGuitarRepository, GuitarRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();

builder.Services.AddScoped<ITourismRepository, TourismRepository>();

//CallRequest
builder.Services.AddScoped<ICallRequestRepository, CallRequestRepository>();

// Register Servcies
// builder.Services.AddScoped<SuperService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpLogging();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
