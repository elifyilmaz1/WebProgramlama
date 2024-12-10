#nullable disable
using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace WebProgramlama.Migrations 
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "calisanlar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    isim = table.Column<string>(type: "text", nullable: false),
                    görev = table.Column<string>(type: "text", nullable: false),
                    saatlikUcret = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_calisanlar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "hizmetler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    isim = table.Column<string>(type: "text", nullable: false),
                    Ucret = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hizmetler", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "musteriler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    isimSoyisim = table.Column<string>(type: "text", nullable: false),
                    iletisimNumarasi = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_musteriler", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "randevular",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    musteriId = table.Column<int>(type: "integer", nullable: false),
                    calisanId = table.Column<int>(type: "integer", nullable: false),
                    randevuTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    hizmet = table.Column<string>(type: "text", nullable: false),
                    ucret = table.Column<decimal>(type: "numeric", nullable: false),
                    hizmetId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_randevular", x => x.Id);
                    table.ForeignKey(
                        name: "FK_randevular_calisanlar_calisanId",
                        column: x => x.calisanId,
                        principalTable: "calisanlar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_randevular_hizmetler_hizmetId",
                        column: x => x.hizmetId,
                        principalTable: "hizmetler",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_randevular_musteriler_musteriId",
                        column: x => x.musteriId,
                        principalTable: "musteriler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_randevular_calisanId",
                table: "randevular",
                column: "calisanId");

            migrationBuilder.CreateIndex(
                name: "IX_randevular_hizmetId",
                table: "randevular",
                column: "hizmetId");

            migrationBuilder.CreateIndex(
                name: "IX_randevular_musteriId",
                table: "randevular",
                column: "musteriId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "randevular");

            migrationBuilder.DropTable(
                name: "calisanlar");

            migrationBuilder.DropTable(
                name: "hizmetler");

            migrationBuilder.DropTable(
                name: "musteriler");
        }
    }
}
