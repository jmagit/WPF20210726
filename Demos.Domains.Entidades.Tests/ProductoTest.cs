using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Demos.Domains.Entidades.Tests {
    [TestClass]
    public class ProductoTest {
        [TestMethod]
        public void SubcategoriaTest() {
            bool raiseSubcategoria = false, raiseProductSubcategoryID = false;
            var p = new Producto() { ProductID = 1 };
            p.PropertyChanged += (s , e) => {
                if (e.PropertyName == "Subcategoria") raiseSubcategoria = true;
                if (e.PropertyName == "ProductSubcategoryID") raiseProductSubcategoryID = true;
            };
            Assert.IsNull(p.ProductSubcategoryID);
            Assert.IsNull(p.Subcategoria);

            p.Subcategoria = new Subcategoria() { ProductSubcategoryID = 66 };
            Assert.IsNotNull(p.Subcategoria);
            Assert.AreEqual(66, p.ProductSubcategoryID);
            Assert.IsTrue(raiseSubcategoria);
            Assert.IsTrue(raiseProductSubcategoryID);

            raiseSubcategoria = false; raiseProductSubcategoryID = false;
            p.Subcategoria = new Subcategoria() { ProductSubcategoryID = 55 };
            Assert.AreEqual(55, p.ProductSubcategoryID);
            Assert.IsTrue(raiseSubcategoria);
            Assert.IsTrue(raiseProductSubcategoryID);

            raiseSubcategoria = false; raiseProductSubcategoryID = false;
            p.Subcategoria = null;
            Assert.IsNull(p.ProductSubcategoryID);
            Assert.IsNull(p.Subcategoria);
            Assert.IsTrue(raiseSubcategoria);
            Assert.IsTrue(raiseProductSubcategoryID);
        }
        [TestMethod]
        public void ProductSubcategoryIDTest() {
            bool raiseSubcategoria = false, raiseProductSubcategoryID = false;
            var p = new Producto() { ProductID = 1 };
            p.PropertyChanged += (s, e) => {
                if (e.PropertyName == "Subcategoria") raiseSubcategoria = true;
                if (e.PropertyName == "ProductSubcategoryID") raiseProductSubcategoryID = true;
            };
            Assert.IsNull(p.ProductSubcategoryID);
            Assert.IsNull(p.Subcategoria);

            p.ProductSubcategoryID = 55;
            Assert.IsNull(p.Subcategoria);
            Assert.AreEqual(55, p.ProductSubcategoryID);
            Assert.IsFalse(raiseSubcategoria);
            Assert.IsTrue(raiseProductSubcategoryID);

            raiseSubcategoria = false; raiseProductSubcategoryID = false;
            p.Subcategoria = new Subcategoria() { ProductSubcategoryID = 66 };
            Assert.IsNotNull(p.Subcategoria);
            Assert.AreEqual(66, p.ProductSubcategoryID);
            Assert.IsTrue(raiseSubcategoria);
            Assert.IsTrue(raiseProductSubcategoryID);

            raiseSubcategoria = false; raiseProductSubcategoryID = false;
            p.ProductSubcategoryID = 2;
            Assert.IsNull(p.Subcategoria);
            Assert.AreEqual(2, p.ProductSubcategoryID);
            Assert.IsTrue(raiseSubcategoria);
            Assert.IsTrue(raiseProductSubcategoryID);

            p.Subcategoria = new Subcategoria() { ProductSubcategoryID = 55 };
            raiseSubcategoria = false; raiseProductSubcategoryID = false;
            p.ProductSubcategoryID = null;
            Assert.IsNull(p.ProductSubcategoryID);
            Assert.IsNull(p.Subcategoria);
            Assert.IsTrue(raiseSubcategoria);
            Assert.IsTrue(raiseProductSubcategoryID);
        }
    }
}
