using Microsoft.VisualStudio.TestTools.UnitTesting;
using Demos.Domains.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demos.Domains.Entidades.Tests {
    [TestClass()]
    public class OrigenTests {
        [TestMethod()]
        public void EqualsTest() {
            var ord = new Origen("PR", "Paris");
            var otro = new Origen("PR", "Paris");
            Assert.IsTrue(ord.Equals(otro));
            Assert.IsTrue(ord.Equals(new Origen("PR")));
            Assert.IsFalse(ord.Equals(new Origen("FP")));
        }
        [TestMethod()]
        public void AddTrabajosTest() {
            var ord = new Origen("PR", "Paris");
            var t = new Trabajo("1", 2, DateTime.Now);
            ord.Trabajos.Add(t);
            ord.Trabajos.Add(new Trabajo("2", 22, DateTime.Now));
            Assert.IsNotNull(ord.Trabajos);
            Assert.AreEqual(2, ord.Trabajos.Count);
            Assert.AreEqual(ord.IdOrigen, ord.Trabajos[1].IdOrigen);
        }
        [TestMethod()]
        public void DeleteTrabajosTest() {
            var ord = new Origen("PR", "Paris");
            var t = new Trabajo("1", 2, DateTime.Now);
            ord.Trabajos.Add(t);
            Assert.IsNotNull(ord.Trabajos);
            Assert.AreEqual(1, ord.Trabajos.Count);
            ord.Trabajos.Remove(t);
            Assert.IsNull(t.IdOrigen);
        }
    }
}