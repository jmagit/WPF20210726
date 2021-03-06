//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Demos.Domains.Core;

namespace Demos.Domains.Entidades {
    public partial class Subcategoria : Entidad {
        public Subcategoria() {
            this.Productos = new ObservableCollection<Producto>();
        }


        private int _ProductSubcategoryID;
        public int ProductSubcategoryID {
            get => _ProductSubcategoryID;
            set {
                if (_ProductSubcategoryID == value) return;
                _ProductSubcategoryID = value;
                RaiseValidateAndChangedProperty(nameof(ProductSubcategoryID));
            }
        }

        private int _ProductCategoryID;
        public int ProductCategoryID {
            get => _ProductCategoryID;
            set {
                if (_ProductCategoryID == value) return;
                _ProductCategoryID = value;
                if (Categoria != null && _ProductCategoryID != Categoria.ProductCategoryID) {
                    _Categoria = null;
                    RaisePropertyChanged(nameof(Categoria));
                }
                RaiseValidateAndChangedProperty(nameof(ProductCategoryID));
            }
        }

        private string _Name;
        public string Name {
            get => _Name;
            set {
                if (_Name == value) return;
                _Name = value;
                RaiseValidateAndChangedProperty(nameof(Name));
            }
        }

        private System.Guid _rowguid;
        public System.Guid rowguid {
            get => _rowguid;
            set {
                if (_rowguid == value) return;
                _rowguid = value;
                RaiseValidateAndChangedProperty(nameof(rowguid));
            }
        }

        private System.DateTime _ModifiedDate;
        public System.DateTime ModifiedDate {
            get => _ModifiedDate;
            set {
                if (_ModifiedDate == value) return;
                _ModifiedDate = value;
                RaiseValidateAndChangedProperty(nameof(ModifiedDate));
            }
        }

        private ObservableCollection<Producto> _Productos;
        public virtual ObservableCollection<Producto> Productos {
            get => _Productos;
            set {
                if (_Productos == value) return;
                _Productos = value;
                RaiseValidateAndChangedProperty(nameof(Productos));
            }
        }
        private Categoria _Categoria;
        public virtual Categoria Categoria {
            get => _Categoria;
            set {
                if (_Categoria == value) return;
                _Categoria = value;
                if (_Categoria != null && ProductCategoryID != _Categoria.ProductCategoryID)
                    ProductCategoryID = _Categoria.ProductCategoryID;
                RaiseValidateAndChangedProperty(nameof(Categoria));
            }
        }

        protected override List<string> ValidateProperty(string propertyName) {
            List<string> lst = new List<string>();
            switch (propertyName) {
                case "": // Validaciones cruzadas
                    break;
            }
            return lst;
        }

    }
}
