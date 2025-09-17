namespace WebPortal.Services
{
    public class CdekService : ICdekService
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public CdekService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public int GetUserId()
        {
            var httpContext = _contextAccessor.HttpContext;
            return int.Parse(httpContext
                .User
                .Claims
                .First(x => x.Type == "Id")
                .Value);
        }

        public bool IsAuthenticated()
        {
            return _contextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
        }
    }
}