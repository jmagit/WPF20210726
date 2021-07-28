﻿using Demos.Domains.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demos.Domains.Entidades {
    public class Origen: ObservableBase {
        private String idOrigen;
        private String nombre;
        private ObservableCollection<Trabajo> trabajos;

        public Origen() {
            Trabajos = new ObservableCollection<Trabajo>();
        }

        public Origen(string idOrigen): this() {
            this.idOrigen = idOrigen;
        }

        public Origen(string idOrigen, string nombre): this() {
            this.idOrigen = idOrigen;
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
                if (trabajos == value) return;
                trabajos = value;
                trabajos.CollectionChanged += (s, ev) => {
                    if (ev.NewItems != null)
                        foreach (Trabajo item in ev.NewItems)
                            if(item.Origen != this)
                                item.Origen = this;
                    if (ev.OldItems != null)
                        foreach (Trabajo item in ev.OldItems)
                            if (item.Origen == this)
                                item.Origen = null;
                };
                RaisePropertyChanged(nameof(Trabajos));
            }
        }
        public override bool Equals(object obj) {
            return obj is Origen source &&
                   idOrigen == source.idOrigen;
        }
    }
}
