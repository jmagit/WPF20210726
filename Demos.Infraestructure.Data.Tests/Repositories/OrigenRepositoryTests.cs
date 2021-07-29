using Microsoft.VisualStudio.TestTools.UnitTesting;
using Demos.Infraestructure.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demos.Infraestructure.Data.Repositories.Tests {
    [TestClass()]
    public class OrigenRepositoryTests {
        [TestMethod()]
        public void CargarTest() {
            var dao = new OrigenRepository();
            var rslt = dao.Cargar(@"D:\Cursos\dotnet\WPF20210726\Curso.xlsx");

            Assert.AreEqual(4, rslt.Count);
            Assert.AreEqual(5, rslt[3].Trabajos.Count);
            Assert.AreEqual("NY4", rslt[0].Trabajos[3].Identificador);
       }
    }
}