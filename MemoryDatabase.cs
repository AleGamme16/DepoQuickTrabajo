using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore​;

namespace Backend
{
    public class MemoryDatabase
    {
        public List<Reserva> Reservas { get; set; }
        public List<Deposito> Depositos { get; set; }
        public List<Promocion> Promociones { get; set; }
        public List<Valoracion> Valoraciones { get; set; }
        public List<Usuario> Usuarios { get; set; }
        public Administrador Administrador { get; set; }

        public List<RegistroAccion> Registros = new List<RegistroAccion>();

        public MemoryDatabase()
        {
            Reservas = new List<Reserva>();
            Depositos = new List<Deposito>();
            Promociones = new List<Promocion>();
            Valoraciones = new List<Valoracion>();
            Usuarios = new List<Usuario>();
            Administrador = new Administrador();
            Registros=new List<RegistroAccion>();

            //InicializarDatosDePrueba_Reserva();
            //InicializarDatosDePrueba_Deposito();
            //InicializarDatosDePrueba_Promocion();
            //InicializarDatosDePrueba_Valoracion();
            //InicializarDatosDePrueba_Usuario();
        }

        private void InicializarDatosDePrueba_Reserva() 
        {
            Cliente cliente1 = new Cliente("Zinedine", "Zidane", "Pa$$w0rD", "mundial98@gmail.com");
            Deposito deposito1 = new Deposito(EnumArea.A, EnumTamano.Grande, true, 
                            new Promocion { Etiqueta = "Descuento", Descuento = 5,
                                    Desde = DateTime.Now, Hasta = DateTime.Now.AddMonths(2) });

            Cliente cliente2 = new Cliente("Pablo", "Garcia", "Pa$$w0rD", "celesteDeAntes@gmail.com");
            Deposito deposito2 = new Deposito(EnumArea.B, EnumTamano.Mediano, true,
                            new Promocion { Etiqueta = "Descuento Uru", Descuento = 25,
                                    Desde = DateTime.Now, Hasta = DateTime.Now.AddMonths(3) });

            Cliente cliente3 = new Cliente("Andres", "Iniesta", "Pa$$w0rD", "sudafrica10@gmail.com");
            Deposito deposito3 = new Deposito(EnumArea.C, EnumTamano.Mediano, true,
                            new Promocion { Etiqueta = "Descuento Mund", Descuento = 18,
                                    Desde = DateTime.Now.AddDays(3), Hasta = DateTime.Now.AddMonths(1) });

            Cliente cliente4 = new Cliente("Xabi", "Alonso", "Pa$$w0rD", "rm@gmail.com");
            Deposito deposito4 = new Deposito(EnumArea.D, EnumTamano.Pequeno, false,
                            new Promocion { Etiqueta = "Descuenton", Descuento = 10,
                                    Desde = DateTime.Now, Hasta = DateTime.Now.AddMonths(2) });

            Cliente cliente5 = new Cliente("David", "Beckham", "Pa$$w0rD", "england7@gmail.com");
            Deposito deposito5 = new Deposito(EnumArea.B, EnumTamano.Grande, true,
                            new Promocion { Etiqueta = "Promo Inv", Descuento = 30,
                                    Desde = DateTime.Now.AddDays(1), Hasta = DateTime.Now.AddDays(40) });

            Cliente cliente6 = new Cliente("Vinicius", "Junior", "Pa$$w0rD", "jogaBonito@gmail.com");
            Deposito deposito6 = new Deposito(EnumArea.C, EnumTamano.Grande, true, new Promocion {Etiqueta = "Promo carioca", Descuento = 30,
                                Desde = DateTime.Now.AddDays(1), Hasta = DateTime.Now.AddDays(40) });


            Reservas.Add(new Reserva(cliente1, DateTime.Now.AddDays(1),
                DateTime.Now.AddDays(3), 1500, EnumEstado.Aprobada, "", deposito1));
            Reservas.Add(new Reserva(cliente2, DateTime.Now.AddDays(1),
                DateTime.Now.AddDays(10), 1323, EnumEstado.Pendiente, "", deposito2));
            Reservas.Add(new Reserva(cliente3, DateTime.Now.AddDays(1),
                DateTime.Now.AddDays(7), 2323, EnumEstado.Rechazada, "Rechazada por fraude", deposito3));
            Reservas.Add(new Reserva(cliente4, DateTime.Now.AddDays(1),
                DateTime.Now.AddDays(20), 9933, EnumEstado.Pendiente, "", deposito4));
            Reservas.Add(new Reserva(cliente5, DateTime.Now.AddDays(1),
                DateTime.Now.AddDays(3), 1500, EnumEstado.Aprobada, "", deposito5));
            Reservas.Add(new Reserva(cliente6, DateTime.Now,
                DateTime.Now.AddDays(-1), 1500, EnumEstado.Aprobada, "", deposito6));

        }

        private void InicializarDatosDePrueba_Deposito()
        {
            Promocion promo = new Promocion("Hola", 0.25m, DateTime.Today.AddDays(2), DateTime.Now.AddDays(4));
            Deposito deposito1 = new Deposito(EnumArea.A, EnumTamano.Grande, false, promo);
            Promocion promo2 = new Promocion("Chau", 0.22m, DateTime.Today.AddDays(5), DateTime.Now.AddDays(8));
            Deposito deposito2 = new Deposito(EnumArea.A, EnumTamano.Grande, true, promo2);

            Depositos.Add(deposito1);
            Depositos.Add(deposito2);

        }

