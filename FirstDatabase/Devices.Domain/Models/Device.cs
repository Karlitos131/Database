namespace Devices.Domain.Models
{
    public abstract class Device
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public bool IsEnabled { get; set; }
        public byte[] RowVersion { get; set; }
    }
}