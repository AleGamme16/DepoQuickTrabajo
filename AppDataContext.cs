using System;
using Microsoft.EntityFrameworkCore;

namespace Backend.Context
{
    public class AppDataContext : DbContext
    {
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<Deposito> Depositos { get; set; }
        public DbSet<Promocion> Promociones { get; set; }
        public DbSet<Valoracion> Valoraciones { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Administrador> Administradores { get; set; }
        public DbSet<RegistroAccion> Registros { get; set; }
        public DbSet<Disponibilidad> Disponibilidades { get; set; }
        public DbSet<Notificacion> Notificaciones { get; set; }

        public AppDataContext(DbContextOptions<AppDataContext> options) : base(options)
        {
            if (!Database.IsInMemory())
            {
                Database.Migrate();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.ID).ValueGeneratedOnAdd();
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Apellido).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Contrasena).IsRequired();
                entity.Property(e => e.Mail).IsRequired().HasMaxLength(100);
                entity.HasIndex(e => e.Mail).IsUnique();
                entity.Property(e => e.Rol).IsRequired();
                entity.HasDiscriminator<string>("Discriminator")
                      .HasValue<Usuario>("Usuario")
                      .HasValue<Administrador>("Administrador")
                      .HasValue<Cliente>("Cliente");
            });

            modelBuilder.Entity<Reserva>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.ID).ValueGeneratedOnAdd();
                entity.Property(e => e.Costo).HasColumnType("decimal(18,2)").IsRequired();
                entity.Property(e => e.FechaInicio).IsRequired();
                entity.Property(e => e.FechaFin).IsRequired();
                entity.Property(e => e.Estado).IsRequired();
                entity.Property(e => e.MotivoRechazo).HasMaxLength(300);
                entity.Property(e => e.EstadoPago)
              .IsRequired(false);

                entity.HasOne(e => e.Cliente)
                      .WithMany()
                      .HasForeignKey("ClienteId")
                      .IsRequired();

                entity.HasOne(e => e.Deposito)
                      .WithMany()
                      .HasForeignKey("DepositoId")
                      .IsRequired();
            });

            modelBuilder.Entity<Promocion>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.ID).ValueGeneratedOnAdd();
                entity.Property(e => e.Etiqueta).IsRequired().HasMaxLength(20);
                entity.Property(e => e.Descuento).HasColumnType("decimal(18,2)").IsRequired();
                entity.Property(e => e.Desde).IsRequired();
                entity.Property(e => e.Hasta).IsRequired();
            });

            modelBuilder.Entity<Deposito>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.ID).ValueGeneratedOnAdd();
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Area).IsRequired();
                entity.Property(e => e.Tamano).IsRequired();
                entity.Property(e => e.Climatizado).IsRequired();

                entity.HasOne(e => e.Promo)
                      .WithMany()
                      .HasForeignKey("PromocionId");

                entity.HasMany(e => e.Disponibilidades)
                     .WithOne()
                     .HasForeignKey(d => d.DepositoID);
            });

            modelBuilder.Entity<Disponibilidad>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.ID).ValueGeneratedOnAdd();
                entity.Property(e => e.FechaInicio).IsRequired();
                entity.Property(e => e.FechaFin).IsRequired();
            });

            modelBuilder.Entity<Valoracion>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.ID).ValueGeneratedOnAdd();
                entity.Property(e => e.Estrellas).IsRequired();
                entity.Property(e => e.Comentario).HasMaxLength(500);

                entity.HasOne(e => e.Deposito)
                      .WithMany()
                      .HasForeignKey("DepositoId")
                      .IsRequired();

                entity.HasOne(e => e.Usuario)
                      .WithMany()
                      .HasForeignKey("UsuarioId")
                      .IsRequired();
            });

            modelBuilder.Entity<RegistroAccion>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.ID).ValueGeneratedOnAdd();
                entity.Property(e => e.TipoAccion).IsRequired().HasMaxLength(100);
                entity.Property(e => e.UsuarioNombre).IsRequired().HasMaxLength(100);
                entity.Property(e => e.UsuarioApellido).IsRequired().HasMaxLength(100);
                entity.Property(e => e.FechaHora).IsRequired();
            });

            modelBuilder.Entity<Notificacion>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.Mensaje).IsRequired().HasMaxLength(500);
                entity.HasOne(e => e.Usuario)
                    .WithMany()
                    .HasForeignKey("UsuarioID")
                    .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(e => e.Reserva)
                    .WithMany()
                    .HasForeignKey("ReservaID")
                    .OnDelete(DeleteBehavior.Restrict); 
                entity.Property(e => e.Fecha).IsRequired();
            });


            // Configuración adicional para Administrador y Cliente para que no intenten definir la clave primaria de nuevo
            modelBuilder.Entity<Administrador>().HasBaseType<Usuario>();
            modelBuilder.Entity<Cliente>().HasBaseType<Usuario>();
        }
    }
}



