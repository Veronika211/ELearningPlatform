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
        public DbSet<Kurs> Kursevi { get; set; }
        public DbSet<Test> Testovi { get; set; }

        public static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddConsole();
        });
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(loggerFactory).EnableSensitiveDataLogging().UseSqlServer(@"Server=(localdb)\mssqllocaldb; 
            Database=ELearning;");
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
                   p.HasKey(p => new { p.KorisnikId, p.TestId});
                   p.HasOne(p => p.Korisnik).WithMany(k => k.Testovi).OnDelete(DeleteBehavior.Restrict);
               }
               );

            modelBuilder.Entity<Test>().HasKey(tid => new { tid.TestId, tid.KursId });
            modelBuilder.Entity<Test>().OwnsMany(t => t.Pitanja); 
            modelBuilder.Entity<Kurs>().OwnsMany(k => k.Lekcije);
        }
    }
}
