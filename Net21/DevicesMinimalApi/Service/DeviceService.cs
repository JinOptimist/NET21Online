using DevicesMinimalApi.DbStuff.Model;
using DevicesMinimalApi.DbStuff.Repository;
using DevicesMinimalApi.ViewModels;

namespace DevicesMinimalApi.Service
{
    public class DeviceService
    {
        private readonly DeviceRepository _deviceRepository;

        public DeviceService(DeviceRepository deviceRepository)
        {
            _deviceRepository = deviceRepository;
        }

        public int CreateDevice(DeviceViewModel device)
        {
            var deviceDB = new Device
            {
                Name = device.Name,
                CategoryName = device.CategoryName,
                UrlImage = device.UrlImage,
                Price = device.Price,
            };

            _deviceRepository.Add(deviceDB);
            return deviceDB.Id;
        }
    }
}
