using BBRRevival.ControlPanel.Pages.ViewModels;
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
    /// Interaction logic for RequestsView.xaml
    /// </summary>
    public partial class RequestsView : Page
    {
        public RequestsViewModel ViewModel { get; }

        public RequestsView()
        {
            if (ViewModel is null)
            {
                ViewModel = new();
            }

            InitializeComponent();
            this.DataContext = ViewModel;
        }
    }
}
