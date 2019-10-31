using Microsoft.EntityFrameworkCore;

namespace ProductsCategories.Models
{
    public class MyContext : DbContext
    {
        // base() calls the parent class' constructor
        // passing the "options" parameter along
        public MyContext(DbContextOptions options) : base(options) { }
        // This property is refers to all the data in the table as a List of Objects.
        // The *name must match the table(case insensitive).
        public DbSet<Product> Products {get;set;}
        public DbSet<Category> Categories {get;set;}
        public DbSet<Association> Associations {get;set;}
        // This Class will likely have DbSet properties for every table in your database
    }
}