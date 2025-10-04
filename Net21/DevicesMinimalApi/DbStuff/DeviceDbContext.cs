using DevicesMinimalApi.DbStuff.Model;
using Microsoft.EntityFrameworkCore;

namespace DevicesMinimalApi.DbStuff
{
    public class DeviceDbContext : DbContext
    {
        public DeviceDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Device> Devices { get; set; }
    }
}
