﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LevantamientoDeRed.Migrations
{
    public partial class QuitarAgregarPropiedades : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "Tipo",
                table: "Mufas",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nombre",
                table: "Mufas",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Nombre",
                table: "Gpons",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nombre",
                table: "Mufas");

            migrationBuilder.DropColumn(
                name: "Nombre",
                table: "Gpons");

            migrationBuilder.AlterColumn<string>(
                name: "Tipo",
                table: "Mufas",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

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
    }
}
