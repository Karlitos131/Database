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

        public async Task<Embedded> GetEmbeddedByDeviceIdAsync(string deviceId)
        {
            return await _deviceRepository.GetEmbeddedByDeviceIdAsync(deviceId);
        }

        public async Task CreateEmbeddedAsync(Embedded embedded)
        {
            await _deviceRepository.CreateEmbeddedAsync(embedded);
        }

        public async Task<PersonalComputer> GetPersonalComputerByDeviceIdAsync(string deviceId)
        {
            return await _deviceRepository.GetPersonalComputerByDeviceIdAsync(deviceId);
        }

        public async Task CreatePersonalComputerAsync(PersonalComputer personalComputer)
        {
            await _deviceRepository.CreatePersonalComputerAsync(personalComputer);
        }

        public async Task<Smartwatch> GetSmartwatchByDeviceIdAsync(string deviceId)
        {
            return await _deviceRepository.GetSmartwatchByDeviceIdAsync(deviceId);
        }

        public async Task CreateSmartwatchAsync(Smartwatch smartwatch)
        {
            await _deviceRepository.CreateSmartwatchAsync(smartwatch);
        }
    }
}