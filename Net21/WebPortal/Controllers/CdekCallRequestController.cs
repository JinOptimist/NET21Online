using Microsoft.AspNetCore.Mvc; 
using WebPortal.DbStuff.Repositories; 
using WebPortal.DbStuff.Models;
using WebPortal.Models;
using EnvironmentName = Microsoft.AspNetCore.Hosting.EnvironmentName;


namespace WebPortal.Controllers
{
    public class CdekCallRequestController : Controller
    {
        private CallRequestRepository _cdekRepository;
    
        public CdekCallRequestController(CallRequestRepository cdekRepository)
        {
            _cdekRepository = cdekRepository;
        }

        private static List<CallRequest> _requests = new List<CallRequest>()
        {
            new CallRequest
            {
                Name = "Дмитрий Иванов",
                Question = "Есть ли скидки для постоянных клиентов?",
                PhoneNumber = "+7996123456",
                CreationTime = DateTime.Now
            },
            new CallRequest
            {
                Name = "Ольга Васильева",
                Question = "Как изменить адрес доставки?",
                PhoneNumber = "+7936123456",
                CreationTime = DateTime.Now.AddHours(-5)
            },
            new CallRequest
            {
                Name = "Игорь Николаев",
                Question = "Какие способы оплаты доступны?",
                PhoneNumber = "+7946123456",
                CreationTime = DateTime.Now.AddDays(-2)
            }
        };

        public IActionResult Remove(int Id)
        {
            _cdekRepository.Remove(Id);

            return RedirectToAction("Index");
        }
        
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(CallRequestViewModel callrequestViewModel)
        {
            var cdekDb = new CallRequest()
            {
                Name = callrequestViewModel.Name,
                Question = callrequestViewModel.Question,
                PhoneNumber = callrequestViewModel.PhoneNumber,
            };

            _cdekRepository.Add(cdekDb);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(_requests);
        }
    }        
}