using System;
using System.ComponentModel.DataAnnotations;


// Author.cs
namespace LibraryManagement.Models{
public class Author{
    [Key]
    public required int AuthorId { get; set; }
    public required string Name { get; set; }

}
}

