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
using System.Windows.Input;
using System.Threading;

namespace Demos.ViewModel {
    public class OrigenViewModel : ObservableBase {
        private IOrigenRepository dao = new OrigenRepository();
        private ObservableCollection<Origen> listado;
        private Origen elemento;
        private DelegateCommand cargarCmd;
        private bool verDetalle = true;

        public OrigenViewModel() {
            cargarCmd = new DelegateCommand((cmd) => Carga(), (cmd) => listado == null);
        }
        public ObservableCollection<Origen> Listado {
            get => listado;
            protected set {
                if (listado == value) return;
                listado = value;
                RaisePropertyChanged(nameof(Listado));
            }
        }
        public Origen Elemento {
            get => elemento;
            set {
                if (elemento == value) return;
                elemento = value;
                RaisePropertyChanged(nameof(Elemento));
            }
        }

        public async void Carga() {
            Listado = new ObservableCollection<Origen>(await dao.CargarAsync(@"D:\Cursos\dotnet\WPF20210726\Curso.xlsx"));
            if(listado != null && listado.Count > 0) {
                Elemento = Listado[0];
            }
            cargarCmd.RaiseCanExecuteChanged();
            //Parallel.Invoke(
            //    () => {
            //        Listado = new ObservableCollection<Origen>(dao.Cargar(@"D:\Cursos\dotnet\WPF20210726\Curso.xlsx"));
            //    },
            //    () => {
            //        Elemento = null;
            //    }
            //    );
            //Parallel.ForEach(listado, (item) => {
            //    foreach (var t in item.Trabajos)
            //        Console.WriteLine(t.Identificador);
            //});
        }


        public DelegateCommand CargarCmd {
            get {
                return cargarCmd;
            }
        }
        public DelegateCommand LimpiaCmd {
            get {
                return new DelegateCommand(cmd => { 
                    Listado = null; 
                    if(cmd is DelegateCommand)
                        (cmd as DelegateCommand).RaiseCanExecuteChanged(); 
                });
            }
        }

        public bool VerDetalle { get => verDetalle; }
        public DelegateCommand VerCmd {
            get {
                return new DelegateCommand(cmd => {
                    verDetalle = !verDetalle;
                    RaisePropertyChanged(nameof(VerDetalle));
                });
            }
        }
    }
}
