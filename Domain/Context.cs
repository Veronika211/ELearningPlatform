using Microsoft.EntityFrameworkCore;
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
        public DbSet<Lekcija> Lekcije { get; set; }
        public DbSet<Test> Testovi { get; set; }
        public DbSet<Pitanje> Pitanja { get; set; }
        public DbSet<Pohadjanje> Pohadjanja { get; set; }
        public DbSet<Polaganje> Polaganja { get; set; }
        //public DbSet<Dopuna> Dopune { get; set; }
        //public DbSet<Checkbox> Checkboxes { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb; 
            Database=ELearning;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Pohadjanje>().HasKey(pid => new { pid.KorisnikId, pid.KursId });
            modelBuilder.Entity<Polaganje>().HasKey(pid => new { pid.KorisnikId, pid.TestId });
            modelBuilder.Entity<Pitanje>().HasKey(pid => new { pid.PitanjeId });
            modelBuilder.Entity<Lekcija>().HasKey(lid => new { lid.LekcijaId, lid.KursId });
            modelBuilder.Entity<Test>().HasKey(tid => new { tid.TestId, tid.KursId });
            
            
        }
    }
}
