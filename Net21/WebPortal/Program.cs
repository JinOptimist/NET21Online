using Microsoft.EntityFrameworkCore;
using WebPortal.Controllers;
using WebPortal.CustomMiddleware;
using WebPortal.DbStuff;
using WebPortal.DbStuff.Repositories;
using WebPortal.DbStuff.Repositories.Cdek;
using WebPortal.DbStuff.Repositories.CompShop;
using WebPortal.DbStuff.Repositories.Interfaces;
using WebPortal.DbStuff.Repositories.Interfaces.CompShop;
using WebPortal.DbStuff.Repositories.Interfaces.Marketplace;
using WebPortal.DbStuff.Repositories.Interfaces.Notes;
using WebPortal.DbStuff.Repositories.Marketplace;
using WebPortal.Services;
using WebPortal.Services.Permissions;
using WebPortal.Services.Permissions.CoffeShop;
using WebPortal.Services.Permissions.Interface;
using NotesRepositories = WebPortal.DbStuff.Repositories.Notes;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpLogging(opt => opt.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.All);
builder.Logging.AddFilter("Microsoft.AspNetCore.HttpLogging", LogLevel.Information);

builder.Configuration
    .AddJsonFile("appsettings.NotesProject.json", optional: true, reloadOnChange: true);

builder.Services
    .AddAuthentication(AuthNotesController.AUTH_KEY)
    .AddCookie(AuthNotesController.AUTH_KEY, o =>
    {
        o.LoginPath = "/AuthNotes/Login";
        o.ForwardForbid = "/AuthNotes/Forbid";
    });

builder.Services
    .AddAuthentication(AuthController.AUTH_KEY)
    .AddCookie(AuthController.AUTH_KEY, o =>
    {
        o.LoginPath = "/Auth/Login";
        o.ForwardForbid = "/Auth/Forbid ";
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
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<ISourcePDFService, SourcePDFService>();
builder.Services.AddScoped<INotePermission, NotePermission>();
//Marketplace
builder.Services.AddScoped<ILaptopRepository, LaptopRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IExportService, ExportService>();
//CompShop
builder.Services.AddScoped<IDeviceRepository, DeviceRepository>();
builder.Services.AddScoped<INewsRepository, NewsRepository>();
builder.Services.AddScoped<CategoryRepository>();
builder.Services.AddScoped<TypeDeviceRepository>(); 
builder.Services.AddScoped<ComputerRepository>(); 
builder.Services.AddScoped<ICompShopFileService, CompShopFileService>(); 

builder.Services.AddScoped<IMotorcycleRepository, MotorcycleRepository>();
builder.Services.AddScoped<IMotorcycleBrandRepositories, MotorcycleBrandRepositories>();
builder.Services.AddScoped<IMotorcycleTypeRepositories, MotorcycleTypeRepositories>();

builder.Services.AddScoped<ISpaceStationRepository, SpaceStationRepository>();
builder.Services.AddScoped<ISpaceNewsPermission, SpaceNewsPermission>();
builder.Services.AddScoped<IGuitarRepository, GuitarRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<IAnimeRepository, AnimeRepository>();


builder.Services.AddScoped<AuthService>();

//Tourism
builder.Services.AddScoped<ITourPreviewRepository, TourPreviewRepository>();
builder.Services.AddScoped<IToursRepository, ToursRepository>();
builder.Services.AddScoped<ITourPermission, TourPermission>();
builder.Services.AddScoped<ITourismFilesService, TourismFilesService>();

//CallRequest
builder.Services.AddScoped<ICallRequestRepository, CallRequestRepository>();
builder.Services.AddScoped<IAdminCallRequestRepository, AdminCallRequestRepository>();
builder.Services.AddScoped<IAdminCallRequestPermission, AdminCallRequestPermission>();

builder.Services.AddScoped<IGirlPermission, GirlPermission>();
builder.Services.AddScoped<IMarketplacePermissions, MarketplacePermissions>();
builder.Services.AddScoped<MarketplacePermissions>();
builder.Services.AddScoped<ICompShopPermission, CompShopPermission>();
builder.Services.AddScoped<ICommentPermission, CommentPermission>();

// Register Servcies
// builder.Services.AddScoped<SuperService>();

//CoffeShop
builder.Services.AddScoped<ICoffeeProductRepository, CoffeeProductRepository>();
builder.Services.AddScoped<IUserCommentRepository, UserCommentRepository>();
builder.Services.AddScoped<IUserCoffeShopRepository, UserCoffeShopRepositrory>();
builder.Services.AddScoped<ICoffeShopPermision, CoffeShopPermision>();
builder.Services.AddScoped<ICoffeShopFileServices, CoffeShopFileServices>();


builder.Services.AddScoped<SeedService>();

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var seed = scope.ServiceProvider.GetRequiredService<SeedService>();
    seed.Seed();
}

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

var localizationMode = builder.Configuration["Localization:Mode"];

if (string.Equals(localizationMode, "Notes", StringComparison.OrdinalIgnoreCase))
{
    app.UseMiddleware<CustomNotesLocalizationMiddleware>();
}
else
{
    app.UseMiddleware<CustomLocalizationMiddleware>();
}

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
