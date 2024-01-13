using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LevantamientoDeRed.Migrations
{
    public partial class eliminarRelacionPoste : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mufas_Postes_PosteId",
                table: "Mufas");

            migrationBuilder.DropIndex(
                name: "IX_Mufas_PosteId",
                table: "Mufas");

            migrationBuilder.DropColumn(
                name: "PosteId",
                table: "Mufas");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PosteId",
                table: "Mufas",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Mufas_PosteId",
                table: "Mufas",
                column: "PosteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Mufas_Postes_PosteId",
                table: "Mufas",
                column: "PosteId",
                principalTable: "Postes",
                principalColumn: "Id");
        }
    }
}
