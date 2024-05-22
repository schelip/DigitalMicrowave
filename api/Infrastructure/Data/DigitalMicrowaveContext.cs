using DigitalMicrowave.Business.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace DigitalMicrowave.Infrastructure.Data
{
    public class DigitalMicrowaveContext : DbContext
    {
        public DbSet<HeatingProcedure> HeatingProcedures { get; set; }

        public DigitalMicrowaveContext() : base("name=DigitalMicrowaveContext")
        {
            Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}