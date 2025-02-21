// Create a new instance of the WebApplication builder, passing the arguments from the command line
using API.BusinessLogicLayer;
using API.DatabaseLayer;
using API.Models;

var builder = WebApplication.CreateBuilder(args);

// Register services with the dependency injection container
builder.Services.AddSingleton<IDonorAccess, DonorAccess>(); // IDonorAccess is associated with the DonorAccess implementation
builder.Services.AddSingleton<IDonorBusinessLogic, DonorBusinessLogic>(); // Injects DonorLogic when IDonorLogic is needed
builder.Services.AddSingleton<IAppointmentAccess, AppointmentAccess>();
builder.Services.AddSingleton<IAppointmentBusinessLogic, AppointmentBusinessLogic>();

// Add support for controllers to the application
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

app.UseAuthorization();

app.MapControllers();

app.Run();