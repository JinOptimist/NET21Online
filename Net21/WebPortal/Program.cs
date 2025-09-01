using Microsoft.EntityFrameworkCore;
using WebPortal.Controllers;
using WebPortal.DbStuff;
using WebPortal.DbStuff.Repositories;
using WebPortal.DbStuff.Repositories.Cdek;
using WebPortal.DbStuff.Repositories.CompShop;
using WebPortal.DbStuff.Repositories.Interfaces;
using WebPortal.DbStuff.Repositories.Interfaces.Marketplace;
using WebPortal.DbStuff.Repositories.Interfaces.Notes;
using WebPortal.DbStuff.Repositories.Marketplace;
using WebPortal.Services;
using WebPortal.Services.Permissions;
using WebPortal.Services.Permissions.CoffeShop;
using NotesRepositories = WebPortal.DbStuff.Repositories.Notes;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpLogging(opt => opt.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.All);
builder.Logging.AddFilter("Microsoft.AspNetCore.HttpLogging", LogLevel.Information);

builder.Services
    .AddAuthentication(AuthController.AUTH_KEY)
    .AddCookie(AuthController.AUTH_KEY, o =>
    {
        o.LoginPath = "/Auth/Login";
        o.ForwardForbid = "/Auth/Forbid ";
    });

builder.Services
    .AddAuthentication(AuthNotesController.AUTH_KEY)
    .AddCookie(AuthNotesController.AUTH_KEY, o =>
    {
        o.LoginPath = "/AuthNotes/Login";
        o.ForwardForbid = "/AuthNotes/Forbid";
    });

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
builder.Services.AddScoped<IUserNotesRepository, NotesRepositories.UserNotesRepository>();
builder.Services.AddScoped<PasswordService>();
builder.Services.AddScoped<AuthNotesService>();
//Marketplace
builder.Services.AddScoped<ILaptopRepository, LaptopRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
//CompShop
builder.Services.AddScoped<DeviceRepository>();
builder.Services.AddScoped<NewsRepository>();
builder.Services.AddScoped<CategoryRepository>();
builder.Services.AddScoped<TypeDeviceRepository>(); 
builder.Services.AddScoped<ComputerRepository>(); 

builder.Services.AddScoped<IMotorcycleRepository, MotorcycleRepository>();
builder.Services.AddScoped<IMotorcycleBrandRepositories, MotorcycleBrandRepositories>();
builder.Services.AddScoped<IMotorcycleTypeRepositories, MotorcycleTypeRepositories>();

builder.Services.AddScoped<ISpaceStationRepository, SpaceStationRepository>();
builder.Services.AddScoped<ISpaceNewsPermission, SpaceNewsPermission>();
builder.Services.AddScoped<IGuitarRepository, GuitarRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();

builder.Services.AddScoped<AuthService>();

//Tourism
builder.Services.AddScoped<ITourPreviewRepository, TourPreviewRepository>();
builder.Services.AddScoped<IToursRepository, ToursRepository>();
builder.Services.AddScoped<ITourPermission, TourPermission>();

//CallRequest
builder.Services.AddScoped<ICallRequestRepository, CallRequestRepository>();
builder.Services.AddScoped<IAdminCallRequestRepository, AdminCallRequestRepository>();

builder.Services.AddScoped<IGirlPermission, GirlPermission>();
builder.Services.AddScoped<IMarketplacePermissions, MarketplacePermissions>();
builder.Services.AddScoped<MarketplacePermissions>();

builder.Services.AddScoped<ICommentPermission, CommentPermission>();
builder.Services.AddScoped<CompShopPermission>();

// Register Servcies
// builder.Services.AddScoped<SuperService>();

//CoffeShop
builder.Services.AddScoped<ICoffeeProductRepository, CoffeeProductRepository>();
builder.Services.AddScoped<IUserCommentRepository, UserCommentRepository>();
builder.Services.AddScoped<IUserCoffeShopRepository, UserCoffeShopRepositrory>();
builder.Services.AddScoped<ICoffeShopPermision, CoffeShopPermision>();

builder.Services.AddHttpContextAccessor();

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

// Who am I?
app.UseAuthentication();
// What can I do?
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
