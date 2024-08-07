using Backend;
using Backend.ManejoDeRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestClass]
    public class testMemoryDatabase
    {
        private MemoryDatabase _dataAccess;

        private RepositorioReserva _reservaRepository;
        private RepositorioDeposito _depositoRepository;
        private RepositorioPromocion _promocionRepository;
        private RepositorioValoracion _valoracionRepository;
        private RepositorioUsuario _usuarioRepository;
        private RepositorioRegistroAcciones _registroAccionesRepository;

        [TestInitialize]
        public void Initialize()
        {
            _dataAccess = new MemoryDatabase();

            _reservaRepository = new RepositorioReserva(_dataAccess);
            _depositoRepository = new RepositorioDeposito(_dataAccess);
            _promocionRepository = new RepositorioPromocion(_dataAccess);
            _valoracionRepository = new RepositorioValoracion(_dataAccess, _registroAccionesRepository);
            _usuarioRepository = new RepositorioUsuario(_dataAccess);
        }

        [TestMethod]
        public void TestAgregarReserva()
        {
            Cliente cliente = new Cliente("Ricardo", "Kaka", "Ps$$W0rdkaka", "kaka8@gmail.com");
            Deposito deposito = new Deposito();  
            Reserva unaReserva = new Reserva(cliente, DateTime.Now.AddDays(3),
                DateTime.Now.AddDays(7), 1500, EnumEstado.Pendiente, "", deposito);

            _reservaRepository.AgregarReserva(unaReserva); 

            Assert.IsTrue(_dataAccess.Reservas.Contains(unaReserva));
        }

        [TestMethod]
        public void TestAgregarYDespuesBorrarReserva()
        {
            Cliente cliente = new Cliente("Ricardo", "Kaka", "Ps$$W0rdkaka", "kaka8@gmail.com");
            Deposito deposito = new Deposito();
            Reserva unaReserva = new Reserva(cliente, DateTime.Now.AddDays(3),
                DateTime.Now.AddDays(7), 1500, EnumEstado.Pendiente, "", deposito);

            _reservaRepository.AgregarReserva(unaReserva);
            _reservaRepository.BorrarReserva(unaReserva);

            Assert.IsFalse(_dataAccess.Reservas.Contains(unaReserva));
        }

        [TestMethod]
        public void TestAgregarDeposito()
        {
            var unDeposito = new Deposito();
            _depositoRepository.AgregarDeposito(unDeposito);

            Assert.IsTrue(_dataAccess.Depositos.Contains(unDeposito));
        }

        [TestMethod]
        public void TestAgregarYDespuesBorrarDeposito()
        {
            Deposito unDeposito = new Deposito();
            _depositoRepository.AgregarDeposito(unDeposito);
            _depositoRepository.BorrarDeposito(unDeposito);
           
            Assert.IsFalse(_depositoRepository.ExisteDeposito(unDeposito));
        }

        [TestMethod]
        public void TestAgregarPromocion()
        {
            Promocion unaPromocion = new Promocion("Verano", 0.15m,
                new DateTime(2024, 9, 25), new DateTime(2024, 9, 29));

            _promocionRepository.AgregarPromocion(unaPromocion);

            Assert.IsTrue(_dataAccess.Promociones.Contains(unaPromocion));
        }

        [TestMethod]
        public void TestAgregarYDespuesBorrarPromocion()
        {
            Promocion unaPromocion = new Promocion("Verano", 0.15m,
                new DateTime(2024, 9, 25), new DateTime(2024, 9, 29));

            _promocionRepository.AgregarPromocion(unaPromocion);
            _promocionRepository.BorrarPromocion(unaPromocion);

            Assert.IsFalse(_promocionRepository.ExistePromocion(unaPromocion));
        }

        [TestMethod]
        public void TestAgregarValoracion()
        {
            Valoracion unaValoracion = new Valoracion();

            _valoracionRepository.AgregarValoracion(unaValoracion);

            Assert.IsTrue(_dataAccess.Valoraciones.Contains(unaValoracion));
        }

        public void TestAgregarYDespuesBorrarValoracion()
        {
            Valoracion unaValoracion = new Valoracion();

            _valoracionRepository.AgregarValoracion(unaValoracion);
            _valoracionRepository.BorrarValoracion(unaValoracion);

            Assert.IsFalse(_valoracionRepository.ExisteValoracion(unaValoracion));
        }

        [TestMethod]
        public void TestAgregarUsuario()
        {
            Usuario unUsuario = new Usuario();

            _usuarioRepository.AgregarUsuario(unUsuario);

            Assert.IsTrue(_dataAccess.Usuarios.Contains(unUsuario));
        }

        public void TestAgregarYDespuesBorrarUsuario()
        {
            Usuario unUsuario = new Usuario();

            _usuarioRepository.AgregarUsuario(unUsuario);
            _usuarioRepository.BorrarUsuario(unUsuario);

            Assert.IsFalse(_dataAccess.Usuarios.Contains(unUsuario));
        }

        [TestMethod]
        public void TestBuscarReservaExistente()
        {
            Cliente cliente = new Cliente("Luis", "Figo", "Ps$$W0rdFig0", "lf100@gmail.com");
            Deposito deposito = new Deposito();
            Reserva unaReserva = new Reserva(cliente, DateTime.Now.AddDays(1),
                DateTime.Now.AddDays(7), 1500, EnumEstado.Pendiente, "", deposito);

            _reservaRepository.AgregarReserva(unaReserva);
            bool resultado = _reservaRepository.ExisteReserva(unaReserva);

            Assert.IsTrue(resultado, "La reserva debería existir en el repositorio.");
        }

        [TestMethod]
        public void TestBuscarReservaInexistente()
        {
            Cliente cliente = new Cliente("Luis", "Figo", "Ps$$W0rdFig0", "lf100@gmail.com");
            Deposito deposito = new Deposito();
            Reserva unaReserva = new Reserva(cliente, DateTime.Now.AddDays(1),
                DateTime.Now.AddDays(7), 1500, EnumEstado.Pendiente, "", deposito);
            bool resultado = _reservaRepository.ExisteReserva(unaReserva);

            Assert.IsFalse(resultado, "La reserva no debería existir en el repositorio.");
        }

        [TestMethod]
        public void TestBuscarDepositoExistente()
        {
            Deposito deposito = new Deposito();

            _depositoRepository.AgregarDeposito(deposito);

            bool resultado = _depositoRepository.ExisteDeposito(deposito);

            Assert.IsTrue(_dataAccess.Depositos.Contains(deposito));
        }

        [TestMethod]
        public void TestBuscarDepositoInexistente()
        {
            Deposito deposito = new Deposito();

            bool resultado = _depositoRepository.ExisteDeposito(deposito);

            Assert.IsFalse(_dataAccess.Depositos.Contains(deposito));
        }

        [TestMethod]
        public void TestBuscarPromocionExistente()
        {
            Promocion promocion = new Promocion();

            _promocionRepository.AgregarPromocion(promocion);

            bool resultado = _promocionRepository.ExistePromocion(promocion);

            Assert.IsTrue(_dataAccess.Promociones.Contains(promocion));
        }

        [TestMethod]
        public void TestBuscarPromocionInexistente()
        {
            Promocion promocion = new Promocion();

            bool resultado = _promocionRepository.ExistePromocion(promocion);

            Assert.IsFalse(_dataAccess.Promociones.Contains(promocion));
        }

        [TestMethod]
        public void TestBuscarValoracionExistente()
        {
            Valoracion valoracion = new Valoracion();

            _valoracionRepository.AgregarValoracion(valoracion);

            bool resultado = _valoracionRepository.ExisteValoracion(valoracion);

            Assert.IsTrue(_dataAccess.Valoraciones.Contains(valoracion));
        }

        [TestMethod]
        public void TestBuscarValoracionInexistente()
        {
            Valoracion valoracion = new Valoracion();
 
            bool resultado = _valoracionRepository.ExisteValoracion(valoracion);

            Assert.IsFalse(_dataAccess.Valoraciones.Contains(valoracion));
        }

        [TestMethod]
        public void TestBuscarUsuarioExistente()
        {
            Usuario unUsuario = new Usuario();

            _usuarioRepository.AgregarUsuario(unUsuario);

            bool resultado = _usuarioRepository.ExisteUsuario(unUsuario);

            Assert.IsTrue(_dataAccess.Usuarios.Contains(unUsuario));
        }

        [TestMethod]
        public void TestBuscarUsuarioInexistente()
        {
            Usuario unUsuario = new Usuario();

            Assert.IsFalse(_dataAccess.Usuarios.Contains(unUsuario));
        }
    }
}