using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace LevantamientoDeRed.Migrations
{
    public partial class AgregarColumnaCoordenadasTablaClientes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Point>(
                name: "Coordenadas",
                table: "Clientes",
                type: "geometry",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Coordenadas",
                table: "Clientes");
        }
    }
}
