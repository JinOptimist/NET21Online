using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Globalization;
using WebPortal.Services;

namespace WebPortal.CustomMiddleware
{
    public class CustomLocalizationMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomLocalizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var authService = context.RequestServices.GetRequiredService<AuthService>();
            if (authService.IsAuthenticated())
            {
                var language = authService.GetLanguage();
                CultureInfo culture;
                switch (language)
                {
                    case Enum.Language.English:
                        culture = new CultureInfo("En");
                        break;
                    case Enum.Language.Russian:
                        culture = new CultureInfo("Ru");
                        break;
                    case Enum.Language.Deutch:
                        culture = new CultureInfo("De");
                        break;
                    default:
                        throw new ArgumentException($"Uknown language {language}");
                }

                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = culture;
            }
            else
            {
                //ru-RU,ru;q=0.9,en-US;q=0.8,en;q=0.7,la;q=0.6
                var h = context.Request.Headers["accept-language"];
            }

            // Before main action check
            //OnActionExecuting

            // call next service
            await _next.Invoke(context);

            //OnActionExecuted
            // After main action check
        }
    }
}
