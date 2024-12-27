using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebProgramlama.Migrations
{
    /// <inheritdoc />
    public partial class CalisanTabloD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MolaSaati",
                table: "Calisan",
                newName: "YapabildigiIslemler");

            migrationBuilder.AddColumn<string>(
                name: "UzmanlikAlani",
                table: "Calisan",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UzmanlikAlani",
                table: "Calisan");

            migrationBuilder.RenameColumn(
                name: "YapabildigiIslemler",
                table: "Calisan",
                newName: "MolaSaati");
        }
    }
}
