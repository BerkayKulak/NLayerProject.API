using Microsoft.EntityFrameworkCore;
using NLayerProject.Core.Models;
using NLayerProject.Data.Configurations;
using NLayerProject.Data.Seeds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerProject.Data
{
    public class AppDbContext:DbContext
    {
        //optionsla beraber ben sql server mi kullanacam ya da postgre sql mi gibi düşün bunu belirticez.
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        public DbSet<Person> Persons { get; set; }

        // veritabanında tablolar oluşurken, oluşmadan önceki çalışacak metod.
        // yani mesela id'm primary key olacak mı ya da uzunluğu ne olacak gibi şeyleri yazarız.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());

            //id değerlerini belirtiyoruz.
            modelBuilder.ApplyConfiguration(new ProductSeed(new int[] { 1,2}));
            modelBuilder.ApplyConfiguration(new CategorySeed(new int[] {1,2 }));

            modelBuilder.Entity<Person>().HasKey(x => x.Id);
            modelBuilder.Entity<Person>().Property(x => x.Id).UseIdentityColumn();
            modelBuilder.Entity<Person>().Property(x => x.Name).HasMaxLength(100);
            modelBuilder.Entity<Person>().Property(x => x.Surname).HasMaxLength(100);






        }



    }





}
