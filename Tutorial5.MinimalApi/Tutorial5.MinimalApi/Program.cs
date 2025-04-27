using Tutorial5.MinimalApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

List<Animal> animals =
[
    new Animal
    {
        Id = 1,
        Name = "Milk",
        Type = "Dog",
        Weight = 1.5,
        FurColor = "Black",

    }
];

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/api/animals", () => animals);
app.MapGet("/api/animals/{id}", (int id) => animals[id-1]);
app.MapPost("/api/animals", (Animal animal) =>
{
    animals.Add(animal);
    return Results.Created($"/api/animals/{animal.Id}", animal);
});

app.MapPut("/api/animals/{id}", (int id, Animal updatedAnimal) =>
{
    var existingAnimal = animals.FirstOrDefault(a => a.Id == id);
    if (existingAnimal is null)
    {
        return Results.NotFound();
    }

    existingAnimal.Name = updatedAnimal.Name;
    existingAnimal.Type = updatedAnimal.Type;
    existingAnimal.Weight = updatedAnimal.Weight;
    existingAnimal.FurColor = updatedAnimal.FurColor;

    return Results.Ok(existingAnimal);
});

app.MapDelete("/api/animals/{id}", (int id) =>
{
    var animal = animals.FirstOrDefault(a => a.Id == id);
    if (animal is null)
    {
        return Results.NotFound();
    }

    animals.Remove(animal);
    return Results.NoContent();
});

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}