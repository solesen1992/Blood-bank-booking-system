using WebApp.BusinessLogicLayer;
using WebApp.ServiceLayer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register services with the dependency injection container
builder.Services.AddSingleton<IDonorService, DonorService>(); // Register DonorService as a singleton
builder.Services.AddSingleton<IDonorBusinessLogic, DonorBusinessLogic>();
builder.Services.AddSingleton<IAppointmentService, AppointmentService>();
builder.Services.AddSingleton<IAppointmentBusinessLogic, AppointmentBusinessLogic>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
