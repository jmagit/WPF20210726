using Microsoft.VisualStudio.TestTools.UnitTesting;
using Demos.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Demos.ViewModel.Tests {
    [TestClass()]
    public class ProductosViewModelTests {
        ProductosViewModel vm;

         [TestMethod()]
        public async Task CargaTest() {
            // Arrange
            var vm = new ProductosViewModel();
            // Act
            vm.Carga();
            await Task.Delay(500);
            // Assert
            Assert.IsNotNull(vm.Categorias);
            Assert.AreEqual(4, vm.Categorias.Count);
            Assert.IsNotNull(vm.Subcategorias);
            Assert.AreEqual(37, vm.Subcategorias.Count);
            Assert.IsNotNull(vm.Productos);
            Assert.AreEqual(504, vm.Productos.Count);
        }

       [TestInitialize]
        public async Task Initialize() {
            vm = new ProductosViewModel();
            // Act
            vm.Carga();
            await Task.Delay(500);
        }

        [TestMethod()]
        public async Task Consulta1SinAgregadosTest() {
            vm.Consulta1.Execute(null);
            Assert.AreEqual(1, vm.ResultSet.Count);
        }

        [TestMethod()]
        public async Task Consulta1ConAgregadosTest() {
            vm.CompletaMuchos2Uno.Execute(null);
            vm.Consulta1.Execute(null);
            Assert.AreEqual(38, vm.ResultSet.Count);
        }
    }
}