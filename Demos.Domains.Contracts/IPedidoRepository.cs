using Demos.Domains.Entidades;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Demos.Domains.Contracts.Repositories {
    public interface IPedidoRepository {
        ObservableCollection<Pedido> Cargar(string filePath);
        Task<ObservableCollection<Pedido>> CargarAsync(string filePath);
        void Cargar(string filePath, ObservableCollection<Pedido> destino);
        void Cargar(string filePath, ObservableCollection<Pedido> destino, Action<Action> invoke);
        Task CargarAsync(string filePath, ObservableCollection<Pedido> destino, Action<Action> invoke);
    }
}