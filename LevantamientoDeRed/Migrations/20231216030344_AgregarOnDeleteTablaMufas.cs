using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LevantamientoDeRed.Migrations
{
    public partial class AgregarOnDeleteTablaMufas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mufas_Postes_PosteId",
                table: "Mufas");

            migrationBuilder.AddForeignKey(
                name: "FK_Mufas_Postes_PosteId",
                table: "Mufas",
                column: "PosteId",
                principalTable: "Postes",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mufas_Postes_PosteId",
                table: "Mufas");

            migrationBuilder.AddForeignKey(
                name: "FK_Mufas_Postes_PosteId",
                table: "Mufas",
                column: "PosteId",
                principalTable: "Postes",
                principalColumn: "Id");
        }
    }
}
