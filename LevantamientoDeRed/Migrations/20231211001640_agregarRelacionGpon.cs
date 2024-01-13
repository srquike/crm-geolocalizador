using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LevantamientoDeRed.Migrations
{
    public partial class agregarRelacionGpon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GponId",
                table: "Mufas",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Mufas_GponId",
                table: "Mufas",
                column: "GponId");

            migrationBuilder.AddForeignKey(
                name: "FK_Mufas_Gpons_GponId",
                table: "Mufas",
                column: "GponId",
                principalTable: "Gpons",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mufas_Gpons_GponId",
                table: "Mufas");

            migrationBuilder.DropIndex(
                name: "IX_Mufas_GponId",
                table: "Mufas");

            migrationBuilder.DropColumn(
                name: "GponId",
                table: "Mufas");
        }
    }
}
