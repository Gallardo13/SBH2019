using FSO.SDD.DbModel;
using Microsoft.EntityFrameworkCore;
using System;

namespace EfConsoleUtil
{
    public class StoreContext : DbContext
    {
        public DbSet<User> Customers { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlite(@"Data Source=FSO.SDD.DataBase.db;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
