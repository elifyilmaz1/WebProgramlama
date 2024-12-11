﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WebProgramlama.Models;


namespace WebProgramlama.Migrations
{
    [DbContext(typeof(UygulamaDbContext))]
    partial class UygulamaDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("WebProgramlama.Models.Calisan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Gorev")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Isim")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("SaatlikUcret")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.ToTable("Calisan");
                });

            modelBuilder.Entity("WebProgramlama.Models.Hizmet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Isim")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Ucret")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.ToTable("Hizmet");
                });

            modelBuilder.Entity("WebProgramlama.Models.Musteri", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal>("IletisimNumarasi")
                        .HasColumnType("numeric");

                    b.Property<string>("IsimSoyisim")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Musteri");
                });

            modelBuilder.Entity("WebProgramlama.Models.Randevu", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CalisanId")
                        .HasColumnType("integer");

                    b.Property<int?>("HizmetId")
                        .HasColumnType("integer");

                    b.Property<int>("MusteriId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("RandevuTarihi")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal>("Ucret")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("CalisanId");

                    b.HasIndex("HizmetId");

                    b.HasIndex("MusteriId");

                    b.ToTable("Randevu");
                });

            modelBuilder.Entity("WebProgramlama.Models.Randevu", b =>
                {
                    b.HasOne("WebProgramlama.Models.Calisan", "Calisan")
                        .WithMany("Randevu")
                        .HasForeignKey("CalisanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebProgramlama.Models.Hizmet", null)
                        .WithMany("Randevu")
                        .HasForeignKey("HizmetId");

                    b.HasOne("WebProgramlama.Models.Musteri", "Musteri")
                        .WithMany("Randevu")
                        .HasForeignKey("MusteriId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Calisan");

                    b.Navigation("Musteri");
                });

            modelBuilder.Entity("WebProgramlama.Models.Calisan", b =>
                {
                    b.Navigation("Randevu");
                });

            modelBuilder.Entity("WebProgramlama.Models.Hizmet", b =>
                {
                    b.Navigation("Randevu");
                });

            modelBuilder.Entity("WebProgramlama.Models.Musteri", b =>
                {
                    b.Navigation("Randevu");
                });
        }
    }
}