        public void InicializarDatosDePrueba_Promocion()
        {
            var promo1 = new Promocion("Promo1", 0.10m, new DateTime(2024, 6, 14), new DateTime(2024, 6, 24));
            var promo2 = new Promocion("Promo2", 0.10m, new DateTime(2024, 6, 15), new DateTime(2024, 6, 19));
            var promo3 = new Promocion("Promo3", 0.10m, new DateTime(2024, 6, 1), new DateTime(2024, 7, 1));
            var promo4 = new Promocion("Promo4", 0.10m, new DateTime(2024, 6, 15), new DateTime(2024, 7, 15));
            var promo5 = new Promocion("Promo5", 0.10m, new DateTime(2024, 7, 1), new DateTime(2024, 8, 1));
            var promo6 = new Promocion("Promo6", 0.10m, new DateTime(2024, 7, 15), new DateTime(2024, 8, 15));
            var promo7 = new Promocion("Promo7", 0.10m, new DateTime(2024, 8, 1), new DateTime(2024, 9, 1));

            Promociones.Add(promo1);
            Promociones.Add(promo2);
            Promociones.Add(promo3);
            Promociones.Add(promo4);
            Promociones.Add(promo5);
            Promociones.Add(promo6);
            Promociones.Add(promo7);
         }

    private void InicializarDatosDePrueba_Valoracion()
     {
            Deposito depo1 = new Deposito(EnumArea.C, EnumTamano.Mediano, true,
                new Promocion("Descuentirijillo", 0.20m, DateTime.Now.AddDays(3), DateTime.Now.AddMonths(1)));

            Deposito depo2 = new Deposito(EnumArea.D, EnumTamano.Grande, false,
                new Promocion("Descuentazo", 0.60m, DateTime.Now.AddDays(3), DateTime.Now.AddMonths(1)));

            Deposito depo3 = new Deposito(EnumArea.A, EnumTamano.Mediano, true,
                new Promocion("Olakease", 0.69m, DateTime.Now.AddDays(69), DateTime.Now.AddMonths(69)));

            Deposito depo4 = new Deposito(EnumArea.B, EnumTamano.Pequeno, true,
                new Promocion("NoPromo", 0.05m, DateTime.Now.AddDays(1), DateTime.Now.AddDays(3)));

            Cliente cliente1 = new Cliente("Emil", "Sinclair", "Pa$$w0rDkk", "demian@gmail.com");
            Cliente cliente2 = new Cliente("George", "Orwell", "Pa$$w0rD8484", "big__brother@gmail.com");
            Cliente cliente3 = new Cliente("Elon", "Musk", "Te$$L4xdXD", "doge@gmail.com");
            Cliente cliente4 = new Cliente("Kim", "JongUn", "Pa$$w0rD77", "big_brother@gmail.com");


            Valoracion valo1 = new Valoracion(3, "Mas o menos, puede mejorar.",
                                       new Deposito(), new Usuario());

            Valoracion valo2 = new Valoracion(4, "Muy buena experiencia.",
                           depo1, cliente1);

            Valoracion valo3 = new Valoracion(1, "Un asco.",
                           depo2, cliente2);

            Valoracion valo4 = new Valoracion(2, "No lo recomiendo.",
                           depo3, cliente3);

            Valoracion valo5 = new Valoracion(4, "Excelente",
                           depo4, cliente4);

            //Sustituir por AgregarValoracion()
            Valoraciones.Add(valo1);
            Valoraciones.Add(valo2);
            Valoraciones.Add(valo3);
            Valoraciones.Add(valo4);
            Valoraciones.Add(valo5);
        }

        private void InicializarDatosDePrueba_Usuario()
        {
            Cliente cliente1 = new Cliente("Jim", "Morrison", "Pa$$w0rDcomp", "las_puertas@outlook.com");
            Cliente cliente2 = new Cliente("Marie", "Curie", "Pa$$w0rD8484", "plutonio@gmail.com");
            Cliente cliente3 = new Cliente("Pedro", "Figari", "Te$$L4xdXD", "pintor@gmail.com");
            Cliente cliente4 = new Cliente("Bon", "Scott", "Pa$$w0rD77", "highway_to_hell_666@outlook.com");
            Cliente cliente5 = new Cliente("Dave", "Mustaine", "Pa$$w0rDkk", "metallica_1987@gmail.com");
            Cliente cliente6 = new Cliente("Marty", "Friedman", "Pa$$w0rD8484", "wtf@gmail.com");
            Cliente cliente7 = new Cliente("Angus", "Young", "H1Ghw@yt0Hell", "acdc@gmail.com");
            Cliente cliente8 = new Cliente("William", "Wilson", "Pa$$w0rDBl", "doubleidentity@gmail.com");

            // Sustituir por AgregarValoracion()
            Usuarios.Add(cliente1);
            Usuarios.Add(cliente2);
            Usuarios.Add(cliente3);
            Usuarios.Add(cliente4);
            Usuarios.Add(cliente5);
            Usuarios.Add(cliente6);
            Usuarios.Add(cliente7);
            Usuarios.Add(cliente8);

        }
    }
}

