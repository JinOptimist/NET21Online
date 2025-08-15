using Microsoft.AspNetCore.Mvc; // Для Controller, IActionResult, HttpGet/HttpPost
using WebPortal.DbStuff.Repositories; // Для CdekCallRequestRepository
using WebPortal.DbStuff.Models; // Для CdekCallRequest
namespace WebPortal.Controllers;

public class CdekCallRequestController : Controller
{
    private readonly CdekCallRequestRepository _repository;

    public CdekCallRequestController(CdekCallRequestRepository repository)
        => _repository = repository;

    [HttpGet]
    public IActionResult Index()
        => View(_repository.GetAll());

    [HttpGet]
    public IActionResult Create()
        => View();

    [HttpPost]
    public IActionResult Create(CdekCallRequest request)
    {
        if (!ModelState.IsValid)
            return View(request);

        _repository.Add(request);
        return RedirectToAction("Index");
    }
}