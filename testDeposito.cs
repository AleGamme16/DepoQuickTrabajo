using Backend;
using System;

namespace Tests
{
    [TestClass]
    public class testDeposito
    {
        [TestMethod]
        public void TestCrearDepositoSinParametros()
        {
            Promocion promo = new Promocion();
            Deposito depo = new Deposito();

            Assert.AreEqual(EnumArea.A, depo.Area);
            Assert.AreEqual(EnumTamano.Pequeno, depo.Tamano);
            Assert.AreEqual(false, depo.Climatizado);
            Assert.AreEqual(promo, depo.Promo);
            Assert.AreEqual(9999, depo.ID);
        }

        [TestMethod]
        public void TestCrearDepositoConParametros()
        {
            Promocion promo = new Promocion("", 0.25m, DateTime.Today.AddDays(1), DateTime.Today.AddDays(5));
            Deposito depo = new Deposito(EnumArea.B, EnumTamano.Mediano, true, promo);

            Assert.AreEqual(EnumArea.B, depo.Area);
            Assert.AreEqual(EnumTamano.Mediano, depo.Tamano);
            Assert.AreEqual(true, depo.Climatizado);
            Assert.AreEqual(promo, depo.Promo);
            Assert.AreEqual(Deposito.ContadorDeID, depo.ID);
        }

        [TestMethod]
        public void TestCrearDepositoConEnums1()
        {
            Promocion promo = new Promocion("", 0.25m, DateTime.Today.AddDays(1), DateTime.Today.AddDays(5));
            Deposito depo = new Deposito(EnumArea.B, EnumTamano.Mediano, true, promo);

            Assert.AreEqual(depo.Area, EnumArea.B);
            Assert.AreEqual(depo.Tamano, EnumTamano.Mediano);
            Assert.AreEqual(depo.Climatizado, true);
            Assert.AreEqual(depo.Promo, promo);
            Assert.AreEqual(Deposito.ContadorDeID, depo.ID);
        }

        [TestMethod]
        public void TestCrearDepositoConEnums2()
        {
            Promocion promo = new Promocion("Hola", 0.25m, DateTime.Today.AddDays(2), DateTime.Now.AddDays(4));
            Deposito depo = new Deposito(EnumArea.A, EnumTamano.Grande, false, promo);

            Assert.AreEqual(depo.Area, EnumArea.A);
            Assert.AreEqual(depo.Tamano, EnumTamano.Grande);
            Assert.AreEqual(depo.Climatizado, false);
            Assert.AreEqual(depo.Promo, promo);
            Assert.AreEqual(Deposito.ContadorDeID, depo.ID);
        }

    }
}
