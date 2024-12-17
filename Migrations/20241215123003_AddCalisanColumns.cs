using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebProgramlama.Migrations
{
    public partial class AddCalisanColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "BaslangicSaati",
                table: "Randevu",
                type: "interval",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<string>(
                name: "CalismaSaatleri",
                table: "Calisan",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MolaSaati",
                table: "Calisan",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BaslangicSaati",
                table: "Randevu");

            migrationBuilder.DropColumn(
                name: "CalismaSaatleri",
                table: "Calisan");

            migrationBuilder.DropColumn(
                name: "MolaSaati",
                table: "Calisan");
        }
    }
}
