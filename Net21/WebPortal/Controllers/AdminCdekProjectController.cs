using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Net.Http.Headers;
using WebPortal.Controllers.CustomAuthorizeAttributes;
using WebPortal.DbStuff.Repositories.Interfaces;
using WebPortal.DbStuff.Repositories.Interfaces.Notes;
using WebPortal.Enum;
using WebPortal.Models.Cdek;
using WebPortal.Services;
using WebPortal.Services.Permissions;

namespace WebPortal.Controllers;

[Authorize]
public class AdminCdekProjectController : Controller
{
    private readonly IAdminCallRequestRepository _adminCallRequestRepository;
    private IUserRepositrory _userRepositrory;
    private AuthService _authService;
    private IAdminCallRequestPermission _adminCallRequestPermission;
    private ICdekFileService _cdekFileService;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public AdminCdekProjectController(
        IAdminCallRequestRepository adminCallRequestRepository, 
        IUserRepositrory userRepositrory, 
        AuthService authService,
        IAdminCallRequestPermission adminCallRequestPermission,
        ICdekFileService cdekFileService,
        IWebHostEnvironment webHostEnvironment)
    {
        _adminCallRequestRepository = adminCallRequestRepository;
        _userRepositrory = userRepositrory;
        _authService = authService;
        _adminCallRequestPermission = adminCallRequestPermission;
        _cdekFileService = cdekFileService;
        _webHostEnvironment = webHostEnvironment;
    }
    
    /// <summary>
    /// Список всех заявок, отфильтрован в репозитории + список загруженных файлов + строка статузов заявок
    /// </summary>
    /// <param name="search"></param>
    /// <param name="statusFilter"></param>
    /// <returns></returns>
    public IActionResult Index(string search = "", string statusFilter = "")
    {
        var requests = _adminCallRequestRepository.GetFilteredRequests(search, statusFilter);   
        var stats = _adminCallRequestRepository.GetStatistics();

        var viewModel = new AdminCallRequestIndexViewModel
        {
            CallRequests = requests
                .Select(r => new AdminCallRequestItemViewModel
                {
                    Id = r.Id,
                    Name = r.Name,
                    PhoneNumber = r.PhoneNumber,
                    Question = r.Question,
                    Status = r.Status,
                    CreatedAt = r.CreatedAt,
                    CanDelete = _adminCallRequestPermission.CanDelete(r),
                }).ToList(),

            SearchTerm = search,
            SelectedStatus = statusFilter,
            StatusOptions = new List<SelectListItem>
            {
                new SelectListItem { Value = "", Text = "Все статусы" },
                new SelectListItem { Value = "Новая", Text = "Новая" },
                new SelectListItem { Value = "Обработана", Text = "Обработана" }
            },
            
            StatusStats = stats,
            
            // добавляем список файлов    
            UploadFiles = _cdekFileService.GetAllFiles()        
        };
        
        return View(viewModel);
    }
    
    /// <summary>
    /// Удаление заявки
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Role(Role.Admin)]
    public IActionResult Remove(int id)
    {
        _adminCallRequestRepository.Remove(id);
        return RedirectToAction("Index");
    }
    
    /// <summary>
    ///  Изменение статуса заявки
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public IActionResult ChangeStatus(int id)
    {
        var request = _adminCallRequestRepository.GetById(id);
        if (request != null)
        {
            request.Status = request.Status == "Новая" ? "Обработана" : "Новая";
            _adminCallRequestRepository.Update(request);
        }
        
        return RedirectToAction("Index");
    }
    
    /// <summary>
    /// Загрузка файлов
    /// </summary>
    /// <param name="uploadFiles"></param>
    /// <returns></returns>
    [HttpPost]
    public IActionResult UpdateFiles(List<IFormFile> uploadFiles)
    {
        foreach (var file in uploadFiles)
        {
            _cdekFileService.UploadFile(file);
        }
        
        return RedirectToAction("Index");
    }
    
    /// <summary>
    /// Удаляет файл из папки wwwroot/uploads
    /// </summary>
    /// <param name="fileId"></param>
    /// <returns></returns>
    [HttpPost]
    public IActionResult DeleteFile(string fileId)
    {
        if (!string.IsNullOrEmpty(fileId) && Guid.TryParse(fileId, out var id))
        {
            _cdekFileService.DeleteFile(id);
            TempData["Message"] = "Файл успешно удалён";
        }
        else
        {
            TempData["Error"] = "Неверный идентификатор файла";
        }

        return RedirectToAction("Index");
    }

    /// <summary>
    /// Открытие файла в браузере
    /// </summary>
    /// <param name="fileId"></param>
    /// <returns></returns>
    public IActionResult OpenFile(Guid fileId)
    {
        // Находим файл по ID
        var file = _cdekFileService.GetAllFiles()
            .FirstOrDefault(f => f.Id == fileId);

        var extension = Path.GetExtension(file.OriginalName);
        var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", fileId + extension);

        if (!System.IO.File.Exists(filePath))
        {
            return NotFound();
        }
        
        // Определяем MIME-тип по расширению
        var mimeType = extension.ToLower() switch
        {
            ".pdf" => "application/pdf",
            ".jpg" => "image/jpeg",
            ".jpeg" => "image/jpeg",
            ".png" => "image/png",
            ".gif" => "image/gif",
            ".txt" => "text/plain",
            ".doc" => "application/msword",
            ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
            _ => "application/octet-stream"
        };
        
        // Кодируем имя файла для безопасного использования в заголовке
        var contentDisposition = new ContentDisposition
        {
            Inline = true,
            FileName = Uri.EscapeDataString(file.OriginalName) // безопасная кодировка
        };

        Response.Headers[HeaderNames.ContentDisposition] = contentDisposition.ToString();

        return PhysicalFile(filePath, mimeType);
            
    }
}