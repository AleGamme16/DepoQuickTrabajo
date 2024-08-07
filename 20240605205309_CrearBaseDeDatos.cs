using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class CrearBaseDeDatos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Promociones",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Etiqueta = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Descuento = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Desde = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Hasta = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promociones", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Contrasena = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Rol = table.Column<int>(type: "int", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Depositos",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Area = table.Column<int>(type: "int", nullable: false),
                    Tamano = table.Column<int>(type: "int", nullable: false),
                    Climatizado = table.Column<bool>(type: "bit", nullable: false),
                    PromocionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Depositos", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Depositos_Promociones_PromocionId",
                        column: x => x.PromocionId,
                        principalTable: "Promociones",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Registros",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoAccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    FechaHora = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registros", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Registros_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservas",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClienteId = table.Column<int>(type: "int", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Costo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false),
                    MotivoRechazo = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    DepositoId = table.Column<int>(type: "int", nullable: false),
                    EstadoPago = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservas", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Reservas_Depositos_DepositoId",
                        column: x => x.DepositoId,
                        principalTable: "Depositos",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservas_Usuarios_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Usuarios",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Valoraciones",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Estrellas = table.Column<int>(type: "int", nullable: false),
                    Comentario = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DepositoId = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Valoraciones", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Valoraciones_Depositos_DepositoId",
                        column: x => x.DepositoId,
                        principalTable: "Depositos",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Valoraciones_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Depositos_PromocionId",
                table: "Depositos",
                column: "PromocionId");

            migrationBuilder.CreateIndex(
                name: "IX_Registros_UsuarioId",
                table: "Registros",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_ClienteId",
                table: "Reservas",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_DepositoId",
                table: "Reservas",
                column: "DepositoId");

            migrationBuilder.CreateIndex(
                name: "IX_Valoraciones_DepositoId",
                table: "Valoraciones",
                column: "DepositoId");

            migrationBuilder.CreateIndex(
                name: "IX_Valoraciones_UsuarioId",
                table: "Valoraciones",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Registros");

            migrationBuilder.DropTable(
                name: "Reservas");

            migrationBuilder.DropTable(
                name: "Valoraciones");

            migrationBuilder.DropTable(
                name: "Depositos");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Promociones");
        }
    }
}
