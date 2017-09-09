namespace DL
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class DBContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Contact> Contacts { get; set; }
    }
}