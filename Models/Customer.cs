using System;
using System.ComponentModel.DataAnnotations;

// Customer.cs
namespace LibraryManagement.Models{
public class Customer{
    [Key]
    public int CustomerId { get; set; }
    public required string Name { get; set; }
    public DateTime CreatedAt{get; set;}
}
}