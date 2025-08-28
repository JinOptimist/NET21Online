using Microsoft.AspNetCore.Mvc.Filters;

namespace WebPortal.Controllers.CustomAuthorizeAttributes
{
    public class NoBadWordAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuted(ResultExecutedContext context)
        {
            var stream = context.HttpContext.Response.Body;

            // TODO: CAN DO IT AFTER LEARN ABOUT MIDLEWARE

            //using var sr = new StreamReader(stream);
            //var allBody = sr.ReadToEnd();
            //if (allBody.IndexOf("smile") > 0)
            //{
            //    context.HttpContext.Response.Redirect("/Auth/ForbidWord");
            //    return; 
            //}

            base.OnResultExecuted(context);
        }
    }
}
