using Demos.Domains.Entidades;
using System.Collections.Generic;

namespace Demos.Domains.Contracts.Repositories {
    public interface ITrabajoRepository {
        List<Trabajo> Cargar();
    }
}