global using Microsoft.EntityFrameworkCore;
using System;

namespace Diplom_Project
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Bill> Bill => Set<Bill>();
        public DbSet<Member> Members => Set<Member>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Initial Catalog=billdb;Trusted_Connection=true;Integrated Security=true;TrustServerCertificate=true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Member>()
                                         .HasOne(m => m.Bill)
                                         .WithMany(b => b.Members)
                                         .HasForeignKey(m => m.BillId)
                                         .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
