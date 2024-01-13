using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LevantamientoDeRed.Migrations
{
    public partial class AgregarOnDeleteColumnaGponIdTablaMufas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mufas_Gpons_GponId",
                table: "Mufas");

            migrationBuilder.AddForeignKey(
                name: "FK_Mufas_Gpons_GponId",
                table: "Mufas",
                column: "GponId",
                principalTable: "Gpons",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mufas_Gpons_GponId",
                table: "Mufas");

            migrationBuilder.AddForeignKey(
                name: "FK_Mufas_Gpons_GponId",
                table: "Mufas",
                column: "GponId",
                principalTable: "Gpons",
                principalColumn: "Id");
        }
    }
}
