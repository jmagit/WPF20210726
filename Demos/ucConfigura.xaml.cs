using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Lógica de interacción para ucConfigura.xaml
    /// </summary>
    public partial class ucConfigura : UserControl {
        public ucConfigura() {
            InitializeComponent();
            Task.Run(() => {
                while (true) {
                    var t = DateTime.Now.ToString();
                    //Console.WriteLine(DateTime.Now.ToString());
                    Dispatcher.Invoke(() => {
                        txtOrigen.Text = t;
                    });
                    Thread.Sleep(500);
                }
            });
        }
    }
}
