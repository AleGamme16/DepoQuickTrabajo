using Backend;
using Backend.Services;
using Backend.SQL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.EntityFrameworkCore;
using Backend.Context;

namespace Tests
{
    [TestClass]
    public class ReservaTests
    {
        private ServicioReserva _servicioReserva;
        private ServicioDeposito _servicioDeposito;
        private ServicioUsuario _servicioUsuario;
        private ServicioValoracion _servicioValoracion;
        private ServicioPromocion _servicioPromocion;
        private ServicioRegistroAccion _servicioRegistroAccion;
        private ServicioSessionLogic _servicioSessionLogic;
        private ServicioNotificacion _servicioNotificacion;
        private AppDataContext _context;
        private SqlRepositorioNotificacion repositorioNotificacion;

        [TestInitialize]
        public void Initialize()
        {
            var options = new DbContextOptionsBuilder<AppDataContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new AppDataContext(options);

            var sqlRepositorioReserva = new SqlRepositorioReserva(_context);
            var sqlRepositorioDeposito = new SqlRepositorioDeposito(_context);
            var sqlRepositorioUsuario = new SqlRepositorioUsuario(_context);
            var sqlRepositorioRegistroAccion = new SqlRepositorioRegistroAcciones(_context);
            var sqlRepositorioValoracion=new SqlRepositorioValoracion(_context, sqlRepositorioRegistroAccion);
            var sqlRepositorioPromocion=new SqlRepositorioPromocion(_context);
            var servicioNotificacion = new ServicioNotificacion(repositorioNotificacion);

            _servicioSessionLogic = new ServicioSessionLogic(sqlRepositorioUsuario, sqlRepositorioRegistroAccion);
            _servicioReserva = new ServicioReserva(sqlRepositorioReserva, sqlRepositorioDeposito, sqlRepositorioUsuario, sqlRepositorioValoracion, servicioNotificacion);
            _servicioDeposito = new ServicioDeposito(sqlRepositorioDeposito, sqlRepositorioPromocion, _servicioSessionLogic);
            _servicioUsuario = new ServicioUsuario(sqlRepositorioUsuario);
            _servicioValoracion= new ServicioValoracion(sqlRepositorioValoracion, sqlRepositorioUsuario, sqlRepositorioDeposito, sqlRepositorioRegistroAccion);
        }

        [TestCleanup]
        public void CleanUp()
        {
            _context.Database.EnsureDeleted();
        }

        [TestMethod]
        public void TestCrearReservaConDatosValidos()
        {
            var cliente = new Cliente("Lionel", "Messi", "Pa$$w0rd", "leoMessi@example.com");
            var deposito = new Deposito();
            var reserva = new Reserva(cliente, DateTime.Now, DateTime.Now.AddDays(7), 1000, EnumEstado.Pendiente, "Tarjeta sin fondos", deposito);

            _servicioReserva.AgregarReserva(reserva);

            Assert.IsNotNull(reserva);
            Assert.AreEqual(cliente, reserva.Cliente);
            Assert.AreEqual(DateTime.Now, reserva.FechaInicio);
            Assert.AreEqual(DateTime.Now.AddDays(7), reserva.FechaFin);
            Assert.AreEqual(1000, reserva.Costo);
            Assert.AreEqual(EnumEstado.Pendiente, reserva.Estado);
            Assert.AreEqual("Tarjeta sin fondos", reserva.MotivoRechazo);
            Assert.AreEqual(deposito, reserva.Deposito);
        }

        [TestMethod]
        public void TestCrearReservaConFechaInicioAnteriorAHoy()
        {
            var cliente = new Cliente("Cristiano", "Ronaldo", "Pa$$w0rd", "cr7@gmail.com");
            var deposito = new Deposito();

            Assert.ThrowsException<ArgumentException>(() =>
            {
                var reserva = new Reserva(cliente, DateTime.Today.AddDays(-5), DateTime.Now.AddDays(7), 2000, EnumEstado.Pendiente, "Porquesi", deposito);
                _servicioReserva.AgregarReserva(reserva);
            }, "Se esperaba que la creacion de reserva lanzara una ArgumentException.");
        }

