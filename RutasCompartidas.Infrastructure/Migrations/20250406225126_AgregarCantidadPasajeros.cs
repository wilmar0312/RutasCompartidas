using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RutasCompartidas.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AgregarCantidadPasajeros : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CantidadPasajeros",
                table: "Rutas",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CantidadPasajeros",
                table: "Rutas");
        }
    }
}
