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
using C1.Chart;

namespace Demos.ViewModel {
    public class ProductosViewModel : ObservableBase {
        private IProductoRepository dao = new ProductoRepository();
        private ProductoSet db;
        private ObservableCollection<Producto> productos;
        public ObservableCollection<Producto> Productos {
            get => productos;
            protected set {
                if (productos == value) return;
                productos = value;
                RaisePropertyChanged(nameof(Productos));
            }
        }
        private ObservableCollection<Categoria> categorias;
        public ObservableCollection<Categoria> Categorias {
            get => categorias;
            protected set {
                if (categorias == value) return;
                categorias = value;
                RaisePropertyChanged(nameof(Categorias));
            }
        }
        private Categoria categoriaSeleccionada;
        public Categoria CategoriaSeleccionada {
            get => categoriaSeleccionada;
            set {
                if (categoriaSeleccionada == value) return;
                categoriaSeleccionada = value;
                RaisePropertyChanged(nameof(CategoriaSeleccionada));
                RaisePropertyChanged(nameof(SubcategoriasSeleccionada));
            }
        }
        public ObservableCollection<Subcategoria> SubcategoriasSeleccionada => subcategorias == null ? null :
            new ObservableCollection<Subcategoria>(
                subcategorias.Where(o => o.ProductCategoryID == categoriaSeleccionada?.ProductCategoryID)
            );

        private ObservableCollection<Subcategoria> subcategorias;
        public ObservableCollection<Subcategoria> Subcategorias {
            get => subcategorias;
            protected set {
                if (subcategorias == value) return;
                subcategorias = value;
                RaisePropertyChanged(nameof(Subcategorias));
            }
        }
        private Producto elemento;
        public Producto Elemento {
            get => elemento;
            set {
                if (elemento == value) return;
                elemento = value;
                RaisePropertyChanged(nameof(Elemento));
            }
        }

        public async void Carga() {
            db = await dao.CargarAsyc(@"D:\Cursos\dotnet\WPF20210726\Productos.xlsx");
            Categorias = db.Categorias;
            Subcategorias = db.Subcategorias;
            Productos = db.Productos;
            if (Productos != null && Productos.Count > 0) {
                Elemento = Productos[0];
            }
        }

        public DelegateCommand CompletaMuchos2Uno => new DelegateCommand(cmd => Parallel.Invoke(
            () => db.PonCategorias2Subcategorias(),
            () => db.PonSubcategorias2Productos()
            ));
        public DelegateCommand CompletaUno2Muchos => new DelegateCommand(cmd => {
            db.PonSubcategorias2Categorias();
            db.PonProductos2Subcategorias();
        });

        public ObservableCollection<object> ResultSet { get; set; }
        public DelegateCommand Consulta1 => new DelegateCommand(cmd => {
            var rslt = new List<object>();

            foreach (var item in Productos.GroupBy(o => o.Subcategoria)) {
                int sinGama = 0, baja = 0, media = 0, alta = 0, total = 0;
                foreach (var n in item) {
                    total++;
                    if (n.Class == null)
                        sinGama++;
                    else switch (n.Class.Trim()) {
                            case "L": baja++; break;
                            case "M": media++; break;
                            case "H": alta++; break;
                        }
                }
                rslt.Add(new {
                    Subcategoria = item.Key == null ? "(Sin subcategoría)" : item.Key.Name,
                    SinGama = sinGama, Baja = baja, Media = media, Alta = alta, Total = total
                });
            }

            ResultSet = new ObservableCollection<object>(rslt.OrderBy(item => item.GetType().GetProperty("Subcategoria").GetValue(item)));
            RaisePropertyChanged(nameof(ResultSet));
            if (cmd is C1.WPF.Chart.C1FlexChart chart) {
                chart.Series.Clear();
                //chart.Series.Add(new C1.WPF.Chart.Series() { BindingX = "Subcategoria", Binding = "SinGama", SeriesName = "Sin Gama" });
                chart.Series.Add(new C1.WPF.Chart.Series() { BindingX = "Subcategoria", Binding = "Baja", SeriesName = "Baja" });
                chart.Series.Add(new C1.WPF.Chart.Series() { BindingX = "Subcategoria", Binding = "Media", SeriesName = "Media" });
                chart.Series.Add(new C1.WPF.Chart.Series() { BindingX = "Subcategoria", Binding = "Alta", SeriesName = "Alta" });
            }
        });
        public DelegateCommand Consulta2 => new DelegateCommand(cmd => {
            ResultSet = new ObservableCollection<object>(
                Productos.GroupBy(o => o.ProductSubcategoryID)
                    .Select(g => new {
                        ProductSubcategoryID = g.Key,
                        SinGama = g.Where(w => w.Class == null).Count(),
                        Baja = g.Where(w => w.Class?.Trim() == "L").Count(),
                        Media = g.Where(w => w.Class?.Trim() == "M").Count(),
                        Alta = g.Where(w => w.Class?.Trim() == "H").Count(),
                        Total = g.Count()
                    })
                    .Join(Subcategorias.Concat(
                        new Subcategoria[] { new Subcategoria() { ProductSubcategoryID = -1, Name = "(Sin subcategoría)" } }
                        ),
                        p => p.ProductSubcategoryID.HasValue ? p.ProductSubcategoryID : -1,
                        s => s.ProductSubcategoryID,
                        (p, s) => new {
                            Subcategoria = s.Name,
                            p.SinGama,
                            p.Baja,
                            p.Media,
                            p.Alta,
                            p.Total
                        }
                    ).OrderBy(o => o.Subcategoria)
                    );
            RaisePropertyChanged(nameof(ResultSet));
            if (cmd is C1.WPF.Chart.C1FlexChart chart) {
                chart.Series.Clear();
                chart.Series.Add(new C1.WPF.Chart.Series() { BindingX = "Subcategoria", Binding = "SinGama", SeriesName = "Sin Gama" });
                chart.Series.Add(new C1.WPF.Chart.Series() { BindingX = "Subcategoria", Binding = "Baja", SeriesName = "Baja" });
                chart.Series.Add(new C1.WPF.Chart.Series() { BindingX = "Subcategoria", Binding = "Media", SeriesName = "Media" });
                chart.Series.Add(new C1.WPF.Chart.Series() { BindingX = "Subcategoria", Binding = "Alta", SeriesName = "Alta" });
            }
        });
        public DelegateCommand Consulta3 => new DelegateCommand(cmd => {
            ResultSet = new ObservableCollection<object>(
                Categorias.Select(s => new {
                    Categoria = s.Name,
                    Subcategorias = s.Subcategorias.Count(),
                    Productos = s.Subcategorias.SelectMany(n => n.Productos).Count()
                }));
            RaisePropertyChanged(nameof(ResultSet));
            if (cmd is C1.WPF.Chart.C1FlexChart chart) {
                chart.Series.Clear();
                chart.Series.Add(new C1.WPF.Chart.Series() { BindingX = "Categoria", Binding = "Subcategorias", SeriesName = "Subcategorias" });
                chart.Series.Add(new C1.WPF.Chart.Series() { BindingX = "Categoria", Binding = "Productos", SeriesName = "Productos" });
            }
        });

        public List<C1.Chart.ChartType> ChartTypes => new List<ChartType>((IEnumerable<ChartType>)Enum.GetValues(typeof(ChartType)));
    }
}
