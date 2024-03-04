using Application.Interfaces;
using Domain.Customers;
using Domain.Members;
using Domain.Orders;
using Domain.Products;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;
public class ApplicationDbContext : DbContext, IApplicationDbContext, IUnitOfWork
{

    public ApplicationDbContext(DbContextOptions options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Member> Members { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderSummary> OrderSummaries { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<LineItem> LineItems { get; set; }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }

}
