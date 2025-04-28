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
        Task<Embedded> GetEmbeddedByDeviceIdAsync(string deviceId);
        Task CreateEmbeddedAsync(Embedded embedded);
        Task<PersonalComputer> GetPersonalComputerByDeviceIdAsync(string deviceId);
        Task CreatePersonalComputerAsync(PersonalComputer personalComputer);
        Task<Smartwatch> GetSmartwatchByDeviceIdAsync(string deviceId);
        Task CreateSmartwatchAsync(Smartwatch smartwatch);
    }
}