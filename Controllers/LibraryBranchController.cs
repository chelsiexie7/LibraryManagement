using System;
using System.Linq;
//LibraryBranchController.cs
using Microsoft.AspNetCore.Mvc;
using LibraryManagement.Models;

namespace LibraryManagement.Controllers{
    public class LibraryBranchController : Controller{
        private readonly AppDbContext _dbContext;
        public LibraryBranchController(AppDbContext dbContext){
        _dbContext = dbContext;
        }
        public IActionResult Index(){
            var libraryBranches = _dbContext.LibraryBranches.ToList();
            return View(libraryBranches);
        }
        public IActionResult Create(){
        return View();
        }
        [HttpPost]
        public IActionResult Create(LibraryBranch libraryBranch){
            if(_dbContext.LibraryBranches.Any(a => a.LibraryBranchId == libraryBranch.LibraryBranchId)){
                ModelState.AddModelError("LibraryBranchId", "Error: Id repeats.");
                return View(libraryBranch);
                }

            libraryBranch.CreatedAt = DateTime.Now;
            _dbContext.LibraryBranches.Add(libraryBranch);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        [Route("LibraryBranch/Edit/{id}")]
        public IActionResult Edit(int id){
            var libraryBranch = _dbContext.LibraryBranches.Find(id);
            if (libraryBranch == null){
                return NotFound();
            }
            return View(libraryBranch);
        }

        [HttpPost]
        [Route("LibraryBranch/Edit/{id}")]
         public IActionResult Edit(int id, LibraryBranch libraryBranch){
            if (!ModelState.IsValid){
                return View(libraryBranch);
            }
            if(id != libraryBranch.LibraryBranchId && _dbContext.LibraryBranches.Any(a => a.LibraryBranchId == libraryBranch.LibraryBranchId)){
                    ModelState.AddModelError("LibraryBranchId", "Error: Id repeats.");
                    return View(libraryBranch);
                }
            var existingLibraryBranch = _dbContext.LibraryBranches.Find(id);
            if (existingLibraryBranch == null){
                return NotFound();
            }
            // 更新实体字段
            existingLibraryBranch.BranchName = libraryBranch.BranchName;

            // 如果 Id 改变了，先删除旧记录，然后添加新记录
            if (existingLibraryBranch.LibraryBranchId != libraryBranch.LibraryBranchId){
                _dbContext.LibraryBranches.Remove(existingLibraryBranch);
                _dbContext.SaveChanges();

                libraryBranch.CreatedAt = existingLibraryBranch.CreatedAt; // 保持创建时间一致
                _dbContext.LibraryBranches.Add(libraryBranch);
            }else{
                existingLibraryBranch.LibraryBranchId = libraryBranch.LibraryBranchId; // 更新ID
            }

            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id){
            var libraryBranch = _dbContext.LibraryBranches.Find(id);
            if (libraryBranch == null){
                return NotFound();
            }
            return View(libraryBranch);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id){
            var libraryBranch = _dbContext.LibraryBranches.Find(id);
            if (libraryBranch == null){
                return NotFound();
            }
            _dbContext.LibraryBranches.Remove(libraryBranch);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}
