using Devices.Application.Interfaces;
using Devices.Domain.Models;

namespace Devices.Application.Services
{
    public class DeviceService : IDeviceService
    {
        private readonly IDeviceRepository _deviceRepository;

        public DeviceService(IDeviceRepository deviceRepository)
        {
            _deviceRepository = deviceRepository;
        }

        public async Task<List<Device>> GetAllDevicesAsync()
        {
            return await _deviceRepository.GetAllDevicesAsync();
        }

        public async Task<Device> GetDeviceByIdAsync(string id)
        {
            return await _deviceRepository.GetDeviceByIdAsync(id);
        }

        public async Task CreateDeviceAsync(Device device)
        {
            await _deviceRepository.CreateDeviceAsync(device);
        }

        public async Task UpdateDeviceAsync(Device device)
        {
            await _deviceRepository.UpdateDeviceAsync(device);
        }

        public async Task DeleteDeviceAsync(string id)
        {
            await _deviceRepository.DeleteDeviceAsync(id);
        }
    }
}