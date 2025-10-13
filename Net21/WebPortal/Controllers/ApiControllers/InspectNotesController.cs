using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using WebPortal.Controllers.CustomNotesAttributes;

namespace WebPortal.Controllers.ApiControllers;

[ApiController]
[Route("api/Notes/inspect")]
public class InspectNotesController : ControllerBase
{
    [HttpGet]
    public IActionResult InspectNotesControllers()
    {
        var assembly = Assembly.GetAssembly(typeof(InspectNotesController))!;
        var controllers = assembly
            .GetTypes()
            .Where(t =>
                t.IsClass &&
                !t.IsAbstract &&
                (t.IsSubclassOf(typeof(Controller)) || t.IsSubclassOf(typeof(ControllerBase))) &&
                t.GetCustomAttribute<ApiGroupAttribute>()?.Name == "Notes")
            .ToList();

        var result = new List<object>();

        foreach (var controller in controllers)
        {
            var controllerName = controller.Name.Replace("Controller", "");
            var authorizeController = controller
                .GetCustomAttribute<AuthorizeAttribute>() != null;

            var actions = controller
                .GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
                .Where(method => method.ReturnType == typeof(IActionResult))
                .Select(m => new
                {
                    Action = m.Name,
                    HttpMethods = m.GetCustomAttributes<HttpMethodAttribute>()
                        .SelectMany(a => a.HttpMethods)
                        .DefaultIfEmpty("GET")
                        .Distinct()
                        .ToArray(),
                    Parameters = m.GetParameters().Select(p => new
                    {
                        p.Name,
                        Type = p.ParameterType.Name
                    }),
                    Authorize = authorizeController || m.GetCustomAttribute<AuthorizeAttribute>() != null
                });

            result.Add(new
            {
                Controller = controllerName,
                Authorize = authorizeController,
                Actions = actions
            });
        }

        return Ok(result);
    }
}