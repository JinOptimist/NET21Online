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
using WebPortal.Hubs;
using WebPortal.Hubs.marketplace;
using WebPortal.Services;
using WebPortal.Services.AutoRegistrationInDI;
using WebPortal.Services.Permissions;
using WebPortal.Services.Permissions.CoffeShop;
using WebPortal.Services.Permissions.Interface;
using NotesRepositories = WebPortal.DbStuff.Repositories.Notes;
using PathToNotes = WebPortal.DbStuff.Repositories.Interfaces.Notes;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpLogging(opt => opt.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.All);
builder.Logging.AddFilter("Microsoft.AspNetCore.HttpLogging", LogLevel.Information);

builder.Configuration
    .AddJsonFile("appsettings.CdekProject.json", optional: true, reloadOnChange: true);
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
string connectionString;
if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Cdek")
{
    connectionString = builder.Configuration.GetConnectionString("CdekDbConnection");
}
else
{
    connectionString = builder.Configuration.GetConnectionString("DefaultDbConnection")!;
}
builder.Services.AddDbContext<WebPortalContext>(
    x => x.UseSqlServer(connectionString)
    );
builder.Services.AddDbContext<NotesDbContext>(
    x => x.UseNpgsql(
        builder.Configuration.GetConnectionString("NotesDbConnection"))
    );

// Register Repositories
builder.Services.AddScoped<IUserRepositrory, UserRepositrory>();
// Notes
builder.Services.AddScoped<INoteRepository, NotesRepositories.NoteRepository>();
builder.Services.AddScoped<PathToNotes.ICategoryRepository, NotesRepositories.CategoryRepository>();
builder.Services.AddScoped<ITagRepository, NotesRepositories.TagRepository>();
builder.Services.AddScoped<IUserNotesRepository, NotesRepositories.UserNotesRepository>();
builder.Services.AddScoped<PasswordService>();
builder.Services.AddScoped<IAuthNotesService, AuthNotesService>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<ISourcePDFService, SourcePDFService>();
builder.Services.AddScoped<INotePermission, NotePermission>();
builder.Services.AddScoped<INotificationNotesRepository, NotesRepositories.NotificationNotesRepository>();
//Marketplace
builder.Services.AddScoped<IExportService, ExportService>();

//CompShop
builder.Services.AddScoped<ICompShopFileService, CompShopFileService>(); 

builder.Services.AddScoped<IMotorcycleBrandRepositories, MotorcycleBrandRepositories>();
builder.Services.AddScoped<IMotorcycleTypeRepositories, MotorcycleTypeRepositories>();

builder.Services.AddScoped<ISpaceNewsPermission, SpaceNewsPermission>();

//Tourism
builder.Services.AddScoped<ITourPermission, TourPermission>();
builder.Services.AddScoped<ITourismFilesService, TourismFilesService>();

//CdekProject
builder.Services.AddScoped<IAdminCallRequestPermission, AdminCallRequestPermission>();
builder.Services.AddScoped<ICdekFileService, CdekFileService>();

builder.Services.AddScoped<IGirlPermission, GirlPermission>();
builder.Services.AddScoped<IMarketplacePermissions, MarketplacePermissions>();
builder.Services.AddScoped<MarketplacePermissions>();
builder.Services.AddScoped<ICompShopPermission, CompShopPermission>();
builder.Services.AddScoped<ICommentPermission, CommentPermission>();

//CoffeShop
builder.Services.AddScoped<ICoffeShopPermision, CoffeShopPermision>();
builder.Services.AddScoped<ICoffeShopFileServices, CoffeShopFileServices>();

var authResolver = new AutoRegisterService();
authResolver.RegisterAllRepositories(builder.Services);
authResolver.RegisterAllByAttribute(builder.Services);

builder.Services.AddScoped<SeedService>();

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var seed = scope.ServiceProvider.GetRequiredService<SeedService>();
    seed.Seed();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment() 
    && Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT").ToLower() != "development")
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

app.MapHub<CdekChatHub>("/cdekchat");

app.MapHub<NotificationHub>("/hubs/notifaction");
app.MapHub<SupportChatHub>("/supportChatHub");
app.MapHub<SpaceNewsHub>("/hubs/spacenews");
app.MapHub<NotificationHubCoffeShop>("/hubs/notifaction/CoffeShop");
app.MapHub<NotificationNotesHub>("/hubs/notification-notes");

app.MapHub<TourNotificationHub>("/hubs/notification/tourism");

// Enable attribute-routed API controllers like /api/CatalogApi
app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
