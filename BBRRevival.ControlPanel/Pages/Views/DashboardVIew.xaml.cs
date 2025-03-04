﻿using BBRRevival.ControlPanel.Pages.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BBRRevival.ControlPanel.Pages.Views
{
    /// <summary>
    /// Interaction logic for DashboardVIew.xaml
    /// </summary>
    public partial class DashboardVIew : Page
    {
        public DashboardViewModel ViewModel { get; set; } = new();

        public DashboardVIew()
        {
            InitializeComponent();
            this.DataContext = ViewModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.StartServer();
        }

        private void StopServerButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.StopServer();
        }
    }
}
