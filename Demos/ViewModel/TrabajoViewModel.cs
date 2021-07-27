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

namespace Demos.ViewModel {
    public class TrabajoViewModel: ObservableBase {
        private ITrabajoRepository dao = new TrabajoRepository();
        private ObservableCollection<Trabajo> listado;
        private Trabajo elemento;

        public ObservableCollection<Trabajo> Listado {
            get => listado;
            protected set {
                if (listado == value) return;
                listado = value;
                RaisePropertyChanged(nameof(Listado));
            }
        }
        public Trabajo Elemento {
            get => elemento;
            set {
                if (elemento == value) return;
                elemento = value;
                RaisePropertyChanged(nameof(Elemento));
            }
        }

        public void Carga() {
            Listado = new ObservableCollection<Trabajo>(dao.Cargar());
            if(listado != null && listado.Count > 0) {
                Elemento = Listado[0];
            }
        }
    }
}
