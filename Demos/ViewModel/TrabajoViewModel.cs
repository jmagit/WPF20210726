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
    public class TrabajoViewModel: ObservableBase {
        private ITrabajoRepository dao = new TrabajoRepository();
        private ObservableCollection<Trabajo> listado;
        private Trabajo elemento;
        private DelegateCommand cargarCmd;
        private bool verDetalle = true;

        public TrabajoViewModel() {
            cargarCmd = new DelegateCommand((cmd) => Carga(), (cmd) => listado == null);
            var t = Task.Run(() => {
                while (true) {
                    if(elemento != null) {
                        elemento.Peso++;
                    }
                    Thread.Sleep(500);
                }
            });
            // t.Wait();
        }
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
            Listado = new ObservableCollection<Trabajo>(dao.Cargar(@"D:\Cursos\dotnet\WPF20210726\Curso.xlsx"));
            if(listado != null && listado.Count > 0) {
                Elemento = Listado[0];
            }
            cargarCmd.RaiseCanExecuteChanged();
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

        public event EventHandler<CancelEventArgs> Cerrar;

        protected void onCerrar(CancelEventArgs ev) {
            Cerrar?.Invoke(this, ev);
        }

        public DelegateCommand CerrarCmd {
            get {
                return new DelegateCommand(cmd => {
                    var ev = new CancelEventArgs();
                    ev.Cancel = false;
                    onCerrar(ev);
                    if(ev.Cancel) {
                        //...
                    }
                });
            }
        }

    }
}
