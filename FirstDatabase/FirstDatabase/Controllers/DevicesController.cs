using Devices.Application.Interfaces;
using Devices.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace FirstDatabase.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DevicesController : ControllerBase
    {
        private readonly IDeviceService _deviceService;

        public DevicesController(IDeviceService deviceService)
        {
            _deviceService = deviceService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Device>>> GetAllDevices()
        {
            var devices = await _deviceService.GetAllDevicesAsync();
            return Ok(devices);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Device>> GetDeviceById(string id)
        {
            var device = await _deviceService.GetDeviceByIdAsync(id);
            if (device == null)
            {
                return NotFound();
            }
            return Ok(device);
        }

        [HttpPost]
        public async Task<ActionResult> CreateDevice([FromBody] Device device)
        {
            if (device == null)
            {
                return BadRequest("Device data is required.");
            }

            await _deviceService.CreateDeviceAsync(device);
            return CreatedAtAction(nameof(GetDeviceById), new { id = device.Id }, device);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateDevice([FromBody] Device device)
        {
            if (device == null)
            {
                return BadRequest("Device data is required.");
            }

            await _deviceService.UpdateDeviceAsync(device);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDevice(string id)
        {
            await _deviceService.DeleteDeviceAsync(id);
            return NoContent();
        }
    }
}