using Devices.Application.Database;
using Devices.Application.Interfaces;
using Devices.Application.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IDeviceRepository>(provider =>
    new DeviceRepository(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IDeviceService, DeviceService>();
builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

app.Run();