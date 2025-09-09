using WebPortal.Services.Permissions;

namespace WebPortal.Services;

public class CdekFileService : ICdekFileService
{
    private readonly string _uploadPath;
    
    public CdekFileService()
    {
        // Папка для хранения файлов
        _uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
        
        // если папки нет — создаём
        if (!Directory.Exists(_uploadPath))
            Directory.CreateDirectory(_uploadPath);
    }

    /// <summary>
    /// Загружает файл на сервер
    /// </summary>
    /// <param name="file">Файл, выбранный пользователем</param>
    public void UploadFile(IFormFile file)
    {
        // Проверяем, что файл не пустой
        if (file == null || file.Length == 0)
        {
            return;
        }

        // Генерируем уникальное имя файла (чтобы не перезаписывались одинаковые файлы)
        var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
        var filePath = Path.Combine(_uploadPath, fileName);

        // Создаём и записываем файл на диск
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            file.CopyTo(stream);
        }
    }

    /// <summary>
    /// Возвращает список всех файлов в папке uploads
    /// </summary>
    /// <returns>Список имён файлов</returns>
    public List<string> GetAllFiles()
    {
        if (!Directory.Exists(_uploadPath))
        {
            return new List<string>();
        }

        // // Получаем все файлы и возвращаем только имена
        var files = Directory
            .GetFiles(_uploadPath)
            .Select(Path.GetFileName)
            .ToList();
        return files;
    }
    
    /// <summary>
    /// Удаляет файл по имени
    /// </summary>
    /// <param name="fileName"></param>
    public void DeleteFile(string fileName)
    {
        var filePath = Path.Combine(_uploadPath, fileName);

        // Проверяем, существует ли файл, и удаляем
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }
}