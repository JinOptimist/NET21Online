namespace WebPortal.Services
{
    public interface IExportService
    {
        string ExportProducts();
        string ExportProductsToFile(string folderPath = null);
    }
}
