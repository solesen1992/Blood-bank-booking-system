using DesktopApp.ServiceLayer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Windows.Forms;

namespace DesktopApp
{
    /// <summary>
    /// The Program class is the entry point of the application. It sets up the application configuration, dependency injection,
    /// and runs the main form.
    /// </summary>
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// <param name="args">The command-line arguments.</param>
        [STAThread]
        static void Main(string[] args)
        { // Handle how the GUI looks
            Application.SetHighDpiMode(HighDpiMode.SystemAware); // Set the high DPI mode to system aware
            Application.EnableVisualStyles(); // Enable visual styles
            Application.SetCompatibleTextRenderingDefault(false); // Set compatible text rendering default

            var builder = Host.CreateDefaultBuilder(args) // Create a default host builder
                .ConfigureAppConfiguration((context, config) => // Configure the application configuration
                {
                    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true); // Add the appsettings.json file
                })
                .ConfigureServices((context, services) => // Configure the services
                {
                    // Register IConfiguration
                    services.AddSingleton<IConfiguration>(context.Configuration);
                });

            var host = builder.Build(); // Build the host

            // Get the IConfiguration object
            var configuration = host.Services.GetRequiredService<IConfiguration>();

            // Run the application
            Application.Run(new MainPage(configuration)); // Directly instantiate and run the main form
        }
    }
}
