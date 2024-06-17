namespace LibraryManagement.ViewModels
{
public class BookViewModel
    {
        public int BookId { get; set; }
        public required string Title { get; set; }
        public int AuthorId { get; set; }
        public string? Name { get; set; }
        public int LibraryBranchId { get; set; }
        public string? BranchName { get; set; }
    }

}