
namespace WebPortal.Services.BackgroudServices
{
    public class AvatarCleaneaper : BackgroundService
    {
        public const int SECOND_BETWEEN_CHECKS = 10;

        private IServiceProvider _serviceProvider;

        public AvatarCleaneaper(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var di = _serviceProvider.CreateScope();
            var fileService = di.ServiceProvider.GetService<IFileService>();

            while (!stoppingToken.IsCancellationRequested)
            {
                //using var di = _serviceProvider.CreateScope();
                //var fileService = di.ServiceProvider.GetService<IFileService>();

                try
                {
                    fileService.RemoveOutdateAvatarFile();
                }
                catch (Exception ex)
                {
                    // ignore and try
                }

                await Task.Delay(SECOND_BETWEEN_CHECKS * 1000);
            }
        }
    }
}
