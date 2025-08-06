namespace WebPortal.Models
{
    /// <summary>
    /// Device type (computer, laptop, phone...)
    /// </summary>
    public enum Typedevice
    {
        None = 0,
        Computer,
        Laptop,
        Phone,
        Details
    };

    public static class TypedeviceExtensions
    {
        public static string Name(this Typedevice device)
        {
            switch (device)
            {
                case Typedevice.Computer:
                    return "Компьютер";
                case Typedevice.Laptop:
                    return "Ноутбук";
                case Typedevice.Phone:
                    return "Телефон";
                case Typedevice.Details:
                    return "Запчасти";
                default:
                    return "Unknown";
            }
        }
    }
}
