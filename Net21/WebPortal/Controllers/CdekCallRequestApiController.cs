using Microsoft.AspNetCore.Mvc; // Для Controller, IActionResult, HttpGet/HttpPost
using WebPortal.DbStuff.Repositories; // Для CdekCallRequestRepository
using WebPortal.DbStuff.Models;
using EnvironmentName = Microsoft.AspNetCore.Hosting.EnvironmentName; // Для CdekCallRequest
namespace WebPortal.Controllers;

public class CdekCallRequestController : Controller
{
    private List<int> numbers = new List<int>() {1, 2, 3, 4, 5, 6, 7, 8, 9};
    
    private List<CallRequest> _requests = new List<CallRequest>() {Name, Question};
    
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult Create(CallRequest request)
    {
        if (ModelState.IsValid)
        {
            _requests.Add(request);
            return RedirectToAction("Index");
        }
        return View(request);
    }
    
    [HttpGet]
    public IActionResult Index()
    {
        return View(_requests);
    }
    
    
    
    
    
    
    
    
    
    
    private readonly CdekCallRequestRepository _cdekRepository;

    public CdekCallRequestController(CdekCallRequestRepository cdekRepository)
    {
        _cdekRepository = cdekRepository;
    }