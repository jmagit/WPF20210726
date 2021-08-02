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
using System.Collections.ObjectModel;

namespace Demos.Infraestructure.Data.Repositories {
    public class ProductoRepository : IProductoRepository {
        public ProductoSet Cargar(string filePath) {
            var rslt = new ProductoSet();
            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read)) {
                using (var reader = ExcelReaderFactory.CreateReader(stream)) {
                    var hoja = 1;
                    do {
                        reader.Read();
                        while (reader.Read()) {
                            switch (hoja) {
                                case 1:
                                    var categoria = new Categoria() {
                                        ProductCategoryID = (int)reader.GetDouble(0),
                                        Name = reader.GetString(1),
                                        rowguid = new Guid(reader.GetString(2)),
                                        ModifiedDate = reader.GetDateTime(3),
                                    };
                                    rslt.Categorias.Add(categoria);
                                    break;
                                case 2:
                                    var subcategoria = new Subcategoria() {
                                        ProductSubcategoryID = (int)reader.GetDouble(0),
                                        ProductCategoryID = (int)reader.GetDouble(1),
                                        Name = reader.GetString(2),
                                        rowguid = new Guid(reader.GetString(3)),
                                        ModifiedDate = reader.GetDateTime(4),
                                    };
                                    rslt.Subcategorias.Add(subcategoria);
                                    break;
                                case 3:
                                    var producto = new Producto() {
                                        ProductID = (int)reader.GetDouble(0),
                                        Name = reader.GetString(1),
                                        ProductNumber = reader.GetString(2),
                                        MakeFlag = reader.GetBoolean(3),
                                        FinishedGoodsFlag = reader.GetBoolean(4),
                                        Color = reader.GetString(5),
                                        SafetyStockLevel = (short)reader.GetDouble(6),
                                        ReorderPoint = (short)reader.GetDouble(7),
                                        StandardCost = (decimal)reader.GetDouble(8),
                                        ListPrice = (decimal)reader.GetDouble(9),
                                        Size = reader.GetString(10),
                                        SizeUnitMeasureCode = reader.GetString(11),
                                        WeightUnitMeasureCode = reader.GetString(12),
                                        Weight = reader.IsDBNull(13) ? null : (decimal?)reader.GetDouble(13),
                                        DaysToManufacture = (int)reader.GetDouble(14),
                                        ProductLine = reader.GetString(15),
                                        Class = reader.GetString(16),
                                        Style = reader.GetString(17),
                                        ProductSubcategoryID = reader.IsDBNull(18) ? null : (int?)reader.GetDouble(18),
                                        ProductModelID = reader.IsDBNull(19) ? null : (int?)reader.GetDouble(19),
                                        SellStartDate = reader.GetDateTime(20),
                                        SellEndDate = reader.IsDBNull(21) ? null : (DateTime?)reader.GetDateTime(21),
                                        DiscontinuedDate = reader.IsDBNull(22) ? null : (DateTime?)reader.GetDateTime(22),
                                        rowguid = new Guid(reader.GetString(23)),
                                        ModifiedDate = reader.GetDateTime(24),
                                    };
                                    rslt.Productos.Add(producto);
                                    break;
                            }
                        }
                        hoja++;
                    } while (reader.NextResult());
                }
            }
            return rslt;
        }

        public Task<ProductoSet> CargarAsyc(string filePath) {
            return Task<ProductoSet>.Run(() => Cargar(filePath));
        }
    }
}
