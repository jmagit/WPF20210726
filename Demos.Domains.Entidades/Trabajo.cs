using Demos.Domains.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demos.Domains.Entidades {
    public class Trabajo: ObservableBase {
        private String origen;
        private String identificador;
        private Double peso;
        private DateTime fechaCarga;

        public Trabajo() {
        }

        public Trabajo(string origen, string identificador, double peso, DateTime fechaCarga) {
            this.origen = origen;
            this.identificador = identificador;
            this.peso = peso;
            this.fechaCarga = fechaCarga;
        }

        public string Origen {
            get => origen;
            set {
                if (origen == value) return;
                origen = value;
                RaisePropertyChanged(nameof(Origen));
            }
        }
        public string Identificador {
            get => identificador;
            set {
                if (identificador == value) return;
                identificador = value;
                RaisePropertyChanged(nameof(Identificador));
            }
        }
        public Double Peso {
            get => peso;
            set {
                if (peso == value) return;
                peso = value;
                RaisePropertyChanged(nameof(Peso));
                RaisePropertyChanged(nameof(SinPeso));
            }
        }
        public DateTime FechaCarga {
            get => fechaCarga;
            set {
                if (fechaCarga == value) return;
                fechaCarga = value;
                RaisePropertyChanged(nameof(FechaCarga));
            }
        }

        public bool SinPeso => peso == 0;

        public override bool Equals(object obj) {
            return obj is Trabajo trabajo &&
                   identificador == trabajo.identificador;
        }
    }
}
