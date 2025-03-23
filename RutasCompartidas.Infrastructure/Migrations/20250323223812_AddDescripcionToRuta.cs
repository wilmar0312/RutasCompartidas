using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RutasCompartidas.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDescripcionToRuta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Descripcion",
                table: "Rutas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descripcion",
                table: "Rutas");
        }
    }
}
