namespace DL
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class DBContext : DbContext
    {
        
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Contact> Contacts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().Property(c => c.name).HasMaxLength(50);
            modelBuilder.Entity<Customer>().Property(c => c.name).IsRequired();
            modelBuilder.Entity<Customer>().Property(c => c.latitude).HasPrecision(12, 9);
            modelBuilder.Entity<Customer>().Property(c => c.latitude).IsRequired();
            modelBuilder.Entity<Customer>().Property(c => c.longetude).HasPrecision(12, 9);
            modelBuilder.Entity<Customer>().Property(c => c.longetude).IsRequired();

            modelBuilder.Entity<Contact>().Property(c => c.name).HasMaxLength(50);
            modelBuilder.Entity<Contact>().Property(c => c.name).IsRequired();
            modelBuilder.Entity<Contact>().Property(c => c.number).HasMaxLength(50);
            modelBuilder.Entity<Contact>().Property(c => c.email).HasMaxLength(50);
            modelBuilder.Entity<Contact>().Property(c => c.email).IsRequired();
            modelBuilder.Entity<Contact>().Property(c => c.customer_id).IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }
}