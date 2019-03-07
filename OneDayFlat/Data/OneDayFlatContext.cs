using Microsoft.EntityFrameworkCore;
using OneDayFlat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OneDayFlat.Data
{
    public class OneDayFlatContext:DbContext
    {
        public OneDayFlatContext(DbContextOptions<OneDayFlatContext>options):base(options)
        {

        }
        public DbSet<User> User { get; set; }
        public DbSet<Flat> Flat { get; set; }
        public DbSet<Calendar> Calendar { get; set; }
        public DbSet<Day> Day { get; set; }
        public DbSet<UserFlat> UserFlat { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Flat>().ToTable("Flat");
            modelBuilder.Entity<Calendar>().ToTable("Calendar");
            modelBuilder.Entity<Day>().ToTable("Day");
            modelBuilder.Entity<UserFlat>().ToTable("UserFlat");

        }
    }
}
