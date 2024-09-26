using exercise.wwwapi.EndPoints;
using exercise.wwwapi.Models;
using exercise.wwwapi.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IGameService>(provider =>
{
    var player1 = new Player { name = "john" };
    var player2 = new Player { name = "smith" };
    return new GameService(player1, player2);



});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.ConfigureBattleshipAPIs();


app.Run();



