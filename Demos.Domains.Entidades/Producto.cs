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
    public partial class Producto : Entidad {

        private int _ProductID;
        public int ProductID {
            get => _ProductID;
            set {
                if (_ProductID == value) return;
                _ProductID = value;
                RaiseValidateAndChangedProperty(nameof(ProductID));
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

        private string _ProductNumber;
        public string ProductNumber {
            get => _ProductNumber;
            set {
                if (_ProductNumber == value) return;
                _ProductNumber = value;
                RaiseValidateAndChangedProperty(nameof(ProductNumber));
            }
        }

        private bool _MakeFlag;
        public bool MakeFlag {
            get => _MakeFlag;
            set {
                if (_MakeFlag == value) return;
                _MakeFlag = value;
                RaiseValidateAndChangedProperty(nameof(MakeFlag));
            }
        }

        private bool _FinishedGoodsFlag;
        public bool FinishedGoodsFlag {
            get => _FinishedGoodsFlag;
            set {
                if (_FinishedGoodsFlag == value) return;
                _FinishedGoodsFlag = value;
                RaiseValidateAndChangedProperty(nameof(FinishedGoodsFlag));
            }
        }

        private string _Color;
        public string Color {
            get => _Color;
            set {
                if (_Color == value) return;
                _Color = value;
                RaiseValidateAndChangedProperty(nameof(Color));
            }
        }

        private short _SafetyStockLevel;
        public short SafetyStockLevel {
            get => _SafetyStockLevel;
            set {
                if (_SafetyStockLevel == value) return;
                _SafetyStockLevel = value;
                RaiseValidateAndChangedProperty(nameof(SafetyStockLevel));
            }
        }

        private short _ReorderPoint;
        public short ReorderPoint {
            get => _ReorderPoint;
            set {
                if (_ReorderPoint == value) return;
                _ReorderPoint = value;
                RaiseValidateAndChangedProperty(nameof(ReorderPoint));
            }
        }

        private decimal _StandardCost;
        public decimal StandardCost {
            get => _StandardCost;
            set {
                if (_StandardCost == value) return;
                _StandardCost = value;
                RaiseValidateAndChangedProperty(nameof(StandardCost));
            }
        }

        private decimal _ListPrice;
        public decimal ListPrice {
            get => _ListPrice;
            set {
                if (_ListPrice == value) return;
                _ListPrice = value;
                RaiseValidateAndChangedProperty(nameof(ListPrice));
            }
        }

        private string _Size;
        public string Size {
            get => _Size;
            set {
                if (_Size == value) return;
                _Size = value;
                RaiseValidateAndChangedProperty(nameof(Size));
            }
        }

        private string _SizeUnitMeasureCode;
        public string SizeUnitMeasureCode {
            get => _SizeUnitMeasureCode;
            set {
                if (_SizeUnitMeasureCode == value) return;
                _SizeUnitMeasureCode = value;
                RaiseValidateAndChangedProperty(nameof(SizeUnitMeasureCode));
            }
        }

        private string _WeightUnitMeasureCode;
        public string WeightUnitMeasureCode {
            get => _WeightUnitMeasureCode;
            set {
                if (_WeightUnitMeasureCode == value) return;
                _WeightUnitMeasureCode = value;
                RaiseValidateAndChangedProperty(nameof(WeightUnitMeasureCode));
            }
        }

        private Nullable<decimal> _Weight;
        public Nullable<decimal> Weight {
            get => _Weight;
            set {
                if (_Weight == value) return;
                _Weight = value;
                RaiseValidateAndChangedProperty(nameof(Weight));
            }
        }

        private int _DaysToManufacture;
        public int DaysToManufacture {
            get => _DaysToManufacture;
            set {
                if (_DaysToManufacture == value) return;
                _DaysToManufacture = value;
                RaiseValidateAndChangedProperty(nameof(DaysToManufacture));
            }
        }

        private string _ProductLine;
        public string ProductLine {
            get => _ProductLine;
            set {
                if (_ProductLine == value) return;
                _ProductLine = value;
                RaiseValidateAndChangedProperty(nameof(ProductLine));
            }
        }

        private string _Class;
        public string Class {
            get => _Class;
            set {
                if (_Class == value) return;
                _Class = value;
                RaiseValidateAndChangedProperty(nameof(Class));
            }
        }

        private string _Style;
        public string Style {
            get => _Style;
            set {
                if (_Style == value) return;
                _Style = value;
                RaiseValidateAndChangedProperty(nameof(Style));
            }
        }

        private Nullable<int> _ProductSubcategoryID;
        public Nullable<int> ProductSubcategoryID {
            get => _ProductSubcategoryID;
            set {
                if (_ProductSubcategoryID == value) return;
                _ProductSubcategoryID = value;
                if(Subcategoria != null && _ProductSubcategoryID != Subcategoria.ProductSubcategoryID) {
                    _Subcategoria = null;
                    RaisePropertyChanged(nameof(Subcategoria));
                }
                RaiseValidateAndChangedProperty(nameof(ProductSubcategoryID));
            }
        }

        private Nullable<int> _ProductModelID;
        public Nullable<int> ProductModelID {
            get => _ProductModelID;
            set {
                if (_ProductModelID == value) return;
                _ProductModelID = value;
                RaiseValidateAndChangedProperty(nameof(ProductModelID));
            }
        }

        private System.DateTime _SellStartDate;
        public System.DateTime SellStartDate {
            get => _SellStartDate;
            set {
                if (_SellStartDate == value) return;
                _SellStartDate = value;
                RaiseValidateAndChangedProperty(nameof(SellStartDate));
            }
        }

        private Nullable<System.DateTime> _SellEndDate;
        public Nullable<System.DateTime> SellEndDate {
            get => _SellEndDate;
            set {
                if (_SellEndDate == value) return;
                _SellEndDate = value;
                RaiseValidateAndChangedProperty(nameof(SellEndDate));
            }
        }

        private Nullable<System.DateTime> _DiscontinuedDate;
        public Nullable<System.DateTime> DiscontinuedDate {
            get => _DiscontinuedDate;
            set {
                if (_DiscontinuedDate == value) return;
                _DiscontinuedDate = value;
                RaiseValidateAndChangedProperty(nameof(DiscontinuedDate));
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

        private Subcategoria _Subcategoria;
        public virtual Subcategoria Subcategoria {
            get => _Subcategoria;
            set {
                if (_Subcategoria == value) return;
                _Subcategoria = value;
                if (ProductSubcategoryID != _Subcategoria?.ProductSubcategoryID)
                    ProductSubcategoryID = _Subcategoria?.ProductSubcategoryID;
                RaiseValidateAndChangedProperty(nameof(Subcategoria));
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
