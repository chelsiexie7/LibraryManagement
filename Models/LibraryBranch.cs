using System;
using System.ComponentModel.DataAnnotations;

// LibraryBranch.cs
namespace LibraryManagement.Models{
public class LibraryBranch{
    [Key]
    public required int LibraryBranchId { get; set; }
    public required string BranchName { get; set; }
    public DateTime CreatedAt{get; set;}
}
}