namespace WebPortal.Models.CoffeShop
{
    public class EndPointsCoffeViewModel
    {
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string HttpVerb { get; set; }
        public string Route { get; set; }
        public string ViewModelTypeName { get; set; }
        public bool HasAuthorize { get; set; }
        public bool HasAllowAnonymous { get; set; }
        public List<string> Parameters { get; set; }
        public List<string> Filters { get; set; }

    }
}
