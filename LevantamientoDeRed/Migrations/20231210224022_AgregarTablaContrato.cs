using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LevantamientoDeRed.Migrations
{
    public partial class AgregarTablaContrato : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContratoId",
                table: "Clientes",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Servicio",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servicio", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contrato",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: false),
                    PrecioMensual = table.Column<double>(type: "double precision", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaFinal = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ServicioId = table.Column<string>(type: "text", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contrato", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contrato_Servicio_ServicioId",
                        column: x => x.ServicioId,
                        principalTable: "Servicio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_ContratoId",
                table: "Clientes",
                column: "ContratoId");

            migrationBuilder.CreateIndex(
                name: "IX_Contrato_ServicioId",
                table: "Contrato",
                column: "ServicioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_Contrato_ContratoId",
                table: "Clientes",
                column: "ContratoId",
                principalTable: "Contrato",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_Contrato_ContratoId",
                table: "Clientes");

            migrationBuilder.DropTable(
                name: "Contrato");

            migrationBuilder.DropTable(
                name: "Servicio");

            migrationBuilder.DropIndex(
                name: "IX_Clientes_ContratoId",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "ContratoId",
                table: "Clientes");
        }
    }
}
