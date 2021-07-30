using Demos.Domains.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demos.Domains.Entidades {
    public class Origen: Entidad {
        private String idOrigen;
        private String nombre;
        private ObservableCollection<Trabajo> trabajos;

        public Origen() {
            Trabajos = new ObservableCollection<Trabajo>();
        }

        public Origen(string idOrigen): this() {
            this.idOrigen = idOrigen;
        }

        public Origen(string idOrigen, string nombre): this(idOrigen) {
            this.nombre = nombre;
        }

        public string IdOrigen {
            get => idOrigen;
            set {
                if (idOrigen == value) return;
                idOrigen = value;
                RaisePropertyChanged(nameof(IdOrigen));
            }
        }
        public string Nombre {
            get => nombre;
            set {
                if (nombre == value) return;
                nombre = value;
                RaisePropertyChanged(nameof(Nombre));
            }
        }
        public ObservableCollection<Trabajo> Trabajos {
            get => trabajos;
            set {
                if (trabajos == value || value == null) return;
                trabajos = value;
                trabajos.CollectionChanged += (s, ev) => {
                    if (ev.NewItems != null)
                        foreach (Trabajo item in ev.NewItems) {
                            if (item.Origen != this)
                                item.Origen = this;
                            item.PropertyChanged += Trabajo_PropertyChanged;
                        }
                    if (ev.OldItems != null)
                        foreach (Trabajo item in ev.OldItems) {
                            if (item.Origen == this)
                                item.Origen = null;
                            item.PropertyChanged -= Trabajo_PropertyChanged;
                        }
                    RaisePropertyChanged(nameof(Count));
                    RaisePropertyChanged(nameof(Total));
                };
                RaisePropertyChanged(nameof(Trabajos));
            }
        }

        private void Trabajo_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
            if(e.PropertyName == "Peso")
                RaisePropertyChanged(nameof(Total));;
        }

        public int Count => trabajos.Count;
        public double Total => trabajos.Sum(item => item.Peso);

        public override bool Equals(object obj) {
            return obj is Origen source &&
                   idOrigen == source.idOrigen;
        }

        protected override List<string> ValidateProperty(string propertyName) {
            List<string> lst = new List<string>();
            switch (propertyName) {
                case nameof(IdOrigen):
                    if (String.IsNullOrWhiteSpace(IdOrigen))
                        lst.Add("Es obligatorio");
                    else {
                        if (IdOrigen.Length > 2)
                            lst.Add("Debe tener 2 letras como máximo");
                        if (IdOrigen != IdOrigen.ToUpper())
                            lst.Add("Debe estar en mayúsculas");
                    }
                    break;
                case nameof(Nombre):
                    if (String.IsNullOrWhiteSpace(Nombre))
                        lst.Add("Es obligatorio");
                    else {
                        if (Nombre.Length > 20)
                            lst.Add("Debe tener 20 letras como máximo");
                    }
                    break;
            }
            return lst;
        }
    }
}
