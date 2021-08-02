using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demos.Domains.Core;
using System.ComponentModel;
using Demos.Domains.Entidades;
using Demos.Domains.Contracts.Repositories;
using Demos.Infraestructure.Data.Repositories;
using System.Threading;

namespace Demos.ViewModel {
    public class PedidoViewModel : ObservableBase {
        private IPedidoRepository dao = new PedidoRepository();
        private ObservableCollection<Pedido> listado;
        private Pedido elemento;

        public ObservableCollection<Pedido> Listado {
            get => listado;
            protected set {
                if (listado == value) return;
                listado = value;
                RaisePropertyChanged(nameof(Listado));
            }
        }
        public Pedido Elemento {
            get => elemento;
            set {
                if (elemento == value) return;
                elemento = value;
                RaisePropertyChanged(nameof(Elemento));
            }
        }

        private string mensaje = "";
        public string Mensaje {
            get => mensaje;
            set {
                if (mensaje == value) return;
                mensaje = value;
                RaisePropertyChanged(nameof(Mensaje));
            }
        }

        private bool esperando = false;
        public bool Esperando {
            get => esperando;
            set {
                if (esperando == value) return;
                esperando = value;
                RaisePropertyChanged(nameof(Esperando));
            }
        }


        public async void Carga(String tipo = "") {
            Esperando = true;
            var ini = DateTime.Now;
            Mensaje = $"Carga: {DateTime.Now}";
            switch (tipo) {
                case "Async Wait":
                    Listado = await dao.CargarAsync(@"D:\Cursos\dotnet\WPF20210726\Pedidos.xlsx");
                    break;
                case "Sync Wait":
                    Listado = dao.Cargar(@"D:\Cursos\dotnet\WPF20210726\Pedidos.xlsx");
                    break;
                case "Async Refresh":
                    Listado = new ObservableCollection<Pedido>();
                    await dao.CargarAsync(@"D:\Cursos\dotnet\WPF20210726\Pedidos.xlsx", Listado, App.Current.Dispatcher.Invoke);
                    break;
                case "Sync Refresh":
                    Listado = new ObservableCollection<Pedido>();
                    dao.Cargar(@"D:\Cursos\dotnet\WPF20210726\Pedidos.xlsx", Listado);
                    break;
                default:
                    Parallel.Invoke(
                        () => Carga("Async Wait"),
                        async () => {
                            var db = await (new ProductoRepository()).CargarAsyc(@"D:\Cursos\dotnet\WPF20210726\Productos.xlsx");
                            db.PonCategorias2Subcategorias();
                            db.PonSubcategorias2Productos();
                            db.PonSubcategorias2Categorias();
                            db.PonProductos2Subcategorias();
                            Categorias = db.Categorias;
                            Subcategorias = db.Subcategorias;
                            Productos = db.Productos;
                        }
                        );
                    break;
            }
            Mensaje += $" -> {DateTime.Now - ini}";
            Esperando = false;
        }
        public ObservableCollection<Categoria> Categorias { get; set; } = new ObservableCollection<Categoria>();
        public ObservableCollection<Subcategoria> Subcategorias { get; set; } = new ObservableCollection<Subcategoria>();
        public ObservableCollection<Producto> Productos { get; set; } = new ObservableCollection<Producto>();

