using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LevantamientoDeRed.Migrations
{
    public partial class AgregarOnDeleteColummnMufaIdTablaClientes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_Mufas_MufaId",
                table: "Clientes");

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_Mufas_MufaId",
                table: "Clientes",
                column: "MufaId",
                principalTable: "Mufas",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_Mufas_MufaId",
                table: "Clientes");

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_Mufas_MufaId",
                table: "Clientes",
                column: "MufaId",
                principalTable: "Mufas",
                principalColumn: "Id");
        }
    }
}
