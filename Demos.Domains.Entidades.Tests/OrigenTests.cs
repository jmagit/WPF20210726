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
            var origen = new Origen("PR", "Paris");
            var otro = new Origen("PR", "Paris");
            Assert.IsTrue(origen.Equals(otro));
            Assert.IsTrue(origen.Equals(new Origen("PR")));
            Assert.IsFalse(origen.Equals(new Origen("FP")));
        }
        [TestMethod()]
        public void AddTrabajosTest() {
            var origen = new Origen("PR", "Paris");
            var t = new Trabajo("1", 2, DateTime.Now);
            origen.Trabajos.Add(t);
            origen.Trabajos.Add(new Trabajo("2", 22, DateTime.Now));
            Assert.IsNotNull(origen.Trabajos);
            Assert.AreEqual(2, origen.Trabajos.Count);
            Assert.AreEqual(origen.IdOrigen, origen.Trabajos[1].IdOrigen);
        }
        [TestMethod()]
        public void DeleteTrabajosTest() {
            var origen = new Origen("PR", "Paris");
            var t = new Trabajo("1", 2, DateTime.Now);
            origen.Trabajos.Add(t);
            Assert.IsNotNull(origen.Trabajos);
            Assert.AreEqual(1, origen.Trabajos.Count);
            origen.Trabajos.Remove(t);
            Assert.IsNull(t.IdOrigen);
        }
        [TestMethod()]
        public void AddOrigen2TrabajosTest() {
            var origen = new Origen("PR", "Paris");
            var t = new Trabajo("1", 2, DateTime.Now);
            t.Origen = origen;
            Assert.AreEqual(1, origen.Trabajos.Count);
            Assert.AreEqual(t, origen.Trabajos[0]);
            Assert.AreEqual(origen, t.Origen);
        }
        [TestMethod()]
        public void RemoveOrigen2TrabajosTest() {
            var origen = new Origen("PR", "Paris");
            var t = new Trabajo("1", 2, DateTime.Now);
            t.Origen = origen;
            Assert.AreEqual(1, origen.Trabajos.Count);
            Assert.AreEqual(t, origen.Trabajos[0]);
            t.Origen = null;
            Assert.AreEqual(0, origen.Trabajos.Count);
            Assert.IsNull(t.Origen);
        }
        [TestMethod()]
        public void MoveOrigen2TrabajosTest() {
            var origen = new Origen("PR", "Paris");
            var destino = new Origen("RM", "Roma");
            var t = new Trabajo("1", 2, DateTime.Now);
            t.Origen = origen;
            Assert.AreEqual(1, origen.Trabajos.Count);
            Assert.AreEqual(t, origen.Trabajos[0]);
            t.Origen = destino;
            Assert.AreEqual(0, origen.Trabajos.Count);
            Assert.AreEqual(1, destino.Trabajos.Count);
            Assert.AreEqual(t, destino.Trabajos[0]);
            Assert.AreEqual(destino, t.Origen);
        }
        [TestMethod()]
        public void CalculaTotalTest() {
            var origen = new Origen("PR", "Paris");
            var t = new Trabajo("1", 2, DateTime.Now);
            origen.Trabajos.Add(t);
            origen.Trabajos.Add(new Trabajo("2", 22, DateTime.Now));
            Assert.AreEqual(24, origen.Total);
        }
    }
}