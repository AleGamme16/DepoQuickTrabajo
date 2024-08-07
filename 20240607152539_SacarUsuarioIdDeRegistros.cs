using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class SacarUsuarioIdDeRegistros : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Registros_Usuarios_UsuarioID",
                table: "Registros");

            migrationBuilder.DropIndex(
                name: "IX_Registros_UsuarioID",
                table: "Registros");

            migrationBuilder.DropColumn(
                name: "UsuarioID",
                table: "Registros");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UsuarioID",
                table: "Registros",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Registros_UsuarioID",
                table: "Registros",
                column: "UsuarioID");

            migrationBuilder.AddForeignKey(
                name: "FK_Registros_Usuarios_UsuarioID",
                table: "Registros",
                column: "UsuarioID",
                principalTable: "Usuarios",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
