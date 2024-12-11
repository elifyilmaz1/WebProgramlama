using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebProgramlama.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Calisan",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Isim = table.Column<string>(type: "text", nullable: false),
                    Gorev = table.Column<string>(type: "text", nullable: false),
                    SaatlikUcret = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calisan", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Hizmet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Isim = table.Column<string>(type: "text", nullable: false),
                    Ucret = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hizmet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Musteri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IsimSoyisim = table.Column<string>(type: "text", nullable: false),
                    IletisimNumarasi = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Musteri", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Randevu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RandevuTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    MusteriId = table.Column<int>(type: "integer", nullable: false),
                    CalisanId = table.Column<int>(type: "integer", nullable: false),
                    Ucret = table.Column<decimal>(type: "numeric", nullable: false),
                    HizmetId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Randevu", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Randevu_Calisan_CalisanId",
                        column: x => x.CalisanId,
                        principalTable: "Calisan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Randevu_Hizmet_HizmetId",
                        column: x => x.HizmetId,
                        principalTable: "Hizmet",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Randevu_Musteri_MusteriId",
                        column: x => x.MusteriId,
                        principalTable: "Musteri",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Randevu_CalisanId",
                table: "Randevu",
                column: "CalisanId");

            migrationBuilder.CreateIndex(
                name: "IX_Randevu_HizmetId",
                table: "Randevu",
                column: "HizmetId");

            migrationBuilder.CreateIndex(
                name: "IX_Randevu_MusteriId",
                table: "Randevu",
                column: "MusteriId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Randevu");

            migrationBuilder.DropTable(
                name: "Calisan");

            migrationBuilder.DropTable(
                name: "Hizmet");

            migrationBuilder.DropTable(
                name: "Musteri");
        }
    }
}
