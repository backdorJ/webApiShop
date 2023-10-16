using Microsoft.EntityFrameworkCore;
using WebApiShop.Models;

namespace WebApiShop.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
        : base(options) { }
    
    public DbSet<Employee?> Employees { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<EmployeeProduct> EmployeeProducts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<EmployeeProduct>().HasKey(x => new { x.EmployeeId, x.ProductId });

        modelBuilder.Entity<EmployeeProduct>()
            .HasOne(x => x.Employees)
            .WithMany(employee => employee.ProductsList)
            .HasForeignKey(x => x.EmployeeId);

        modelBuilder.Entity<EmployeeProduct>()
            .HasOne(x => x.Products)
            .WithMany(product => product.EmployeesList)
            .HasForeignKey(x => x.ProductId);
    }
}