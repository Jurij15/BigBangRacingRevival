using BBRRevival.ControlPanel.Pages.ViewModels;
using BBRRevival.ControlPanel.Pages.Views;
using BBRRevival.ControlPanel.Services;
using BBRRevival.ControlPanel.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Runtime.CompilerServices;
using System.Windows;
using Wpf.Ui;

namespace BBRRevival.ControlPanel
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IServiceProvider Services { get; private set; }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            ServiceCollection services = new ServiceCollection();

            services.AddSingleton<IServerService, ServerService>();

            Services= services.BuildServiceProvider();
        }
    }

}
