using Demos.ViewModel;
using Demos.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        private void btnConfigura_Click(object sender, RoutedEventArgs e) {
            if (host.Content is IDisposable) {
                (host.Content as IDisposable).Dispose();
            }
            host.Content = new ucConfigura();
        }

        private void Otro_Click(object sender, RoutedEventArgs e) {
            if (host.Content is IDisposable) {
                (host.Content as IDisposable).Dispose();
            }
            host.Content = new ucOtro();
        }

        private void Limpia_Click(object sender, RoutedEventArgs e) {
            if (host.Content is IDisposable) {
                (host.Content as IDisposable).Dispose();
            }
            host.Content = null;
        }

        TrabajoViewModel vmTrabajo = new TrabajoViewModel();
        private void TrabajosLst_Click(object sender, RoutedEventArgs e) {

            EventHandler<CancelEventArgs> cerrar = null;
            cerrar = (object s, CancelEventArgs ev) => {
                host.Content = null;
                vmTrabajo.Cerrar -= cerrar;

            };
            vmTrabajo.Cerrar += cerrar;

            if (host.Content is IDisposable) {
                (host.Content as IDisposable).Dispose();
            }
            var cntr = new ucTrabajosLst();
            // var vm = new TrabajoViewModel();
            cntr.DataContext = vmTrabajo;
            host.Content = cntr;
            //vm.Carga();
        }

        private void TrabajosForm_Click(object sender, RoutedEventArgs e) {
            if (host.Content is IDisposable) {
                (host.Content as IDisposable).Dispose();
            }
            var cntr = new ucTrabajosForm();
            // var vm = new TrabajoViewModel();
            cntr.DataContext = vmTrabajo;
            host.Content = cntr;

        }

        private void Origenes_Click(object sender, RoutedEventArgs e) {
            if (host.Content is IDisposable) {
                (host.Content as IDisposable).Dispose();
            }
            var cntr = new ucOrigen();
            cntr.DataContext = new OrigenViewModel();
            host.Content = cntr;
        }
        private void Productos_Click(object sender, RoutedEventArgs e) {
            if (host.Content is IDisposable) {
                (host.Content as IDisposable).Dispose();
            }
            var cntr = new ucProductos();
            var vm = new ProductosViewModel();
            cntr.DataContext = vm;
            host.Content = cntr;
            vm.Carga();
        }
        private void Pedidos_Click(object sender, RoutedEventArgs e) {
            if (host.Content is IDisposable) {
                (host.Content as IDisposable).Dispose();
            }
            var cntr = new ucPedidos();
            var vm = new PedidoViewModel();
            cntr.DataContext = vm;
            host.Content = cntr;
            vm.Carga((sender as Button).CommandParameter.ToString());
        }

    }
}
