using Microsoft.AspNetCore.Mvc; 
using WebPortal.DbStuff.Repositories; 
using WebPortal.DbStuff.Models;
using WebPortal.DbStuff.Repositories.Interfaces;
using WebPortal.Models;

namespace WebPortal.Controllers;

    public class CallRequestController : Controller
    {
        private readonly ICallRequestRepository _repository;
        
        public CallRequestController(ICallRequestRepository repository)
        {
            _repository = repository;
        }

        // Страница со списком заявок
        public async Task<IActionResult> Index(CancellationToken ct)
        {
            var requests = await _repository.GetAllAsync(ct);
            return View(requests);
        }

        // Страница добавления новой заявки
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        
        // Создание новой заявки
        [HttpPost]
        public async Task<IActionResult> CreateRequest(CallRequestViewModel requestVM, CancellationToken ct)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var callRequest = new CallRequest
            {
                Name = requestVM.Name,
                Question = requestVM.Question,
                PhoneNumber = requestVM.PhoneNumber,
                CreationTime = DateTime.Now
            };

            await _repository.AddAsync(callRequest, ct);

            return Ok(new
            {
                Success = true,
                Message = "Заявка отправлена! Менеджер отдела продаж с Вами свяжется в течение 15 минут, для уточнения деталей.",
                RequestId = callRequest.Id
            });
        }
        
        // Удаление заявки
        [HttpPost]
        public async Task<IActionResult> Remove(int id, CancellationToken ct)
        {
            var deleted = await _repository.DeleteAsync(id, ct);
            if (!deleted)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }
    }        