        [TestMethod]
        public void TestRechazarReservaConEstadoPendiente()
        {
            var cliente = new Cliente("Edinson", "Cavani", "Pa$$w0Rdd", "edicavani@gmail.com");
            var deposito = new Deposito();
            var reserva = new Reserva(cliente, DateTime.Now.AddDays(1), DateTime.Now.AddDays(7),
                1500, EnumEstado.Pendiente, "", deposito);

            var motivoRechazo = "Se rechaza por fraude";
            _servicioReserva.RechazarSolicitud(reserva, motivoRechazo);

            Assert.AreEqual(EnumEstado.Rechazada, reserva.Estado);
            Assert.AreEqual(motivoRechazo, reserva.MotivoRechazo);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestRechazarReservaConEstadoAprobada()
        {
            var cliente = new Cliente("Luka", "Modric", "Pa$$w0Rdd", "luka10@gmail.com");
            var deposito = new Deposito();
            var reserva = new Reserva(cliente, DateTime.Now.AddDays(1), DateTime.Now.AddDays(7), 700, EnumEstado.Aprobada, "", deposito);

            _servicioReserva.AgregarReserva(reserva);
            _servicioReserva.RechazarSolicitud(reserva, reserva.MotivoRechazo);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestRechazarReservaConEstadoPendienteSinMotivoDeRechazo()
        {
            var cliente = new Cliente("Diego", "Maradona", "Pa$$w0Rdd", "diego10@gmail.com");
            var deposito = new Deposito();
            var reserva = new Reserva(cliente, DateTime.Now.AddDays(1), DateTime.Now.AddDays(7), 12500, EnumEstado.Pendiente, "", deposito);

            _servicioReserva.AgregarReserva(reserva);
            _servicioReserva.RechazarSolicitud(reserva, reserva.MotivoRechazo);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestRechazarReservaConMasDe300Caracteres()
        {
            var cliente = new Cliente("Diego", "Forlan", "Pa$$w0Rdd", "forlan@gmail.com");
            var deposito = new Deposito();
            var reserva = new Reserva(cliente, DateTime.Now.AddDays(1), DateTime.Now.AddDays(7), 12100, EnumEstado.Pendiente,
                "Este motivo de rechazo tendra mas de 300 caracteres: asdhajhcakjscnblksjadbclkjdsbclksajdb Este motivo de rechazo tendra" +
                " mas de 300 caracteres: asdhajhcakjscnblksjadbclkjdsbclksajdb  Este motivo de rechazo tendra mas de 300 caracteres: asdh" +
                "ajhcakjscnblksjadbclkjdsbclksajdb  kdmflkdamfa;skmd;lsamad cddfaldkjaoisdjaskdalks", deposito);

            _servicioReserva.AgregarReserva(reserva);
            _servicioReserva.RechazarSolicitud(reserva, reserva.MotivoRechazo);
        }

        [TestMethod]
        public void TestAprobarReservaPendiente()
        {
            var cliente = new Cliente("Thiago", "Alcantara", "Pa$$w0Rdd", "thiago6@gmail.com");
            var deposito = new Deposito();
            var reserva = new Reserva(cliente, DateTime.Now.AddDays(1), DateTime.Now.AddDays(7), 12500, EnumEstado.Pendiente, "", deposito);

            _servicioReserva.AgregarReserva(reserva);
            _servicioReserva.AprobarSolicitud(reserva);

            Assert.AreEqual(EnumEstado.Aprobada, reserva.Estado);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestAprobarReservaRechazada_DebeLanzarException()
        {
            var cliente = new Cliente("Thiago", "Alcantara", "Pa$$w0Rdd", "thiago6@gmail.com");
            var deposito = new Deposito();
            var reserva = new Reserva(cliente, DateTime.Now.AddDays(1), DateTime.Now.AddDays(7), 12500, EnumEstado.Rechazada, "", deposito);

            _servicioReserva.AgregarReserva(reserva);
            _servicioReserva.AprobarSolicitud(reserva);
        }

        //[TestMethod]
        //public void TestCalcularPrecioDepositoConDescuento()
        //{
        //    DateTime fechaInicio = new DateTime(2024, 5, 10);
        //    DateTime fechaFin = new DateTime(2024, 5, 17); // Reserva de 7 días

        //    Promocion promo = new Promocion(DateTime.Today.AddDays(-5), DateTime.Today.AddYears(2)); // Promo vigente para HOY por 25%
        //    Deposito deposito = new Deposito(EnumArea.A, EnumTamano.Pequeno, true, promo);
        //    Cliente cliente = new Cliente("nombre", "apellido", "Pa$$w0Rdd", "alegamme16@gmail.com");

        //    Reserva reserva = new Reserva(cliente, fechaInicio, fechaFin, 0, EnumEstado.Pendiente, "", deposito);
        //    reserva.Costo = _servicioReserva.CalcularPrecioDeposito(fechaInicio, fechaFin, deposito);

        //    Assert.AreEqual(343m, reserva.Costo); // El costo para 7 días con 5% de descuento y climatización debe ser (7 * 50 + 7 * 20) * (1 - (0.25 + 0.05)) = 343
        //}

        //[TestMethod]
        //public void TestCalcularPrecioDepositoConDescuentoYPromocionVigente()
        //{
        //    DateTime fechaInicio = new DateTime(2024, 6, 1);
        //    DateTime fechaFin = new DateTime(2024, 6, 10); // Reserva de 9 días

        //    Promocion promo = new Promocion(DateTime.Today.AddDays(-5), DateTime.Today.AddYears(2)); // 25% por defecto
        //    Deposito deposito = new Deposito(EnumArea.C, EnumTamano.Grande, false, promo);
        //    Cliente cliente = new Cliente("nombre", "apellido", "Pa$$w0Rdd", "alegamme16@gmail.com");

        //    Reserva reserva = new Reserva(cliente, fechaInicio, fechaFin, 0, EnumEstado.Pendiente, "", deposito);
        //    reserva.Costo = _servicioReserva.CalcularPrecioDeposito(fechaInicio, fechaFin, deposito);

        //    Assert.AreEqual(630, reserva.Costo); // Costo para 9 días con 25% de promo y : 9 * 100 * (1 - (0.25 + 0.05)) = 630
        //}

        //[TestMethod]
        //public void TestCalcularPrecioDepositoSinDescuentosYPromocionNOVigente()
        //{
        //    DateTime fechaInicio = new DateTime(2024, 8, 10);
        //    DateTime fechaFin = new DateTime(2024, 8, 12);  // 2 días

        //    //Promocion promo = new Promocion(DateTime.Today.AddYears(1), DateTime.Today.AddYears(2)); // 25% por defecto
        //    Deposito deposito = new Deposito(EnumArea.A, EnumTamano.Pequeno, false, promo);
        //    Cliente cliente = new Cliente("nombre", "apellido", "Pa$$w0Rdd", "alegamme16@gmail.com");

        //    Reserva reserva = new Reserva(cliente, fechaInicio, fechaFin, 0, EnumEstado.Pendiente, "", deposito);
        //    reserva.Costo = _servicioReserva.CalcularPrecioDeposito(fechaInicio, fechaFin, deposito);

        //    Assert.AreEqual(100, reserva.Costo); // El costo para 2 días debe ser 2 * 50  = 100
        //}

        //[TestMethod]
        //public void TestCalcularPrecioDepositoSinDescuentoConPromocionVigente()
        //{
        //    DateTime fechaInicio = new DateTime(2024, 6, 10);
        //    DateTime fechaFin = new DateTime(2024, 6, 12);  // 2 días

        //    Promocion promo = new Promocion(DateTime.Today.AddDays(-5), DateTime.Today.AddYears(2)); // 25% por defecto
        //    Deposito deposito = new Deposito(EnumArea.A, EnumTamano.Pequeno, false, promo);
        //    Cliente cliente = new Cliente("Frank", "Lampard", "Pa$$w0Rdd", "lampard@gmail.com");

        //    Reserva reserva = new Reserva(cliente, fechaInicio, fechaFin, 0, EnumEstado.Pendiente, "", deposito);
        //    reserva.Costo = _servicioReserva.CalcularPrecioDeposito(fechaInicio, fechaFin, deposito);

        //    Assert.AreEqual(75, reserva.Costo); // El costo para 2 días con 25% debe ser 2 * 50 * (1 - 0,25) = 75
        //}

        //[TestMethod]
        //public void TestCalcularPrecioDepositoConDescuentoConPromocionVigente2()
        //{
        //    DateTime fechaInicio = new DateTime(2024, 6, 1);
        //    DateTime fechaFin = new DateTime(2024, 6, 23);  // 22 días

        //    Promocion promo = new Promocion(DateTime.Today.AddDays(-5), DateTime.Today.AddYears(2)); // 25% por defecto
        //    Deposito deposito = new Deposito(EnumArea.A, EnumTamano.Mediano, false, promo);
        //    Cliente cliente = new Cliente("Alex", "DeLarge", "Pa$$w0Rdd", "orange666@outlook.com.uk");

        //    Reserva reserva = new Reserva(cliente, fechaInicio, fechaFin, 0, EnumEstado.Pendiente, "", deposito);
        //    reserva.Costo = _servicioReserva.CalcularPrecioDeposito(fechaInicio, fechaFin, deposito);

        //    Assert.AreEqual(1072.5m, reserva.Costo); // El costo para 22 días con 35% debe ser: 22 * 75 * (1 - (0.25 + 0.10)) = 1072.5
        //}

        //[TestMethod]
        //public void TestCalcularPrecioDepositoConDescuentoConPromocionVigente3()
        //{
        //    DateTime fechaInicio = new DateTime(2024, 6, 1);
        //    DateTime fechaFin = new DateTime(2024, 6, 10);  // 9 días

        //    Promocion promo = new Promocion(DateTime.Today.AddDays(-5), DateTime.Today.AddYears(2)); // 25% por defecto
        //    Deposito deposito = new Deposito(EnumArea.A, EnumTamano.Pequeno, false, promo);
        //    Cliente cliente = new Cliente("Winston", "Smith", "Pa$$w0Rdd1984", "sendhelp@gmail.com.uk");

        //    Reserva reserva = new Reserva(cliente, fechaInicio, fechaFin, 0, EnumEstado.Pendiente, "", deposito);
        //    reserva.Costo = _servicioReserva.CalcularPrecioDeposito(fechaInicio, fechaFin, deposito);

        //    Assert.AreEqual(315m, reserva.Costo); // El costo para 9 días con 35% debe ser: 9 * 50 * (1 - (0.25 + 0.10)) = 315
        //}

        //[TestMethod]
        //public void TestCalcularPrecioDepositoConDescuentoConPromocionVigente4()
        //{
        //    DateTime fechaInicio = new DateTime(2024, 6, 1);
        //    DateTime fechaFin = new DateTime(2024, 6, 2);  // 1 día

        //    Promocion promo = new Promocion(DateTime.Today.AddDays(-5), DateTime.Today.AddYears(2)); // 25% por defecto
        //    Deposito deposito = new Deposito(EnumArea.D, EnumTamano.Grande, true, promo);
        //    Cliente cliente = new Cliente("Barton", "Daves", "Pa$$w0Rdd333", "kaboom@gmail.com");

        //    Reserva reserva = new Reserva(cliente, fechaInicio, fechaFin, 0, EnumEstado.Pendiente, "", deposito);
        //    reserva.Costo = _servicioReserva.CalcularPrecioDeposito(fechaInicio, fechaFin, deposito);

        //    Assert.AreEqual(90m, reserva.Costo); // El costo para 1 día con 25% debe ser: (1 * 100) + (1 * 20) * (1 - 0.25) = 90
        //}

        [TestMethod]
        public void TestCalcularPrecioDepositoCon100DescuentoBUG()
        {
            DateTime fechaInicio = new DateTime(2024, 6, 1);
            DateTime fechaFin = new DateTime(2024, 6, 10);  // 9 días

            Promocion promo = new Promocion();
            promo.Desde = DateTime.Today.AddDays(-5);
            promo.Hasta = DateTime.Today.AddYears(2);
            promo.Descuento = 1.00m;

            Deposito deposito = new Deposito(EnumArea.A, EnumTamano.Pequeno, false, promo);
            Cliente cliente = new Cliente("Christopher", "Wallace", "BR0@KlYn", "the_notorious_big@vera.com.uy");

            Reserva reserva = new Reserva(cliente, fechaInicio, fechaFin, 0, EnumEstado.Pendiente, "", deposito);
            reserva.Costo = _servicioReserva.CalcularPrecioDeposito(fechaInicio, fechaFin, deposito);

            Assert.AreEqual(0m, reserva.Costo); // El costo para 9 días con 100% debe ser: = 0
        }
    }
}
