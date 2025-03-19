using DesktopApp.BusinessLogicLayer;
using DesktopApp.GUI;
using DesktopApp.ServiceLayer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;

namespace DesktopApp
{
    /// <summary>
    /// The Program class is the entry point of the application.
    /// and runs the main form.
    /// </summary>
    internal static class Program
    {
        
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainPage());
        }
    }
}
