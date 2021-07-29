using Demos.Domains.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demos.Domains.Contracts.Repositories {
    public interface IOrigenRepository {
        List<Origen> Cargar(string filePath);
        Task<List<Origen>> CargarAsync(string filePath);
    }
}