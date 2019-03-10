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
        public DbSet<Role> Role { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Flat>().ToTable("Flat");
            modelBuilder.Entity<Calendar>().ToTable("Calendar");
            modelBuilder.Entity<Day>().ToTable("Day");
            modelBuilder.Entity<UserFlat>().ToTable("UserFlat");
            modelBuilder.Entity<Role>().ToTable("Role");
            //string adminRoleName = "admin";
            //string userRoleName = "user";

            //string adminEmail = "admin@gmail.com";
            //string adminPassword = "123456";

            
            //Role adminRole = new Role { Id = 1, Name = adminRoleName };
            //Role userRole = new Role { Id = 2, Name = userRoleName };
            //User adminUser = new User { UserID = 1, Login = adminEmail, Password = adminPassword, RoleID = adminRole.Id };

            //modelBuilder.Entity<Role>().HasData(new Role[] { adminRole, userRole });
            //modelBuilder.Entity<User>().HasData(new User[] { adminUser });
            //base.OnModelCreating(modelBuilder);
        }
    }
}
