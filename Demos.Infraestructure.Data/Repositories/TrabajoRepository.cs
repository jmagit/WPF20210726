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
    public class TrabajoRepository : ITrabajoRepository {
        public List<Trabajo> CargarDS(string filePath) {
            //var filePath = @"D:\Cursos\dotnet\WPF20210726\Curso.xlsx";
            var lstTrabajos = new List<Trabajo>();
            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read)) {
                using (var reader = ExcelReaderFactory.CreateReader(stream)) {
                    var hoja = 1;
                    do {
                        reader.Read();
                        while (reader.Read()) {
                            switch (hoja) {
                                case 1:
                                    lstTrabajos.Add(
                                        new Trabajo(
                                            reader.GetString(0),
                                            reader.GetString(1),
                                            reader.GetDouble(2),
                                            reader.GetDateTime(3)
                                            )
                                        );
                                    break;
                            }
                        }
                        hoja++;
                    } while (reader.NextResult());
                }
            }
            return lstTrabajos;
        }

        public List<Trabajo> Cargar(string filePath) {
            //var filePath = @"D:\Cursos\dotnet\WPF20210726\Curso.xlsx";
            var lstTrabajos = new List<Trabajo>();
            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read)) {
                using (var reader = ExcelReaderFactory.CreateReader(stream)) {
                    var result = reader.AsDataSet(new ExcelDataSetConfiguration() {
                        UseColumnDataType = true,
                        ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration() {
                            UseHeaderRow = true,
                        }
                    });
                    lstTrabajos = result.Tables[0].AsEnumerable().Select(item => new Trabajo(item.ItemArray[0] as string, item.ItemArray[1] as string,
                            (double)item.ItemArray[2], (DateTime)item.ItemArray[3])).ToList();
                }
            }
            return lstTrabajos;
          }

    }
}
