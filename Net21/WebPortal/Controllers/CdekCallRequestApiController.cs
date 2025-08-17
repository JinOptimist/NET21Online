using Microsoft.AspNetCore.Mvc; // Для Controller, IActionResult, HttpGet/HttpPost
using WebPortal.DbStuff.Repositories; // Для CdekCallRequestRepository
using WebPortal.DbStuff.Models; // Для CdekCallRequest
namespace WebPortal.Controllers;

public class CdekCallRequestController : Controller
{
    private readonly CdekCallRequestRepository _repository;

    public CdekCallRequestController(CdekCallRequestRepository repository)
    {
       _repository = repository; 
    }
   
    [HttpPost]
    public IActionResult Create(CallRequest request)
    {
        
    }
}