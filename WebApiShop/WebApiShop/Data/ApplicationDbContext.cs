using Microsoft.EntityFrameworkCore;
using WebApiShop.Models;

namespace WebApiShop.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
        : base(options) { }
    
    public DbSet<Employee?> Employees { get; set; }
    public DbSet<Product> Products { get; set; }
}