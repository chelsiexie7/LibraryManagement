using System;
using System.Linq;
// AuthorController.cs
using Microsoft.AspNetCore.Mvc;
using LibraryManagement.Models;

namespace LibraryManagement.Controllers{
    public class AuthorController : Controller{
        private readonly AppDbContext _dbContext;
        public AuthorController(AppDbContext dbContext){
        _dbContext = dbContext;
        }
        public IActionResult Index(){
            var authors = _dbContext.Authors.ToList();
            return View(authors);
        }
        public IActionResult Create(){
            return View();
        }
        [HttpPost]
        public IActionResult Create(Author author){
            if(_dbContext.Authors.Any(a => a.AuthorId == author.AuthorId)){
                ModelState.AddModelError("AuthorId", "Error: Id repeats");
                return View(author);
            }
            author.CreatedAt = DateTime.Now;
            _dbContext.Authors.Add(author);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        [Route("Author/Edit/{id}")]
        public IActionResult Edit(int id){
            var author = _dbContext.Authors.Find(id);
            if (author == null){
                return NotFound();
            }
            return View(author);
        }

        [HttpPost]
        [Route("Author/Edit/{id}")]
         public IActionResult Edit(int id, Author author){
            if (!ModelState.IsValid){
                return View(author);
            }
            if(id != author.AuthorId && _dbContext.Authors.Any(a => a.AuthorId == author.AuthorId)){
                    ModelState.AddModelError("AuthorId", "Error: Id repeats.");
                    return View(author);
                }
            var existingAuthor = _dbContext.Authors.Find(id);
            if (existingAuthor == null){
                return NotFound();
            }
            // 更新实体字段
            existingAuthor.Name = author.Name;

            // 如果 AuthorId 改变了，先删除旧记录，然后添加新记录
            if (existingAuthor.AuthorId != author.AuthorId){
                _dbContext.Authors.Remove(existingAuthor);
                _dbContext.SaveChanges();

                author.CreatedAt = existingAuthor.CreatedAt; // 保持创建时间一致
                _dbContext.Authors.Add(author);
            }else{
                existingAuthor.AuthorId = author.AuthorId; // 更新ID
            }

            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id){
            var author = _dbContext.Authors.Find(id);
            if (author == null){
                return NotFound();
            }
            return View(author);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id){
            var author = _dbContext.Authors.Find(id);
            if (author == null){
                return NotFound();
            }
            _dbContext.Authors.Remove(author);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}
