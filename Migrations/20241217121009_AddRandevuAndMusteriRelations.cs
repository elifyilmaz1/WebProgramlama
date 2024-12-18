using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebProgramlama.Migrations
{
    /// <inheritdoc />
    public partial class AddRandevuAndMusteriRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "IletisimNumarasi",
                table: "Musteri",
                type: "text",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "IletisimNumarasi",
                table: "Musteri",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
