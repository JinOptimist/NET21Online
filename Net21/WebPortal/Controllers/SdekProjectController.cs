using Microsoft.AspNetCore.Mvc;
using WebPortal.Models;

namespace WebPortal.Controllers;
public class SdekProjectController : Controller
{
    private ICdekRepository _cdekRepository;

    public SdekProjectController(ICdekRepository cdekRepository)
    {
        _cdekRepository = cdekRepository;
    }
    
    // GET
    public IActionResult Index()
    {
        var cdek = _cdekRepository
            .Select(dbCdek => 
                new CdekViewModel
                     { 
                         Id = dbCdek.Id,
                        Name = dbCdek.Name,
                        Question = dbCdek.Question,
                        PhoneNumber = dbCdek.PhoneNumber,
                        CreationTime = DateTime.Now,
                     })
        
        return View(cdek);
    }
    
    public IActionResult Remove(int Id)
    {
        _cdekRepository.Remove(Id);

        return RedirectToAction("Index");
    }
    
    [HttpGet]
    public IActionResult CallRequest()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult CallRequest(CdekViewModel cdekViewModel)
    {
        return RedirectToAction("Index");
    }
}