using Backend;
using System;

namespace Tests
{
    [TestClass]
    public class testPromocion
    {
        [TestMethod]
        public void TestCrearPromocionSinParametros()
        {
            DateTime desde = new DateTime(2024, 02, 03);
            DateTime hasta = new DateTime(2024, 04, 04);

            Promocion promo = new Promocion();

            Assert.AreEqual("", promo.Etiqueta);
            Assert.AreEqual(0.10m, promo.Descuento);
            Assert.AreEqual(desde, promo.Desde);
            Assert.AreEqual(hasta, promo.Hasta);
            Assert.AreEqual(9999, promo.ID);
        }

        [TestMethod]
        public void TestCrearPromocionConParametros()
        {
            DateTime desde = new DateTime(2024, 11, 11);
            DateTime hasta = new DateTime(2024, 12, 12);

            Promocion promo = new Promocion("Promo verano", 0.45m, desde, hasta);

            Assert.AreEqual("Promo verano", promo.Etiqueta);
            Assert.AreEqual(0.45m, promo.Descuento);
            Assert.AreEqual(desde, promo.Desde);
            Assert.AreEqual(hasta, promo.Hasta);
            //Assert.AreEqual(Promocion.ContadorDeID, promo.ID);
        }

        [TestMethod]
        public void TestCrearPromocionConParametrosYEtiquetaVacia()
        {
            DateTime desde = new DateTime(2024, 11, 11);
            DateTime hasta = new DateTime(2024, 12, 12);

            Promocion promo = new Promocion("", 0.15m, desde, hasta);

            Assert.AreEqual("", promo.Etiqueta);
            Assert.AreEqual(0.15m, promo.Descuento);
            Assert.AreEqual(desde, promo.Desde);
            Assert.AreEqual(hasta, promo.Hasta);
            //Assert.AreEqual(Promocion.ContadorDeID, promo.ID);
        }

