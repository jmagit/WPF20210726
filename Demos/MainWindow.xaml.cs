using Demos.ViewModel;
using Demos.Views;
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

namespace Demos {
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e) {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e) {
            if(host.Content is IDisposable) {
                (host.Content as IDisposable).Dispose();
            }
            host.Content = new ucConfigura();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e) {
            if (host.Content is IDisposable) {
                (host.Content as IDisposable).Dispose();
            }
            host.Content = new ucOtro();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e) {
            if (host.Content is IDisposable) {
                (host.Content as IDisposable).Dispose();
            }
            host.Content = null;
        }

        TrabajoViewModel vmTrabajo = new TrabajoViewModel();
        private void Button_Click_4(object sender, RoutedEventArgs e) {
            if (host.Content is IDisposable) {
                (host.Content as IDisposable).Dispose();
            }
            var cntr = new ucTrabajosLst();
            // var vm = new TrabajoViewModel();
            cntr.DataContext = vmTrabajo;
            host.Content = cntr;
            //vm.Carga();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e) {
            if (host.Content is IDisposable) {
                (host.Content as IDisposable).Dispose();
            }
            var cntr = new ucTrabajosForm();
            // var vm = new TrabajoViewModel();
            cntr.DataContext = vmTrabajo;
            host.Content = cntr;

        }
    }
}
