using BBRRevival.ControlPanel.Services.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BBRRevival.ControlPanel.Pages.ViewModels
{
    public partial class DashboardViewModel : ObservableObject
    {
        IServerService serverService = App.Services.GetService<IServerService>();

        [ObservableProperty]
        private string serverStatus = "Stopped";

        public DashboardViewModel() { }

        public void StartServer()
        {
            try
            {
                serverService.Start();
                ServerStatus = "Running";
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void StopServer()
        {
            serverService.Stop();
            ServerStatus = "Stopped";
        }
    }
}
