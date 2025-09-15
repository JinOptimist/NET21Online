using WebPortal.Models.Cdek;
using WebPortal.Services.Permissions;

namespace WebPortal.Services;

public class CdekFileService : ICdekFileService
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly AuthService _authService;
    private readonly string _uploadPath;

    public CdekFileService(IWebHostEnvironment webHostEnvironment, AuthService authService)
        {
            _webHostEnvironment = webHostEnvironment;
            _authService = authService;
            
            // Используем IWebHostEnvironment для правильного пути
            _uploadPath = GetUploadFolderPath(); 
    
            // если папки нет — создаём
            if (!Directory.Exists(_uploadPath))
            {
                 Directory.CreateDirectory(_uploadPath);
            }
        }
    
    private string GetUploadFolderPath()
    {
        return Path.Combine(_webHostEnvironment.WebRootPath, "uploads"); 
    }

    private string GetFilePath(Guid fileId, string extension)
    {
        return Path.Combine(GetUploadFolderPath(), fileId + extension);
    }

    private string GetMetaFilePath(Guid fileId)
    {
        return Path.Combine(GetUploadFolderPath(), fileId + ".txt"); 
    }
    
    /// <summary>
    /// Загружает файл на сервер
    /// </summary>
    /// <param name="file"></param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="ArgumentException"></exception>
    public void UploadFile(IFormFile file)
    {
        // Проверяем, что файл не пустой
        if (file == null)   
        {
            throw new ArgumentNullException(nameof(file), $"Файл не может быть null");
        }

        if (file.Length == 0)
        {
            throw new ArgumentException($"Файл не может быть пустым", nameof(file));
        }
        
        var fileId = Guid.NewGuid();
        var extension = Path.GetExtension(file.FileName);
        
        // Генерируем уникальное имя файла
        var filePath = GetFilePath(fileId, extension);
        var metaPath = GetMetaFilePath(fileId);

        // Создаём и записываем файл на диск
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            file.CopyTo(stream);
        }
        
        // сохраняем оригинальное имя в отдельный .txt
        File.WriteAllText(metaPath, file.FileName);
    }

    /// <summary>
    /// Получаем список файлов (по ID)
    /// </summary>
    /// <returns></returns>
    public List<AdminCdekFileViewModel> GetAllFiles()
    {
        var files = new List<AdminCdekFileViewModel>();

        foreach (var filePath in Directory.GetFiles(_uploadPath))
        {
            var ext = Path.GetExtension(filePath);

            // пропускаем вспомогательные .txt
            if (ext == ".txt")
            {
                continue;
            }

            var id = Path.GetFileNameWithoutExtension(filePath);

            var metaPath = Path.Combine(_uploadPath, id + ".txt");
            var originalName = File.Exists(metaPath)
                ? File.ReadAllText(metaPath)
                : Path.GetFileName(filePath);

            if (Guid.TryParse(id, out var guidId))
            {
                files.Add(new AdminCdekFileViewModel
                {
                    Id = guidId,
                    OriginalName = originalName
                });
            }
        }

        return files;
    }   
    
    /// <summary>
    /// Удаляет файл по Id
    /// </summary>
    /// <param name="id"></param>
    public void DeleteFile(Guid id)
    {
        // Удаляем ВСЕ файлы с этим ID (основной + .txt)
        var filesToDelete = Directory.GetFiles(_uploadPath)
            .Where(f => Path.GetFileNameWithoutExtension(f) == id.ToString())
            .ToList();

        foreach (var filePath in filesToDelete)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}