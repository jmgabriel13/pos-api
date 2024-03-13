﻿namespace Domain.Customers;
public class Customer
{
    // entity, unique identifier this case use guid
    public CustomerId Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
}