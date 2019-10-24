using FSO.SDD.DbModel;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System;

namespace FSO.SDD.DataBaseEfStore
{
    public class StoreContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        private string DbPath { get; set; }
        public StoreContext(string dbPath)
        {
            DbPath = dbPath;
        }

        public StoreContext() : this(null){}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var source = $"Data Source={DbPath ?? @"..\FSO.SDD.DataBase.db"}";
            optionsBuilder.UseSqlite(source);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");
        }
    }
}
