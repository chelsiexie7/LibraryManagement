using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using LibraryManagement.Models;
using LibraryManagement.ViewModels;
using Microsoft.EntityFrameworkCore;
using LibraryManagement.Data;


namespace LibraryManagement.Controllers
{
       public class BookController : Controller
    {
        private readonly AppDbContext _dbContext;

        public BookController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index(){
            var books = _dbContext.Books.ToList();
            var authors = _dbContext.Authors.ToList();
            var branches = _dbContext.LibraryBranches.ToList();
            var bookViewModels = books.Select(b => new BookViewModel{
                BookId = b.BookId,
                Title = b.Title,
                AuthorId = b.AuthorId,
                Name = authors.FirstOrDefault(a => a.AuthorId == b.AuthorId)?.Name,
                LibraryBranchId = b.LibraryBranchId,
                BranchName = branches.FirstOrDefault(c => c.LibraryBranchId == b.LibraryBranchId)?.BranchName
            }).ToList();

            return View(bookViewModels);
            }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create(Book book)
        {
            if (ModelState.IsValid)
            {
                if(_dbContext.Books.Any(a => a.BookId == book.BookId)){
                ModelState.AddModelError("BookId", "Error: Id repeats.");
                return View(book);
                }
                
                _dbContext.Books.Add(book);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(book);
        }

        [HttpGet]
        [Route("Book/Edit/{id}")]

        public IActionResult Edit(int id){
            var book = _dbContext.Books.Find(id);
            if (book == null){
                return NotFound();
            }
            
            var authors = _dbContext.Authors.ToList();
                var branches = _dbContext.LibraryBranches.ToList();
                var bookViewModels = new BookViewModel
                {
                    BookId = book.BookId,
                    Title = book.Title,
                    AuthorId = book.AuthorId,
                    Name = authors.FirstOrDefault(a => a.AuthorId == book.AuthorId)?.Name,
                    LibraryBranchId = book.LibraryBranchId,
                    BranchName = branches.FirstOrDefault(c => c.LibraryBranchId == book.LibraryBranchId)?.BranchName
                };
            
            return View(book);
        }

        [HttpPost]
        [Route("Book/Edit/{id}")]
        [ValidateAntiForgeryToken]

        public IActionResult Edit(int id, Book book)
        {
            if (!ModelState.IsValid)
            {
                return View(book);
            }
            if(id != book.BookId && _dbContext.Books.Any(a => a.BookId == book.BookId)){
                    ModelState.AddModelError("BookId", "Error: Id repeats.");
                    return View(book);
                }
            var existingBook = _dbContext.Books.Find(id);
            if (existingBook == null)
            {
                return NotFound();
            }

            // 更新实体字段
            existingBook.Title = book.Title;
            existingBook.AuthorId = book.AuthorId;
            existingBook.LibraryBranchId = book.LibraryBranchId;

            // 如果 Id 改变了，先删除旧记录，然后添加新记录
            if (existingBook.BookId != book.BookId)
            {
                _dbContext.Books.Remove(existingBook);
                _dbContext.SaveChanges();

                
                _dbContext.Books.Add(book);
            }
            else
            {
                existingBook.BookId = book.BookId; // 更新ID
            }

            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var book = _dbContext.Books.Find(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
 
        public IActionResult DeleteConfirmed(int id)
        {
            var book = _dbContext.Books.Find(id);
            if (book == null)
            {
                return NotFound();
            }
            _dbContext.Books.Remove(book);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
