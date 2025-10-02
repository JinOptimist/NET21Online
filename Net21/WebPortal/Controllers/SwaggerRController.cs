using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Reflection;
using WebPortal.Models.Home;
using WebPortal.Models.Swagger;

namespace WebPortal.Controllers
{
    public class SwaggerRController : Controller
    {
        public IActionResult SwaggerR()
        {
            var contollers = Assembly
                .GetAssembly(typeof(HomeController))!
                .GetTypes()
                .Where(type => type.BaseType == typeof(Controller));

            var swaggerViewModel = new List<SwaggerRViewModel>();

            foreach (var contoller in contollers)
            {
                var isAutorizeOnContollerLevel = contoller
                    .GetCustomAttribute<AuthorizeAttribute>() != null;

                var actions = contoller
                    .GetMethods(BindingFlags.Instance | BindingFlags.Public)
                    .Where(method => method.ReturnType == typeof(IActionResult));

                var viewModels = actions
                    .Select(method =>
                        new SwaggerRViewModel
                        {
                            NameMethod = method.Name,
                            Parametrs = GetParametersNames(method),
                            TypeMethod = GetHttpMethod(method),
                        })
                    .ToList();

                swaggerViewModel.AddRange(viewModels);
            }

            var projectName = Assembly.GetAssembly(typeof(HomeController))!.GetCustomAttribute<AssemblyTitleAttribute>()?.Title
                      ?? Assembly.GetAssembly(typeof(HomeController))!.GetName().Name
                      ?? "Web API";

            var viewModel = new SwaggerRPageViewModel
            {
                SwaggerRViewModels = swaggerViewModel,
                NameProject = projectName,
            };

            return View(viewModel);
        }

        private List<string> GetParametersNames(MethodInfo method)
        {
            var nameParametrs = new List<string>();
            foreach (var parameter in method.GetParameters())
            {
                var jsonParametr = $"\"{parameter.Name}\": \"{parameter.ParameterType.Name.ToLower()}\",";
                nameParametrs.Add(jsonParametr);
            }

            return nameParametrs.Count() != 0
                ? nameParametrs 
                : new List<string> { "NO PARAMETERS" };
        }

        private string GetHttpMethod(MethodInfo method)
        {
            var httpMethodAttr = method.GetCustomAttribute<HttpMethodAttribute>();

            if (httpMethodAttr == null)
            {
                return "Get";
            }

            return httpMethodAttr.HttpMethods.Contains("POST")
                ? "Post"
                : "Get";
        }
    }
}
