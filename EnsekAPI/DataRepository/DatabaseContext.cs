using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Security.Principal;
using EnsekAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EnsekAPI.DataRepository
{
    /// <summary>
    /// The database context.
    /// </summary>
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>();
            modelBuilder.Entity<MeterReading>();
        }

        public DbSet<MeterReading> MeterReadings { get; set; }
        public DbSet<Account> Accounts { get; set; }
    }
}