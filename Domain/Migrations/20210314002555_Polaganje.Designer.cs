﻿// <auto-generated />
using System;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Domain.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20210314002555_Polaganje")]
    partial class Polaganje
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Domain.Korisnik", b =>
                {
                    b.Property<int>("KorisnikId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BrPoena")
                        .HasColumnType("int");

                    b.Property<string>("Ime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Prezime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("KorisnikId");

                    b.ToTable("Korisnici");
                });

            modelBuilder.Entity("Domain.Kurs", b =>
                {
                    b.Property<int>("KursId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("NazivKursa")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("KursId");

                    b.ToTable("Kursevi");
                });

            modelBuilder.Entity("Domain.Lekcija", b =>
                {
                    b.Property<int>("LekcijaId")
                        .HasColumnType("int");

                    b.Property<int>("KursId")
                        .HasColumnType("int");

                    b.Property<string>("Naziv")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sadrzaj")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LekcijaId", "KursId");

                    b.HasIndex("KursId");

                    b.ToTable("Lekcije");
                });

            modelBuilder.Entity("Domain.Pitanje", b =>
                {
                    b.Property<int>("PitanjeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Naziv")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TestId")
                        .HasColumnType("int");

                    b.Property<int?>("TestKursId")
                        .HasColumnType("int");

                    b.HasKey("PitanjeId");

                    b.HasIndex("TestId", "TestKursId");

                    b.ToTable("Pitanja");
                });

            modelBuilder.Entity("Domain.Pohadjanje", b =>
                {
                    b.Property<int>("KorisnikId")
                        .HasColumnType("int");

                    b.Property<int>("KursId")
                        .HasColumnType("int");

                    b.Property<int>("Bodovi")
                        .HasColumnType("int");

                    b.HasKey("KorisnikId", "KursId");

                    b.HasIndex("KursId");

                    b.ToTable("Pohadjanja");
                });

            modelBuilder.Entity("Domain.Polaganje", b =>
                {
                    b.Property<int>("KorisnikId")
                        .HasColumnType("int");

                    b.Property<int>("TestId")
                        .HasColumnType("int");

                    b.Property<int>("BodoviT")
                        .HasColumnType("int");

                    b.Property<int>("TestId1")
                        .HasColumnType("int");

                    b.Property<int>("TestKursId")
                        .HasColumnType("int");

                    b.HasKey("KorisnikId", "TestId");

                    b.HasIndex("TestId1", "TestKursId");

                    b.ToTable("Polaganja");
                });

            modelBuilder.Entity("Domain.Test", b =>
                {
                    b.Property<int>("TestId")
                        .HasColumnType("int");

                    b.Property<int>("KursId")
                        .HasColumnType("int");

                    b.Property<string>("Nivo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TestId", "KursId");

                    b.HasIndex("KursId");

                    b.ToTable("Testovi");
                });

            modelBuilder.Entity("Domain.Lekcija", b =>
                {
                    b.HasOne("Domain.Kurs", "Kurs")
                        .WithMany()
                        .HasForeignKey("KursId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Pitanje", b =>
                {
                    b.HasOne("Domain.Test", "Test")
                        .WithMany()
                        .HasForeignKey("TestId", "TestKursId");
                });

            modelBuilder.Entity("Domain.Pohadjanje", b =>
                {
                    b.HasOne("Domain.Korisnik", "Korisnik")
                        .WithMany()
                        .HasForeignKey("KorisnikId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Kurs", "Kurs")
                        .WithMany()
                        .HasForeignKey("KursId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Polaganje", b =>
                {
                    b.HasOne("Domain.Korisnik", "Korisnik")
                        .WithMany()
                        .HasForeignKey("KorisnikId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Test", "Test")
                        .WithMany()
                        .HasForeignKey("TestId1", "TestKursId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Test", b =>
                {
                    b.HasOne("Domain.Kurs", "Kurs")
                        .WithMany()
                        .HasForeignKey("KursId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
