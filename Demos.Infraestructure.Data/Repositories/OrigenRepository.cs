using Demos.Domains.Contracts.Repositories;
using Demos.Domains.Entidades;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Demos.Infraestructure.Data.Repositories {
    public class OrigenRepository : IOrigenRepository {

        public List<Origen> Cargar(string filePath) {
            //var filePath = @"D:\Cursos\dotnet\WPF20210726\Curso.xlsx";
            var lstOrigenes = new List<Origen>();
            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read)) {
                using (var reader = ExcelReaderFactory.CreateReader(stream)) {
                    var result = reader.AsDataSet(new ExcelDataSetConfiguration() {
                        UseColumnDataType = true,
                        ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration() {
                            UseHeaderRow = true,
                        }
                    });
                    lstOrigenes = result.Tables[1].AsEnumerable()
                        .Select(item => new Origen(item.Field<string>(0), item.Field<string>(1))).ToList();
                    Origen origen = null;
                    foreach (var item in result.Tables[0].AsEnumerable()
                        .Select(r => new {
                            idOrigen = r.Field<string>(0), identificador = r.Field<string>(1),
                            peso = r.Field<double>(2), fechaCarga = r.Field<DateTime>(3)
                        })
                        .OrderBy(r => r.idOrigen)
                        ) {
                        if (origen == null || origen.IdOrigen != item.idOrigen) {
                            origen = lstOrigenes.FirstOrDefault(o => o.IdOrigen == item.idOrigen);
                        }
                        if (origen != null) {
                            origen.Trabajos.Add(new Trabajo(item.identificador, item.peso, item.fechaCarga));
                        }
                    }
                }
            }
            return lstOrigenes;
        }
        public Task<List<Origen>> CargarAsync(string filePath) {
            return Task<List<Origen>>.Run(() => Cargar(filePath));
        }
    }
}
