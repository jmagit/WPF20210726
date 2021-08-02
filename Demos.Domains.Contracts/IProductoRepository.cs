using Demos.Domains.Entidades;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Demos.Domains.Contracts.Repositories {
    public interface IProductoRepository {
        ProductoSet Cargar(string filePath);
        Task<ProductoSet> CargarAsyc(string filePath);
    }
}