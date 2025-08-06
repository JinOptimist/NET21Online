namespace WebPortal.Models
{
    /// <summary>
    /// Type of device by application (gaming, office)
    /// </summary>
    public enum DeviceCategory
    {
        None = 0,
        Gaming,
        Office,
    }

    public static class DeviceCategoryExtensions
    {
        public static string Name(this DeviceCategory device)
        {
            switch (device)
            {
                case DeviceCategory.Gaming:
                    return "Игровой";
                case DeviceCategory.Office:
                    return "Офисный";
                default:
                    return "Unknown";
            }
        }
    }

}
