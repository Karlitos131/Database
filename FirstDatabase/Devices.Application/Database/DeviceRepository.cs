using Devices.Application.Interfaces;
using Devices.Domain.Models;
using Microsoft.Data.SqlClient;

namespace Devices.Application.Database
{
    public class DeviceRepository : IDeviceRepository
    {
        private readonly string _connectionString;

        public DeviceRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Device>> GetAllDevicesAsync()
        {
            var devices = new List<Device>();

            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            var command = new SqlCommand("SELECT Id, Name, IsEnabled FROM Device", connection);
            var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                devices.Add(new Device
                {
                    Id = reader.GetString(0),
                    Name = reader.GetString(1),
                    IsEnabled = reader.GetBoolean(2)
                });
            }

            return devices;
        }

        public async Task<Device> GetDeviceByIdAsync(string id)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            var command = new SqlCommand("SELECT Id, Name, IsEnabled FROM Device WHERE Id = @Id", connection);
            command.Parameters.AddWithValue("@Id", id);

            var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new Device
                {
                    Id = reader.GetString(0),
                    Name = reader.GetString(1),
                    IsEnabled = reader.GetBoolean(2)
                };
            }

            return null;
        }

        public async Task CreateDeviceAsync(Device device)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            var command = new SqlCommand("INSERT INTO Device (Id, Name, IsEnabled) VALUES (@Id, @Name, @IsEnabled)", connection);
            command.Parameters.AddWithValue("@Id", device.Id);
            command.Parameters.AddWithValue("@Name", device.Name);
            command.Parameters.AddWithValue("@IsEnabled", device.IsEnabled);

            await command.ExecuteNonQueryAsync();
        }

        public async Task UpdateDeviceAsync(Device device)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            var command = new SqlCommand("UPDATE Device SET Name = @Name, IsEnabled = @IsEnabled WHERE Id = @Id", connection);
            command.Parameters.AddWithValue("@Id", device.Id);
            command.Parameters.AddWithValue("@Name", device.Name);
            command.Parameters.AddWithValue("@IsEnabled", device.IsEnabled);

            await command.ExecuteNonQueryAsync();
        }

        public async Task DeleteDeviceAsync(string id)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            var command = new SqlCommand("DELETE FROM Device WHERE Id = @Id", connection);
            command.Parameters.AddWithValue("@Id", id);

            await command.ExecuteNonQueryAsync();
        }

        public async Task<Embedded> GetEmbeddedByDeviceIdAsync(string deviceId)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            var command = new SqlCommand("SELECT Id, IpAddress, NetworkName, DeviceId FROM Embedded WHERE DeviceId = @DeviceId", connection);
            command.Parameters.AddWithValue("@DeviceId", deviceId);

            var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new Embedded
                {
                    Id = reader.GetInt32(0),
                    IpAddress = reader.GetString(1),
                    NetworkName = reader.GetString(2),
                    DeviceId = reader.GetString(3)
                };
            }

            return null;
        }

        public async Task CreateEmbeddedAsync(Embedded embedded)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            var command = new SqlCommand("INSERT INTO Embedded (IpAddress, NetworkName, DeviceId) VALUES (@IpAddress, @NetworkName, @DeviceId)", connection);
            command.Parameters.AddWithValue("@IpAddress", embedded.IpAddress);
            command.Parameters.AddWithValue("@NetworkName", embedded.NetworkName);
            command.Parameters.AddWithValue("@DeviceId", embedded.DeviceId);

            await command.ExecuteNonQueryAsync();
        }

        public async Task<PersonalComputer> GetPersonalComputerByDeviceIdAsync(string deviceId)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            var command = new SqlCommand("SELECT Id, OperationSystem, DeviceId FROM PersonalComputer WHERE DeviceId = @DeviceId", connection);
            command.Parameters.AddWithValue("@DeviceId", deviceId);

            var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new PersonalComputer
                {
                    Id = reader.GetInt32(0),
                    OperationSystem = reader.GetString(1),
                    DeviceId = reader.GetString(2)
                };
            }

            return null;
        }

        public async Task CreatePersonalComputerAsync(PersonalComputer personalComputer)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            var command = new SqlCommand("INSERT INTO PersonalComputer (OperationSystem, DeviceId) VALUES (@OperationSystem, @DeviceId)", connection);
            command.Parameters.AddWithValue("@OperationSystem", personalComputer.OperationSystem);
            command.Parameters.AddWithValue("@DeviceId", personalComputer.DeviceId);

            await command.ExecuteNonQueryAsync();
        }

        public async Task<Smartwatch> GetSmartwatchByDeviceIdAsync(string deviceId)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            var command = new SqlCommand("SELECT Id, BatteryPercentage, DeviceId FROM Smartwatch WHERE DeviceId = @DeviceId", connection);
            command.Parameters.AddWithValue("@DeviceId", deviceId);

            var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new Smartwatch
                {
                    Id = reader.GetInt32(0),
                    BatteryPercentage = reader.GetInt32(1),
                    DeviceId = reader.GetString(2)
                };
            }

            return null;
        }

        public async Task CreateSmartwatchAsync(Smartwatch smartwatch)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            var command = new SqlCommand("INSERT INTO Smartwatch (BatteryPercentage, DeviceId) VALUES (@BatteryPercentage, @DeviceId)", connection);
            command.Parameters.AddWithValue("@BatteryPercentage", smartwatch.BatteryPercentage);
            command.Parameters.AddWithValue("@DeviceId", smartwatch.DeviceId);

            await command.ExecuteNonQueryAsync();
        }
    }
    
}