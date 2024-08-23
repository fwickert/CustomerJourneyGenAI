using CustomerJourney.API.Extensions;
using CustomerJourney.API.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ILogger>(sp => sp.GetRequiredService<ILogger<Program>>())
    .AddOptions(builder.Configuration)
    .AddAIResponses()
    .AddPromptService()
    .AddSemanticKernelServices();


builder.Services.AddSignalR();

builder.Services.AddCorsPolicy();

var app = builder.Build();

app.MapHub<MessageRelayHub>("/messageRelayHub");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
