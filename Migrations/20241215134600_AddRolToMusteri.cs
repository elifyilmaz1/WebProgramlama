using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebProgramlama.Migrations
{
    /// <inheritdoc />
    public partial class AddRolToMusteri : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Rol",
                table: "Musteri",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rol",
                table: "Musteri");
        }
    }
}
