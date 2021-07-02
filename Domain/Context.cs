using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class Context : DbContext
    {

        public DbSet<Korisnik> Korisnici { get; set; }
        public DbSet<Administrator> Administratori { get; set; }
        public DbSet<Kurs> Kursevi { get; set; }
        public DbSet<Test> Testovi { get; set; }
        public DbSet<Pitanje> Pitanja { get; set; }
        public DbSet<Dopuna> Dopune { get; set; }
        public DbSet<Pohadjanje> Pohadjanje { get; set; }
        public DbSet<Polaganje> Polaganje { get; set; }
        public DbSet<Checkbox> Checkboxes{ get; set; }
        public static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddConsole();
        });
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(loggerFactory).EnableSensitiveDataLogging().UseSqlServer(@"Server=(localdb)\mssqllocaldb; 
            Database=ELearning2;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Pohadjanje>(
                p =>
                {
                    p.HasKey(p => new { p.KorisnikId, p.KursId });
                    p.HasOne(p => p.Korisnik).WithMany(k => k.Kursevi).OnDelete(DeleteBehavior.Restrict);
                }
                );
            modelBuilder.Entity<Polaganje>(
               p =>
               {

                   p.HasOne(p => p.Korisnik).WithMany(k => k.Testovi).OnDelete(DeleteBehavior.Restrict);
               }
               );

            modelBuilder.Entity<Test>().HasKey(tid => new { tid.TestId });
            modelBuilder.Entity<Test>().HasOne(t => t.Kurs);
            modelBuilder.Entity<Test>().HasMany(t => t.Pitanja); //veza jedan vise
            modelBuilder.Entity<Kurs>().OwnsMany(k => k.Lekcije);
            modelBuilder.Entity<Korisnik>().HasData(new { KorisnikId = 1, Ime = "Aleksandra", Prezime = "Markovic", Username = "am", Password = "am", BrPoena = 0 },
                                                    new { KorisnikId = 2, Ime = "Veronika", Prezime = "Markovic", Username = "vm", Password = "vm", BrPoena = 0 });
            modelBuilder.Entity<Kurs>().HasData(new { KursId = 1, NazivKursa = "Napredne .NET tehnologije" },
                                                new { KursId = 2, NazivKursa = "Napredna Java" },
                                                new { KursId = 3, NazivKursa = "Projektovanje informacionih sistema" },
                                                new { KursId = 4, NazivKursa = "Osnove teorije igara" });
            modelBuilder.Entity<Kurs>().OwnsMany(l => l.Lekcije).HasData(new Lekcija { LekcijaId = 1, KursId = 1, Naziv = "Tipovi objekata" },
                                                   new Lekcija { LekcijaId = 2, KursId = 1, Naziv = "Konstruktori" },
                                                   new Lekcija { LekcijaId = 3, KursId = 2, Naziv = "Osnove OOP" },
                                                   new Lekcija { LekcijaId = 4, KursId = 3, Naziv = "Tronivojska arhitektura" },
                                                   new Lekcija { LekcijaId = 5, KursId = 3, Naziv = "Implementacija korisnickog interfejsa" },
                                                   new Lekcija { LekcijaId = 6, KursId = 4, Naziv = "Mesovite igre" },
                                                   new Lekcija { LekcijaId = 7, KursId = 4, Naziv = "Dilema zatvorenika" },
                                                   new Lekcija { LekcijaId = 8, KursId = 4, Naziv = "Igre koordinacije" },
                                                   new Lekcija { LekcijaId = 9, KursId = 4, Naziv = "Igre antikoordinacije" });

            modelBuilder.Entity<Test>().HasData(new { TestId = 1, KursId = 1, Nivo = "I" },
                                                new { TestId = 2, KursId = 1, Nivo = "II" },
                                                new { TestId = 3, KursId = 1, Nivo = "III" },
                                                new { TestId = 4, KursId = 2, Nivo = "I" },
                                                new { TestId = 5, KursId = 2, Nivo = "II" },
                                                new { TestId = 6, KursId = 2, Nivo = "III" },
                                                new { TestId = 7, KursId = 3, Nivo = "I" },
                                                new { TestId = 8, KursId = 3, Nivo = "II" },
                                                new { TestId = 9, KursId = 3, Nivo = "III" },
                                                new { TestId = 10, KursId = 4, Nivo = "I" },
                                                new { TestId = 11, KursId = 4, Nivo = "II" },
                                                new { TestId = 12, KursId = 4, Nivo = "III" });

            modelBuilder.Entity<Checkbox>().HasData(new
            {
                PitanjeId = 1,
                Naziv = "Koja su cetiri osnovna principa OOP?",
                Discriminator = "Checkbox",
                TestId = 4,
                TestKursId = 2,
                TacanOdgovor = "Nasledjivanje, modularnost, polumorfizam, enkapsulacija",
                TacanBodovi = 5,
                NetacanOdgovor1 = "Nasledjivanje, modularnost, apstrakcija, enkapsulacija",
                NetacanOdgovor2 = "Nasledjivanje, modularnost, asocijacija, enkapsulacija",
                NetacanOdgovor3 = "Klasifikacija, modularnost, apstrakcija, enkapsulacija"
            });

            modelBuilder.Entity<Checkbox>().HasData(new
            {
                PitanjeId = 2,
                Naziv = "Arhitektura informacionih sistema je?",
                Discriminator = "Checkbox",
                TestId = 7,
                TestKursId = 3,
                TacanOdgovor = "Tronivojska",
                TacanBodovi = 5,
                NetacanOdgovor1 = "Dvonivojska",
                NetacanOdgovor2 = "Sestonivojska",
                NetacanOdgovor3 = "Petonivojska"
            });

            modelBuilder.Entity<Checkbox>().HasData(new
            {
                PitanjeId = 3,
                Naziv = "Navedite validator u ASP.NET-u koji se koristi kako bismo se uverili da se vrednosti u dve razlicite kontrole podudaraju",
                Discriminator = "Dopuna",
                TestId = 2,
                TestKursId = 1,
                TacanOdgovor = "Compare Validator control",
                TacanBodovi = 10
            });

            modelBuilder.Entity<Checkbox>().HasData(new
            {
                PitanjeId = 4,
                Naziv = "Navedite tri vrste caching-a u ASP.NET-u",
                Discriminator = "Checkbox",
                TestId = 3,
                TestKursId = 1,
                TacanOdgovor = "Output Caching,Fragment Caching,Data Caching",
                TacanBodovi = 15,
                NetacanOdgovor1 = "Output Caching,In Caching,Data Caching",
                NetacanOdgovor2 = "Output Caching,Fragment Caching,Type Caching",
                NetacanOdgovor3 = "In Caching,Fragment Caching,Data Caching"
            });
            modelBuilder.Entity<Administrator>().HasData(new { AdministratorId = 1, Ime = "Tatjana", Prezime = "Stojanovic", Username = "ts", Password = "ts" });
        }
    }
}
