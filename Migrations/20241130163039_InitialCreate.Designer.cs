﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WebProgramlama.Models;

#nullable disable

namespace WebProgramlama.Migrations
{
    [DbContext(typeof(UygulamaDbContextModelSnapshot))]
    [Migration("20241130163039_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("WebProgramlama.Models.calisan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("görev")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("isim")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("saatlikUcret")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.ToTable("calisanlar");
                });

            modelBuilder.Entity("WebProgramlama.Models.hizmet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Ucret")
                        .HasColumnType("numeric");

                    b.Property<string>("isim")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("hizmetler");
                });

            modelBuilder.Entity("WebProgramlama.Models.musteri", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal>("iletisimNumarasi")
                        .HasColumnType("numeric");

                    b.Property<string>("isimSoyisim")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("musteriler");
                });

            modelBuilder.Entity("WebProgramlama.Models.randevu", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("calisanId")
                        .HasColumnType("integer");

                    b.Property<string>("hizmet")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("hizmetId")
                        .HasColumnType("integer");

                    b.Property<int>("musteriId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("randevuTarihi")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal>("ucret")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("calisanId");

                    b.HasIndex("hizmetId");

                    b.HasIndex("musteriId");

                    b.ToTable("randevular");
                });

            modelBuilder.Entity("WebProgramlama.Models.randevu", b =>
                {
                    b.HasOne("WebProgramlama.Models.calisan", "calisanlar")
                        .WithMany("randevular")
                        .HasForeignKey("calisanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebProgramlama.Models.hizmet", null)
                        .WithMany("randevular")
                        .HasForeignKey("hizmetId");

                    b.HasOne("WebProgramlama.Models.musteri", "musteriler")
                        .WithMany("randevular")
                        .HasForeignKey("musteriId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("calisanlar");

                    b.Navigation("musteriler");
                });

            modelBuilder.Entity("WebProgramlama.Models.calisan", b =>
                {
                    b.Navigation("randevular");
                });

            modelBuilder.Entity("WebProgramlama.Models.hizmet", b =>
                {
                    b.Navigation("randevular");
                });

            modelBuilder.Entity("WebProgramlama.Models.musteri", b =>
                {
                    b.Navigation("randevular");
                });
#pragma warning restore 612, 618
        }
    }
}
