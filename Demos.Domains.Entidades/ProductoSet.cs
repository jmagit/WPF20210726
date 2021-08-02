using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demos.Domains.Entidades {
    public class ProductoSet {
        public ObservableCollection<Categoria> Categorias { get; set; } = new ObservableCollection<Categoria>();
        public ObservableCollection<Subcategoria> Subcategorias { get; set; } = new ObservableCollection<Subcategoria>();
        public ObservableCollection<Producto> Productos { get; set; } = new ObservableCollection<Producto>();

        public void PonCategorias2Subcategorias() {
            Parallel.ForEach(Categorias, item => {
                foreach (var n in Subcategorias.Where(o => o.ProductCategoryID == item.ProductCategoryID))
                    n.Categoria = item;
            });
        }
        public void PonSubcategorias2Productos() {
            Parallel.ForEach(Subcategorias, item => {
                foreach (var n in Productos.Where(o => o.ProductSubcategoryID == item.ProductSubcategoryID))
                    n.Subcategoria = item;
            });
        }
        public void PonSubcategorias2Categorias() {
            foreach (var item in Subcategorias.GroupBy(o => o.ProductCategoryID)) {
                var categoria = Categorias.FirstOrDefault(o => o.ProductCategoryID == item.Key);
                if (categoria == null) continue;
                categoria.Subcategorias.Clear();
                foreach (var n in item)
                    categoria.Subcategorias.Add(n);
            }
        }
        public void PonProductos2Subcategorias() {
            foreach (var item in Productos.GroupBy(o => o.ProductSubcategoryID)) {
                if (item.Key == null) continue;
                var subcategoria = Subcategorias.FirstOrDefault(o => o.ProductSubcategoryID == item.Key);
                if (subcategoria == null) continue;
                subcategoria.Productos.Clear();
                foreach (var n in item)
                    subcategoria.Productos.Add(n);
            }
        }
    }
}
