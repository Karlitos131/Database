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
    }
}