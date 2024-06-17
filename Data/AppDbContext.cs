using Microsoft.EntityFrameworkCore;

namespace LibraryManagement
{
public class AppDbContext : DbContext
{
public DbSet<Models.Author> Authors { get; set; }
public DbSet<Models.Book> Books { get; set; }
public DbSet<Models.Customer> Customers { get; set; }
public DbSet<Models.LibraryBranch> LibraryBranches { get; set; }



private readonly IConfiguration _configuration;
public AppDbContext(IConfiguration configuration)
{
 _configuration = configuration;
}
protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
 {
 optionsBuilder.UseSqlite(_configuration.GetConnectionString("DefaultConnection"));
 }
 }
}