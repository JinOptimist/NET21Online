using WebPortal.Models.Cdek;
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
        {
             Directory.CreateDirectory(_uploadPath);
        }
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

        // создаём Id
        var id = Guid.NewGuid();
        var extension = Path.GetExtension(file.FileName);
        
        // имя файла будет вида "guid.docx"
        var storedName = id + extension;
        var filePath = Path.Combine(_uploadPath, storedName);

        // Создаём и записываем файл на диск
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            file.CopyTo(stream);
        }
        // сохраняем оригинальное имя в отдельный .txt
        File.WriteAllText(Path.Combine(_uploadPath, id + ".txt"), file.FileName);
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
            if (ext == ".txt") continue;

            var id = Path.GetFileNameWithoutExtension(filePath);

            var metaPath = Path.Combine(_uploadPath, id + ".txt");
            var originalName = File.Exists(metaPath)
                ? File.ReadAllText(metaPath)
                : Path.GetFileName(filePath);

            files.Add(new AdminCdekFileViewModel
            {
                Id = Guid.Parse(id),
                OriginalName = originalName
            });
        }

        return files;
    }
    
    /// <summary>
    /// Удаляет файл по Id
    /// </summary>
    /// <param name="fileName"></param>
    public void DeleteFile(Guid id)
    {
        var file = Directory.GetFiles(_uploadPath)
            .FirstOrDefault(f => Path.GetFileNameWithoutExtension(f) == id.ToString());

        var metaFile = Path.Combine(_uploadPath, id + ".txt");
        if (file != null && File.Exists(file))
        {
            File.Delete(file);
        }
    }
}