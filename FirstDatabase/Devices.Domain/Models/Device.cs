namespace Devices.Domain.Models
{
    public class Device
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public bool IsEnabled { get; set; }
    }
}