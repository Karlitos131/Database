using Devices.Domain.Models;

namespace Devices.Application.Interfaces
{
    public interface IDeviceRepository
    {
        Task<List<Device>> GetAllDevicesAsync();
        Task<Device> GetDeviceByIdAsync(string id);
        Task CreateDeviceAsync(Device device);
        Task UpdateDeviceAsync(Device device);
        Task DeleteDeviceAsync(string id);
    }
}