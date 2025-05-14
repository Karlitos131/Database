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
        [HttpGet("embedded/{deviceId}")]
        public async Task<ActionResult<Embedded>> GetEmbeddedByDeviceId(string deviceId)
        {
            var embedded = await _deviceService.GetEmbeddedByDeviceIdAsync(deviceId);
            if (embedded == null)
            {
                return NotFound();
            }
            return Ok(embedded);
        }

        [HttpPost("embedded")]
        public async Task<ActionResult> CreateEmbedded([FromBody] Embedded embedded)
        {
            if (embedded == null)
            {
                return BadRequest("Embedded data is required.");
            }

            await _deviceService.CreateEmbeddedAsync(embedded);
            return Created("", embedded);
        }

        [HttpGet("personalcomputer/{deviceId}")]
        public async Task<ActionResult<PersonalComputer>> GetPersonalComputerByDeviceId(string deviceId)
        {
            var pc = await _deviceService.GetPersonalComputerByDeviceIdAsync(deviceId);
            if (pc == null)
            {
                return NotFound();
            }
            return Ok(pc);
        }

        [HttpPost("personalcomputer")]
        public async Task<ActionResult> CreatePersonalComputer([FromBody] PersonalComputer pc)
        {
            if (pc == null)
            {
                return BadRequest("Personal Computer data is required.");
            }

            await _deviceService.CreatePersonalComputerAsync(pc);
            return Created("", pc);
        }

        [HttpGet("smartwatch/{deviceId}")]
        public async Task<ActionResult<Smartwatch>> GetSmartwatchByDeviceId(string deviceId)
        {
            var smartwatch = await _deviceService.GetSmartwatchByDeviceIdAsync(deviceId);
            if (smartwatch == null)
            {
                return NotFound();
            }
            return Ok(smartwatch);
        }

        [HttpPost("smartwatch")]
        public async Task<ActionResult> CreateSmartwatch([FromBody] Smartwatch smartwatch)
        {
            if (smartwatch == null)
            {
                return BadRequest("Smartwatch data is required.");
            }

            await _deviceService.CreateSmartwatchAsync(smartwatch);
            return Created("", smartwatch);
        }
        
        [HttpPost("import")]
        [Consumes("text/plain")]
        public async Task<IActionResult> ImportDevice([FromBody] string content)
        {
            if (string.IsNullOrWhiteSpace(content))
                return BadRequest("Content is empty.");

            var parts = content.Split(',');

            if (parts.Length != 2)
                return BadRequest("Invalid format. Expected: Name,bool");

            var name = parts[0].Trim();
            if (!bool.TryParse(parts[1].Trim(), out var isEnabled))
                return BadRequest("Invalid boolean value.");

            var device = new GenericDevice
            {
                Name = name,
                IsEnabled = isEnabled
            };

            await _deviceService.CreateDeviceAsync(device);

            return CreatedAtAction(nameof(GetDeviceById), new { id = device.Id }, device);
        }
    }
}