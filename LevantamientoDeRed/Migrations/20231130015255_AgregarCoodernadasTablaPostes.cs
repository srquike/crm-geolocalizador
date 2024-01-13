using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace LevantamientoDeRed.Migrations
{
    public partial class AgregarCoodernadasTablaPostes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Point>(
                name: "Coordenadas",
                table: "Postes",
                type: "geometry",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Coordenadas",
                table: "Postes");
        }
    }
}