        private class Bloqueo {
            public Decimal Total { get; set; } = 0;
        }
        public DelegateCommand Tiempos => new DelegateCommand(cmd => {
            Esperando = true;
            Decimal total = 0;
            var ini = DateTime.Now;
            total = 0;
            foreach (var item in Listado) {
                total += item.Total;
            }
            Mensaje = $"Secuencial:    {total} en {DateTime.Now - ini}";
            ini = DateTime.Now;
            total = 0;
            Parallel.ForEach(Listado, item => total += item.Total);
            Mensaje += $"\nParalelo1:     {total} en {DateTime.Now - ini}";
            var bloqueo = new Bloqueo();
            ini = DateTime.Now;
            Parallel.ForEach(Listado, item => { lock (bloqueo) { bloqueo.Total += item.Total; } });
            Mensaje += $"\nParalelo2:     {bloqueo.Total} en {DateTime.Now - ini}";
            bloqueo = new Bloqueo();
            ini = DateTime.Now;
            Parallel.ForEach<Pedido, decimal>(Listado, // source collection
                () => 0, // method to initialize the local variable
                (item, loop, subtotal) => { // method invoked by the loop on each iteration                           
                    subtotal += item.Total; //modify local variable
                    return subtotal; // value to be passed to next iteration
                },
                // Method to be executed when each partition has completed.
                // finalResult is the final value of subtotal for a particular partition.
                (finalResult) => { lock (bloqueo) { bloqueo.Total += finalResult; } }
                );
            Mensaje += $"\nParalelo3:     {bloqueo.Total} en {DateTime.Now - ini}";
            ini = DateTime.Now;
            total = Listado.Sum(item => item.Total);
            Mensaje += $"\nLINQ:          {total} en {DateTime.Now - ini}";
            ini = DateTime.Now;
            total = Listado.AsParallel().Sum(item => item.Total);
            Mensaje += $"\nPLINQ:         {total} en {DateTime.Now - ini}";
            Esperando = false;
        });
        public ObservableCollection<object> ResultSet { get; set; }
        public DelegateCommand Consulta1s => new DelegateCommand(cmd => {
            var ini = DateTime.Now;
            var rslt = new ObservableCollection<object>();
            ResultSet = new ObservableCollection<object>(
                Listado
                    .GroupBy(o => new { Año = o.DueDate.Year, Trimestre = Math.Abs((o.DueDate.Month - 1) / 3) + 1 })
                    .Select(g => new { g.Key.Año, g.Key.Trimestre, Pedidos = g.Count(), Ventas = g.Sum(p => p.TotalDue) })
                    .OrderBy(o => (o.Año, o.Trimestre))
                );
            RaisePropertyChanged(nameof(ResultSet));
            Mensaje = $"Duración: {DateTime.Now - ini}";
            if (cmd is C1.WPF.Chart.C1FlexChart chart) {
                chart.Series.Clear();
                foreach (var grupo in ResultSet.GroupBy(item => item.GetType().GetProperty("Año").GetValue(item)))
                    chart.Series.Add(new C1.WPF.Chart.Series() {
                        BindingX = "Trimestre", Binding = "Pedidos",
                        SeriesName = grupo.Key.ToString(), ItemsSource = grupo
                    });
            }
        });
        public DelegateCommand Consulta1p => new DelegateCommand(cmd => {
            var ini = DateTime.Now;
            var rslt = new ObservableCollection<object>();
            ResultSet = new ObservableCollection<object>(
                Listado.AsParallel()
                    .GroupBy(o => new { Año = o.DueDate.Year, Trimestre = Math.Abs((o.DueDate.Month - 1) / 3) + 1 })
                    .Select(g => new { g.Key.Año, g.Key.Trimestre, Pedidos = g.Count(), Ventas = g.Sum(p => p.TotalDue) })
                    .AsSequential()
                    .OrderBy(o => (o.Año, o.Trimestre))
                );
            RaisePropertyChanged(nameof(ResultSet));
            Mensaje = $"Duración: {DateTime.Now - ini}";
            if (cmd is C1.WPF.Chart.C1FlexChart chart) {
                chart.Series.Clear();
                foreach (var grupo in ResultSet.GroupBy(item => item.GetType().GetProperty("Año").GetValue(item)))
                    chart.Series.Add(new C1.WPF.Chart.Series() {
                        BindingX = "Trimestre", Binding = "Ventas",
                        SeriesName = grupo.Key.ToString(), ItemsSource = grupo
                    });
            }
        });
        public DelegateCommand Consulta2s => new DelegateCommand(cmd => {
            var ini = DateTime.Now;
            ResultSet = new ObservableCollection<object>(
                Listado.SelectMany(p => p.Lineas)
                    .GroupBy(l => l.ProductID)
                    .Select(r => new { ProductID = r.Key, Unidades = r.Sum(l => l.OrderQty), Ventas = r.Sum(l => l.LineTotal) })
                    .OrderBy(o => o.ProductID)
            );
            RaisePropertyChanged(nameof(ResultSet));
            Mensaje = $"Duración: {DateTime.Now - ini}";
        });
        public DelegateCommand Consulta2p => new DelegateCommand(cmd => {
            var ini = DateTime.Now;
            ResultSet = new ObservableCollection<object>(
                Listado.AsParallel().SelectMany(p => p.Lineas)
                    .GroupBy(l => l.ProductID)
                    .Select(r => new { ProductID = r.Key, Unidades = r.Sum(l => l.OrderQty), Ventas = r.Sum(l => l.LineTotal) })
                    .AsSequential()
                    .OrderBy(o => o.ProductID)
            );
            RaisePropertyChanged(nameof(ResultSet));
            Mensaje = $"Duración: {DateTime.Now - ini}";
        });
        public DelegateCommand Consulta3p => new DelegateCommand(cmd => {
            var ini = DateTime.Now;
            ResultSet = new ObservableCollection<object>(
                Productos.AsParallel()
                    .GroupJoin(
                        Listado.AsParallel().SelectMany(p => p.Lineas),
                        p => p.ProductID,
                        r => r.ProductID,
                        (p, r) => new { Producto = p.Name, Unidades = r.Sum(l => l.OrderQty), Ventas = r.Sum(l => l.LineTotal) }
                        ).OrderBy(o => o.Producto)
            );
            RaisePropertyChanged(nameof(ResultSet));
            Mensaje = $"Duración: {DateTime.Now - ini}";
        });
    }
}