        [TestMethod]
        public void TestCrearPromocionConParametrosYEtiqueta20CharExactos()
        {
            DateTime desde = new DateTime(2024, 11, 11);
            DateTime hasta = new DateTime(2024, 12, 12);

            Promocion promo = new Promocion("12345678912345678912", 0.15m, desde, hasta);

            Assert.AreEqual("12345678912345678912", promo.Etiqueta);
            Assert.AreEqual(0.15m, promo.Descuento);
            Assert.AreEqual(desde, promo.Desde);
            Assert.AreEqual(hasta, promo.Hasta);
            //Assert.AreEqual(Promocion.ContadorDeID, promo.ID);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCrearPromocionConParametrosYEtiqueta21CharExactos()
        {
            DateTime desde = new DateTime(2024, 11, 11);
            DateTime hasta = new DateTime(2024, 12, 12);

            Promocion promo = new Promocion("123456789123456789120", 0.15m, desde, hasta);
        }

        [TestMethod]
        public void TestFechasValidas()
        {
            DateTime desde = new DateTime(2024, 11, 03);
            DateTime hasta = new DateTime(2024, 12, 03);

            Promocion promo = new Promocion("", 0.10m, desde, hasta);

            Assert.AreEqual("", promo.Etiqueta);
            Assert.AreEqual(0.10m, promo.Descuento);
            Assert.AreEqual(desde, promo.Desde);
            Assert.AreEqual(hasta, promo.Hasta);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestFechasNoValidasPorSerAntesDeHoy()
        {
            DateTime desde = new DateTime(1998, 11, 16);
            DateTime hasta = new DateTime(2025, 11, 16);

            Promocion promo = new Promocion("", 0.10m, desde, hasta);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestFechasNoValidasPorSerMismoDia()
        {
            DateTime desde = new DateTime(2025, 11, 16);

            Promocion promo = new Promocion("", 0.16m, desde, desde);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestFechaInicioNoValidaPorSerHoy()
        {
            DateTime desde = DateTime.Today;
            DateTime hasta = new DateTime(2025, 11, 16);

            Promocion promo = new Promocion("", 0.22m, desde, hasta);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestFechaFinNoValidaPorSerHoy()
        {
            DateTime desde = DateTime.Today.AddDays(1);
            DateTime hasta = DateTime.Today;

            Promocion promo = new Promocion("", 0.10m, desde, hasta);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestFechaFinNoValidaPorSerAmbasPasadas()
        {
            DateTime desde = DateTime.Today.AddDays(-120);
            DateTime hasta = DateTime.Today.AddDays(-100);

            Promocion promo = new Promocion("", 0.35m, desde, hasta);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestEtiquetaNoValidaPorExtension()
        {
            DateTime desde = new DateTime(2024, 09, 03);
            DateTime hasta = new DateTime(2025, 11, 16);

            Promocion promo = new Promocion("1234564355544534554345354453" +
                "7891011121321232grfdsggfdfgdsfdgh34", 0.10m, desde, hasta);
        }

        [TestMethod]
        public void TestEtiquetaValida()
        {
            DateTime desde = new DateTime(2024, 09, 03);
            DateTime hasta = new DateTime(2025, 11, 16);

            Promocion promo = new Promocion("1234 34", 0.10m, desde, hasta);

            Assert.AreEqual("1234 34", promo.Etiqueta);
            Assert.AreEqual(0.10m, promo.Descuento);
            Assert.AreEqual(desde, promo.Desde);
            Assert.AreEqual(hasta, promo.Hasta);
        }

        [TestMethod]
        public void TestDescuentoValido1()
        {
            DateTime desde = new DateTime(2024, 09, 03);
            DateTime hasta = new DateTime(2025, 11, 16);

            Promocion promo = new Promocion("1234 34", 0.10m, desde, hasta);

            Assert.AreEqual("1234 34", promo.Etiqueta);
            Assert.AreEqual(0.10m, promo.Descuento);
            Assert.AreEqual(desde, promo.Desde);
            Assert.AreEqual(hasta, promo.Hasta);
        }

        [TestMethod]
        public void TestDescuentoValido2()
        {
            DateTime desde = new DateTime(2024, 09, 03);
            DateTime hasta = new DateTime(2025, 11, 16);

            Promocion promo = new Promocion("1234 34", 0.65m, desde, hasta);

            Assert.AreEqual("1234 34", promo.Etiqueta);
            Assert.AreEqual(0.65m, promo.Descuento);
            Assert.AreEqual(desde, promo.Desde);
            Assert.AreEqual(hasta, promo.Hasta);
        }

        [TestMethod]
        public void TestDescuentoValidoLimite1()
        {
            DateTime desde = new DateTime(2024, 09, 03);
            DateTime hasta = new DateTime(2025, 11, 16);

            Promocion promo = new Promocion("1234 34", 0.05m, desde, hasta);

            Assert.AreEqual("1234 34", promo.Etiqueta);
            Assert.AreEqual(0.05m, promo.Descuento);
            Assert.AreEqual(desde, promo.Desde);
            Assert.AreEqual(hasta, promo.Hasta);
        }

        [TestMethod]
        public void TestDescuentoValidoLimite2()
        {
            DateTime desde = new DateTime(2024, 09, 03);
            DateTime hasta = new DateTime(2025, 11, 16);

            Promocion promo = new Promocion("1234 34", 0.75m, desde, hasta);

            Assert.AreEqual("1234 34", promo.Etiqueta);
            Assert.AreEqual(0.75m, promo.Descuento);
            Assert.AreEqual(desde, promo.Desde);
            Assert.AreEqual(hasta, promo.Hasta);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestDescuentoNoValidoLimite1()
        {
            DateTime desde = new DateTime(2024, 09, 03);
            DateTime hasta = new DateTime(2025, 11, 16);

            Promocion promo = new Promocion("1234 34", 0.04m, desde, hasta);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestDescuentoNoValidoLimite2()
        {
            DateTime desde = new DateTime(2024, 09, 03);
            DateTime hasta = new DateTime(2025, 11, 16);

            Promocion promo = new Promocion("1234 34", 0.76m, desde, hasta);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestDescuentoNoValido1()
        {
            DateTime desde = new DateTime(2024, 09, 03);
            DateTime hasta = new DateTime(2025, 11, 16);

            Promocion promo = new Promocion("1234 34", -175, desde, hasta);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestDescuentoNoValido2()
        {
            DateTime desde = new DateTime(2024, 09, 03);
            DateTime hasta = new DateTime(2025, 11, 16);

            Promocion promo = new Promocion("1234 34", 1275, desde, hasta);
        }

    }

}
