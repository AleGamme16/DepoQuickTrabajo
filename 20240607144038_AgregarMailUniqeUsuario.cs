using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class AgregarMailUniqeUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Registros_Usuarios_UsuarioId",
                table: "Registros");

            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "Registros",
                newName: "UsuarioID");

            migrationBuilder.RenameIndex(
                name: "IX_Registros_UsuarioId",
                table: "Registros",
                newName: "IX_Registros_UsuarioID");

            migrationBuilder.AlterColumn<string>(
                name: "TipoAccion",
                table: "Registros",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "UsuarioApellido",
                table: "Registros",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UsuarioNombre",
                table: "Registros",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Mail",
                table: "Usuarios",
                column: "Mail",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Registros_Usuarios_UsuarioID",
                table: "Registros",
                column: "UsuarioID",
                principalTable: "Usuarios",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Registros_Usuarios_UsuarioID",
                table: "Registros");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_Mail",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "UsuarioApellido",
                table: "Registros");

            migrationBuilder.DropColumn(
                name: "UsuarioNombre",
                table: "Registros");

            migrationBuilder.RenameColumn(
                name: "UsuarioID",
                table: "Registros",
                newName: "UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_Registros_UsuarioID",
                table: "Registros",
                newName: "IX_Registros_UsuarioId");

            migrationBuilder.AlterColumn<string>(
                name: "TipoAccion",
                table: "Registros",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddForeignKey(
                name: "FK_Registros_Usuarios_UsuarioId",
                table: "Registros",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
