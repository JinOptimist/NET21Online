using WebPortal.DbStuff.Repositories.Interfaces.Marketplace;
using System.IO;

namespace WebPortal.Services
{
    public class ExportService : IExportService
    {
        private readonly IProductRepository _productRepository;
        private readonly IWebHostEnvironment _environment;

        public ExportService(IProductRepository productRepository, IWebHostEnvironment environment)
        {
            _productRepository = productRepository;
            _environment = environment;
        }

        public string ExportProducts()
        {
            return GenerateExportContent();
        }

        public string ExportProductsToFile(string folderPath = null)
        {
            var content = GenerateExportContent();
            var savePath = GetExportFilePath(folderPath);
            var directory = Path.GetDirectoryName(savePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            File.WriteAllText(savePath, content, System.Text.Encoding.UTF8);

            return savePath;
        }

        private string GenerateExportContent()
        {
            var products = _productRepository.GetAll().ToList();
            var exportContent = new System.Text.StringBuilder();

            exportContent.AppendLine("=== КАТАЛОГ ===");
            exportContent.AppendLine($"Экспорт от: {DateTime.Now:dd.MM.yyyy HH:mm}");
            exportContent.AppendLine($"Всего товаров: {products.Count}");
            exportContent.AppendLine();
            exportContent.AppendLine(new string('=', 80));

            foreach (var product in products)
            {
                exportContent.AppendLine($"ID: {product.Id}");
                exportContent.AppendLine($"Тип: {product.ProductType}");
                exportContent.AppendLine($"Название: {product.Name}");
                exportContent.AppendLine($"Бренд: {product.Brand}");
                exportContent.AppendLine($"Цена: {product.Price:C}");
                exportContent.AppendLine($"Описание: {product.Description}");
                exportContent.AppendLine($"Изображение: {product.ImageUrl}");
                exportContent.AppendLine($"Дата добавления: {product.CreatedDate:dd.MM.yyyy}");
                exportContent.AppendLine($"Статус: {(product.IsActive ? "Активен" : "Неактивен")}");
                exportContent.AppendLine(new string('-', 40));
            }

            return exportContent.ToString();
        }

        private string GetExportFilePath(string customFolderPath = null)
        {
            var fileName = $"catalog_export.txt";
            var wwwrootPath = _environment.WebRootPath;
            var exportsFolder = Path.Combine(wwwrootPath, "exports");

            return Path.Combine(exportsFolder, fileName);
        }
    }
}