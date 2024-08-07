using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class AgregarNotificaciones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Notificaciones",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mensaje = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    UsuarioID = table.Column<int>(type: "int", nullable: false),
                    ReservaID = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notificaciones", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Notificaciones_Reservas_ReservaID",
                        column: x => x.ReservaID,
                        principalTable: "Reservas",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Notificaciones_Usuarios_UsuarioID",
                        column: x => x.UsuarioID,
                        principalTable: "Usuarios",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notificaciones_ReservaID",
                table: "Notificaciones",
                column: "ReservaID");

            migrationBuilder.CreateIndex(
                name: "IX_Notificaciones_UsuarioID",
                table: "Notificaciones",
                column: "UsuarioID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notificaciones");
        }
    }
}
