using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebProgramlama.Migrations
{
    /// <inheritdoc />
    public partial class AddMusteriColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
   migrationBuilder.AddColumn<string>(
            name: "Eposta",
            table: "Musteri",
            type: "text",
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<string>(
            name: "Sifre",
            table: "Musteri",
            type: "text",
            nullable: false,
            defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
  migrationBuilder.DropColumn(
            name: "Eposta",
            table: "Musteri");

        migrationBuilder.DropColumn(
            name: "Sifre",
            table: "Musteri");
        }
    }
}
