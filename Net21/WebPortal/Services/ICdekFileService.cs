using WebPortal.Models.Cdek;

namespace WebPortal.Services.Permissions;

public interface ICdekFileService
{
    /// <summary>
    /// Загружает файл на сервер (в папку wwwroot/uploads)
    /// </summary>
    /// <param name="file"></param>
    void UploadFile(IFormFile file);
    
    /// <summary>
    /// Возвращает список всех файлов в папке uploads
    /// </summary>
    /// <returns></returns>
    List<AdminCdekFileViewModel> GetAllFiles();
    
    /// <summary>
    /// Удаляет файл по имени
    /// </summary>
    /// <param name="fileName"></param>
    void DeleteFile(Guid id);
}