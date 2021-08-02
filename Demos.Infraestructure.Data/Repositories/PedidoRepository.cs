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
    public class PedidoRepository : IPedidoRepository {
        public ObservableCollection<Pedido> Cargar(string filePath) {
            var rslt = new ObservableCollection<Pedido>();
            Cargar(filePath, rslt);
            return rslt;
        }
        public void Cargar(string filePath, ObservableCollection<Pedido> destino) {
            Cargar(filePath, destino, (invoke) => invoke());
        }
        public void Cargar(string filePath, ObservableCollection<Pedido> destino, Action<Action> invoke) {
            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read)) {
                using (var reader = ExcelReaderFactory.CreateReader(stream)) {
                    var hoja = 1;
                    do {
                        reader.Read();
                        Pedido cachePedido = null;
                        while (reader.Read()) {
                            switch (hoja) {
                                case 1:
                                    var pedido = new Pedido() {
                                        SalesOrderID = (int)reader.GetDouble(0),
                                        RevisionNumber = (byte)reader.GetDouble(1),
                                        OrderDate = reader.GetDateTime(2),
                                        DueDate = reader.GetDateTime(3),
                                        ShipDate = reader.GetDateTime(4),
                                        Status = (byte)reader.GetDouble(5),
                                        OnlineOrderFlag = reader.GetBoolean(6),
                                        SalesOrderNumber = reader.GetString(7),
                                        PurchaseOrderNumber = reader.GetString(8),
                                        AccountNumber = reader.GetString(9),
                                        CustomerID = (int)reader.GetDouble(10),
                                        SalesPersonID = reader.IsDBNull(11) ? null : (int?)reader.GetDouble(11),
                                        TerritoryID = reader.IsDBNull(12) ? null : (int?)reader.GetDouble(12),
                                        BillToAddressID = (int)reader.GetDouble(13),
                                        ShipToAddressID = (int)reader.GetDouble(14),
                                        ShipMethodID = (int)reader.GetDouble(15),
                                        CreditCardID = reader.IsDBNull(16) ? null : (int?)reader.GetDouble(16),
                                        CreditCardApprovalCode = reader.GetString(17),
                                        CurrencyRateID = reader.IsDBNull(18) ? null : (int?)reader.GetDouble(18),
                                        SubTotal = (Decimal)reader.GetDouble(19),
                                        TaxAmt = (Decimal)reader.GetDouble(20),
                                        Freight = (Decimal)reader.GetDouble(21),
                                        TotalDue = (Decimal)reader.GetDouble(22),
                                        Comment = reader.GetString(23),
                                        rowguid = new Guid(reader.GetString(24)),
                                        ModifiedDate = reader.GetDateTime(25),
                                    };
                                    invoke(() => destino.Add(pedido));
                                    break;
                                case 2:
                                    if (cachePedido == null || cachePedido.SalesOrderID != (int)reader.GetDouble(0)) {
                                        cachePedido = destino.FirstOrDefault(o => o.SalesOrderID == (int)reader.GetDouble(0));
                                    }
                                    if (cachePedido != null) {
                                        var linea = new Linea() {
                                            SalesOrderID = (int)reader.GetDouble(0),
                                            SalesOrderDetailID = (int)reader.GetDouble(1),
                                            CarrierTrackingNumber = reader.GetString(2),
                                            OrderQty = (short)reader.GetDouble(3),
                                            ProductID = (int)reader.GetDouble(4),
                                            SpecialOfferID = (int)reader.GetDouble(5),
                                            UnitPrice = (decimal)reader.GetDouble(6),
                                            UnitPriceDiscount = (decimal)reader.GetDouble(7),
                                            rowguid = new Guid(reader.GetString(8)),
                                            ModifiedDate = reader.GetDateTime(9)
                                        };
                                        invoke(() => cachePedido.Lineas.Add(linea));
                                    }
                                    break;
                            }
                        }
                        hoja++;
                    } while (reader.NextResult());
                }
            }
        }

        public void Cargar1(string filePath, ObservableCollection<Pedido> destino, Action<Action> invoke) {
            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read)) {
                using (var reader = ExcelReaderFactory.CreateReader(stream)) {
                    var result = reader.AsDataSet(new ExcelDataSetConfiguration() {
                        UseColumnDataType = true,
                        ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration() {
                            UseHeaderRow = true,
                        }
                    });
                    foreach (DataRow item in result.Tables[0].Rows) {
                        invoke(() => {
                            destino.Add(
                                new Pedido() {
                                    SalesOrderID = (int)item.Field<double>(0),
                                    RevisionNumber = (byte)item.Field<double>(1),
                                    OrderDate = item.Field<DateTime>(2),
                                    DueDate = item.Field<DateTime>(3),
                                    ShipDate = item.Field<DateTime>(4),
                                    Status = (byte)item.Field<double>(5),
                                    OnlineOrderFlag = item.Field<bool>(6),
                                    SalesOrderNumber = item.Field<string>(7),
                                    PurchaseOrderNumber = item.Field<string>(8),
                                    AccountNumber = item.Field<string>(9),
                                    CustomerID = (int)item.Field<double>(10),
                                    SalesPersonID = item.IsNull(11) ? null : (int?)item.Field<double>(11),
                                    TerritoryID = item.IsNull(12) ? null : (int?)item.Field<double>(12),
                                    BillToAddressID = (int)item.Field<double>(13),
                                    ShipToAddressID = (int)item.Field<double>(14),
                                    ShipMethodID = (int)item.Field<double>(15),
                                    CreditCardID = item.IsNull(16) ? null : (int?)item.Field<double>(16),
                                    CreditCardApprovalCode = item.Field<string>(17),
                                    CurrencyRateID = item.IsNull(18) ? null : (int?)item.Field<double>(18),
                                    SubTotal = (Decimal)item.Field<double>(19),
                                    TaxAmt = (Decimal)item.Field<double>(20),
                                    Freight = (Decimal)item.Field<double>(21),
                                    TotalDue = (Decimal)item.Field<double>(22),
                                    Comment = item.Field<string>(23),
                                    rowguid = new Guid(item.Field<string>(24)),
                                    ModifiedDate = item.Field<DateTime>(25),
                                });
                        });

                    }
                    Pedido cachePedido = null;
                    foreach (DataRow item in result.Tables[1].Rows) {
                        if (cachePedido == null || cachePedido.SalesOrderID != (int)item.Field<double>(0)) {
                            cachePedido = destino.FirstOrDefault(o => o.SalesOrderID == (int)item.Field<double>(0));
                        }
                        if (cachePedido != null)
                            cachePedido
                            //destino.FirstOrDefault(o => o.SalesOrderID == (int)reader.GetDouble(0))
                            .Lineas.Add(new Linea() {
                                SalesOrderID = (int)item.Field<double>(0),
                                SalesOrderDetailID = (int)item.Field<double>(1),
                                CarrierTrackingNumber = item.Field<string>(2),
                                OrderQty = (short)item.Field<double>(3),
                                ProductID = (int)item.Field<double>(4),
                                SpecialOfferID = (int)item.Field<double>(5),
                                UnitPrice = (decimal)item.Field<double>(6),
                                UnitPriceDiscount = (decimal)item.Field<double>(7),
                                rowguid = new Guid(item.Field<string>(8)),
                                ModifiedDate = item.Field<DateTime>(9)
                            });
                    }
                }
            }
        }

        public Task<ObservableCollection<Pedido>> CargarAsync(string filePath) {
            return Task<ObservableCollection<Pedido>>.Run(() => Cargar(filePath));
        }
        public Task CargarAsync(string filePath, ObservableCollection<Pedido> destino, Action<Action> invoke) {
            return Task.Run(() => Cargar(filePath, destino, invoke));
        }
    }
}
