using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LevantamientoDeRed.Migrations
{
    public partial class AgregarOnDeleteTablaGpons : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gpons_Postes_PosteId",
                table: "Gpons");

            migrationBuilder.AddForeignKey(
                name: "FK_Gpons_Postes_PosteId",
                table: "Gpons",
                column: "PosteId",
                principalTable: "Postes",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gpons_Postes_PosteId",
                table: "Gpons");

            migrationBuilder.AddForeignKey(
                name: "FK_Gpons_Postes_PosteId",
                table: "Gpons",
                column: "PosteId",
                principalTable: "Postes",
                principalColumn: "Id");
        }
    }
}
