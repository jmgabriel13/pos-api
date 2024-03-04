﻿using Domain.Customers;
using Domain.Members;
using Domain.Orders;
using Domain.Products;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces;
public interface IApplicationDbContext
{
    DbSet<Customer> Customers { get; set; }
    DbSet<Member> Members { get; set; }
    DbSet<Order> Orders { get; set; }
    DbSet<OrderSummary> OrderSummaries { get; set; }
    DbSet<Product> Products { get; set; }
    DbSet<LineItem> LineItems { get; set; }
}
