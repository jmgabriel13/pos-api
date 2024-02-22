using Domain.Customers;
using Domain.Members;
using Domain.Orders;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces;
public interface IApplicationDbContext
{
    DbSet<Customer> Customers { get; set; }
    DbSet<Member> Members { get; set; }
    DbSet<Order> Orders { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

}
