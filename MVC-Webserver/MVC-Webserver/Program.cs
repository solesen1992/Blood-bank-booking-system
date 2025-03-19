using MVC_Webserver.BusinessLogicLayer;
using MVC_Webserver.Servicelayer;

// Create the WebApplication builder
var builder = WebApplication.CreateBuilder(args);

// Add services to the container (for dependency injection).
builder.Services.AddControllersWithViews();

// Register services with the dependency injection container
builder.Services.AddSingleton<IDonorService, DonorService>(); // Register DonorService as a singleton
builder.Services.AddSingleton<IDonorBusinessLogic, DonorBusinessLogic>();
builder.Services.AddSingleton<IAppointmentService, AppointmentService>();
builder.Services.AddSingleton<IAppointmentBusinessLogic, AppointmentBusinessLogic>();

//Register HttpClient for DonoService

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Redirect HTTP requests to HTTPS
app.UseHttpsRedirection();
app.UseStaticFiles();

// Enable routing for the application
app.UseRouting();

// Enable authorization middleware
app.UseAuthorization();

// Configure the default route for MVC controllers
app.MapControllerRoute(
    name: "default", // Name of the route
    pattern: "{controller=Home}/{action=Index}"); // Default route pattern

// Start the web application
app.Run();
