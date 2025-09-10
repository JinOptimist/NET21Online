using WebPortal.DbStuff.Models.CompShop.Devices;

namespace WebPortal.DbStuff.Repositories.Interfaces.CompShop
{
    public interface IDeviceRepository : IBaseRepository<Device>
    {
        List<Device> GetAllPopular();
        Device GetDeviceWithAll(Device device);
        IEnumerable<Device> GetIEnumerableDeviceWithCategoryAndType();
        bool IsUniqName(string name);
    }
}