// <auto-generated />
using System;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Domain.Migrations
{
    [DbContext(typeof(Context))]
    partial class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Domain.Administrator", b =>
                {
                    b.Property<int>("AdministratorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Ime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Prezime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AdministratorId");

                    b.ToTable("Administratori");

                    b.HasData(
                        new
                        {
                            AdministratorId = 1,
                            Ime = "Tatjana",
                            Password = "ts",
                            Prezime = "Stojanovic",
                            Username = "ts"
                        });
                });

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

                    b.HasData(
                        new
                        {
                            KorisnikId = 1,
                            BrPoena = 0,
                            Ime = "Aleksandra",
                            Password = "am",
                            Prezime = "Markovic",
                            Username = "am"
                        },
                        new
                        {
                            KorisnikId = 2,
                            BrPoena = 0,
                            Ime = "Veronika",
                            Password = "vm",
                            Prezime = "Markovic",
                            Username = "vm"
                        });
                });

            modelBuilder.Entity("Domain.Kurs", b =>
                {
                    b.Property<int>("KursId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("NazivKursa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("KursId");

                    b.ToTable("Kursevi");

                    b.HasData(
                        new
                        {
                            KursId = 1,
                            NazivKursa = "Napredne .NET tehnologije"
                        },
                        new
                        {
                            KursId = 2,
                            NazivKursa = "Napredna Java"
                        },
                        new
                        {
                            KursId = 3,
                            NazivKursa = "Projektovanje informacionih sistema"
                        },
                        new
                        {
                            KursId = 4,
                            NazivKursa = "Osnove teorije igara"
                        });
                });

            modelBuilder.Entity("Domain.Pitanje", b =>
                {
                    b.Property<int>("PitanjeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Naziv")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TestId")
                        .HasColumnType("int");

                    b.HasKey("PitanjeId");

                    b.HasIndex("TestId");

                    b.ToTable("Pitanja");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Pitanje");
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

                    b.ToTable("Pohadjanje");
                });

            modelBuilder.Entity("Domain.Polaganje", b =>
                {
                    b.Property<int>("PolaganjeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BodoviT")
                        .HasColumnType("int");

                    b.Property<int>("KorisnikId")
                        .HasColumnType("int");

                    b.Property<int?>("TestId")
                        .HasColumnType("int");

                    b.HasKey("PolaganjeId");

                    b.HasIndex("KorisnikId");

                    b.HasIndex("TestId");

                    b.ToTable("Polaganje");
                });

            modelBuilder.Entity("Domain.Test", b =>
                {
                    b.Property<int>("TestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("KursId")
                        .HasColumnType("int");

                    b.Property<string>("Nivo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TestId");

                    b.HasIndex("KursId");

                    b.ToTable("Testovi");

                    b.HasData(
                        new
                        {
                            TestId = 1,
                            KursId = 1,
                            Nivo = "I"
                        },
                        new
                        {
                            TestId = 2,
                            KursId = 1,
                            Nivo = "II"
                        },
                        new
                        {
                            TestId = 3,
                            KursId = 1,
                            Nivo = "III"
                        },
                        new
                        {
                            TestId = 4,
                            KursId = 2,
                            Nivo = "I"
                        },
                        new
                        {
                            TestId = 5,
                            KursId = 2,
                            Nivo = "II"
                        },
                        new
                        {
                            TestId = 6,
                            KursId = 2,
                            Nivo = "III"
                        },
                        new
                        {
                            TestId = 7,
                            KursId = 3,
                            Nivo = "I"
                        },
                        new
                        {
                            TestId = 8,
                            KursId = 3,
                            Nivo = "II"
                        },
                        new
                        {
                            TestId = 9,
                            KursId = 3,
                            Nivo = "III"
                        },
                        new
                        {
                            TestId = 10,
                            KursId = 4,
                            Nivo = "I"
                        },
                        new
                        {
                            TestId = 11,
                            KursId = 4,
                            Nivo = "II"
                        },
                        new
                        {
                            TestId = 12,
                            KursId = 4,
                            Nivo = "III"
                        });
                });

            modelBuilder.Entity("Domain.Checkbox", b =>
                {
                    b.HasBaseType("Domain.Pitanje");

                    b.Property<string>("NetacanOdgovor1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NetacanOdgovor2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NetacanOdgovor3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TacanBodovi")
                        .HasColumnType("int");

                    b.Property<string>("TacanOdgovor")
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("Checkbox");

                    b.HasData(
                        new
                        {
                            PitanjeId = 1,
                            Discriminator = "Checkbox",
                            Naziv = "Koja su cetiri osnovna principa OOP?",
                            TestId = 4,
                            NetacanOdgovor1 = "Nasledjivanje, modularnost, apstrakcija, enkapsulacija",
                            NetacanOdgovor2 = "Nasledjivanje, modularnost, asocijacija, enkapsulacija",
                            NetacanOdgovor3 = "Klasifikacija, modularnost, apstrakcija, enkapsulacija",
                            TacanBodovi = 5,
                            TacanOdgovor = "Nasledjivanje, modularnost, polumorfizam, enkapsulacija"
                        },
                        new
                        {
                            PitanjeId = 2,
                            Discriminator = "Checkbox",
                            Naziv = "Arhitektura informacionih sistema je?",
                            TestId = 7,
                            NetacanOdgovor1 = "Dvonivojska",
                            NetacanOdgovor2 = "Sestonivojska",
                            NetacanOdgovor3 = "Petonivojska",
                            TacanBodovi = 5,
                            TacanOdgovor = "Tronivojska"
                        },
                        new
                        {
                            PitanjeId = 3,
                            Discriminator = "Dopuna",
                            Naziv = "Navedite validator u ASP.NET-u koji se koristi kako bismo se uverili da se vrednosti u dve razlicite kontrole podudaraju",
                            TestId = 2,
                            TacanBodovi = 10,
                            TacanOdgovor = "Compare Validator control"
                        },
                        new
                        {
                            PitanjeId = 4,
                            Discriminator = "Checkbox",
                            Naziv = "Navedite tri vrste caching-a u ASP.NET-u",
                            TestId = 3,
                            NetacanOdgovor1 = "Output Caching,In Caching,Data Caching",
                            NetacanOdgovor2 = "Output Caching,Fragment Caching,Type Caching",
                            NetacanOdgovor3 = "In Caching,Fragment Caching,Data Caching",
                            TacanBodovi = 15,
                            TacanOdgovor = "Output Caching,Fragment Caching,Data Caching"
                        });
                });

            modelBuilder.Entity("Domain.Dopuna", b =>
                {
                    b.HasBaseType("Domain.Pitanje");

                    b.Property<int>("TacanBodovi")
                        .HasColumnName("Dopuna_TacanBodovi")
                        .HasColumnType("int");

                    b.Property<string>("TacanOdgovor")
                        .HasColumnName("Dopuna_TacanOdgovor")
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("Dopuna");
                });

            modelBuilder.Entity("Domain.Kurs", b =>
                {
                    b.OwnsMany("Domain.Lekcija", "Lekcije", b1 =>
                        {
                            b1.Property<int>("KursId")
                                .HasColumnType("int");

                            b1.Property<int>("LekcijaId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("Naziv")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Sadrzaj")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("KursId", "LekcijaId");

                            b1.ToTable("Lekcija");

                            b1.WithOwner("Kurs")
                                .HasForeignKey("KursId");

                            b1.HasData(
                                new
                                {
                                    KursId = 1,
                                    LekcijaId = 1,
                                    Naziv = "Tipovi objekata"
                                },
                                new
                                {
                                    KursId = 1,
                                    LekcijaId = 2,
                                    Naziv = "Konstruktori"
                                },
                                new
                                {
                                    KursId = 2,
                                    LekcijaId = 3,
                                    Naziv = "Osnove OOP"
                                },
                                new
                                {
                                    KursId = 3,
                                    LekcijaId = 4,
                                    Naziv = "Tronivojska arhitektura"
                                },
                                new
                                {
                                    KursId = 3,
                                    LekcijaId = 5,
                                    Naziv = "Implementacija korisnickog interfejsa"
                                },
                                new
                                {
                                    KursId = 4,
                                    LekcijaId = 6,
                                    Naziv = "Mesovite igre"
                                },
                                new
                                {
                                    KursId = 4,
                                    LekcijaId = 7,
                                    Naziv = "Dilema zatvorenika"
                                },
                                new
                                {
                                    KursId = 4,
                                    LekcijaId = 8,
                                    Naziv = "Igre koordinacije"
                                },
                                new
                                {
                                    KursId = 4,
                                    LekcijaId = 9,
                                    Naziv = "Igre antikoordinacije"
                                });
                        });
                });

            modelBuilder.Entity("Domain.Pitanje", b =>
                {
                    b.HasOne("Domain.Test", null)
                        .WithMany("Pitanja")
                        .HasForeignKey("TestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Pohadjanje", b =>
                {
                    b.HasOne("Domain.Korisnik", "Korisnik")
                        .WithMany("Kursevi")
                        .HasForeignKey("KorisnikId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Domain.Kurs", "Kurs")
                        .WithMany("Korisnici")
                        .HasForeignKey("KursId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Polaganje", b =>
                {
                    b.HasOne("Domain.Korisnik", "Korisnik")
                        .WithMany("Testovi")
                        .HasForeignKey("KorisnikId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Domain.Test", "Test")
                        .WithMany("Korisnici")
                        .HasForeignKey("TestId");
                });

            modelBuilder.Entity("Domain.Test", b =>
                {
                    b.HasOne("Domain.Kurs", "Kurs")
                        .WithMany("Testovi")
                        .HasForeignKey("KursId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
