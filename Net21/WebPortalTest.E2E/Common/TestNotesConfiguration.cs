using Microsoft.Extensions.Configuration;

namespace WebPortalTest.E2E.Common;

public class TestNotesConfiguration
{
    private static IConfiguration? _configuration;

    private static IConfiguration Configuration
    {
        get
        {
            if (_configuration == null)
            {
                _configuration = new ConfigurationBuilder()
                    .AddUserSecrets<TestNotesConfiguration>()
                    .Build();
            }

            return _configuration;
        }
    }

    public static string TestAdminLogin =>
        Configuration["TestAdmin:Login"] ?? throw new InvalidOperationException("TestAdmin:Login not configured");

    public static string TestAdminPassword =>
        Configuration["TestAdmin:Password"] ?? throw new InvalidOperationException("TestAdmin:Password not configured");
}