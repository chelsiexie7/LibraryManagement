using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace LibraryManagement.Data
{
public class AppDbContext : IdentityDbContext<IdentityUser>
{
public DbSet<Models.Author> Authors { get; set; }
public DbSet<Models.Book> Books { get; set; }
public DbSet<Models.Customer> Customers { get; set; }
public DbSet<Models.LibraryBranch> LibraryBranches { get; set; }

//private readonly IConfiguration _configuration;

public AppDbContext(DbContextOptions<AppDbContext> options)
    : base(options)
{
    //_configuration = configuration;
}
protected override void OnModelCreating(ModelBuilder builder)
{
    base.OnModelCreating(builder);
 }
 }
}