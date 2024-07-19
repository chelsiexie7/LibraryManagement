using System;
using System.Linq;
// CustomerController.cs
using Microsoft.AspNetCore.Mvc;
using LibraryManagement.Models;
using LibraryManagement.Data;


namespace LibraryManagement.Controllers
{

    public class CustomerController : Controller
    {
        private readonly AppDbContext _dbContext;
        
        public CustomerController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var customers = _dbContext.Customers.ToList();
            return View(customers);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            if(_dbContext.Customers.Any(a => a.CustomerId == customer.CustomerId)){
                ModelState.AddModelError("CustomerId", "Error: Id repeats.");
                return View(customer);
                }
            
            _dbContext.Customers.Add(customer);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("Customer/Edit/{id}")]
        public IActionResult Edit(int id)
        {
            var customer = _dbContext.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        [HttpPost]
        [Route("Customer/Edit/{id}")]
        public IActionResult Edit(int id, Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return View(customer);
            }
            if(id != customer.CustomerId && _dbContext.Customers.Any(a => a.CustomerId == customer.CustomerId)){
                    ModelState.AddModelError("CustomerId", "Error: Id repeats.");
                    return View(customer);
                }
            var existingCustomer = _dbContext.Customers.Find(id);
            if (existingCustomer == null)
            {
                return NotFound();
            }
            // 更新实体字段
            existingCustomer.Name = customer.Name;

            // 如果 CustomerId 改变了，先删除旧记录，然后添加新记录
            if (existingCustomer.CustomerId != customer.CustomerId)
            {
                _dbContext.Customers.Remove(existingCustomer);
                _dbContext.SaveChanges();

                
                _dbContext.Customers.Add(customer);
            }
            else
            {
                existingCustomer.CustomerId = customer.CustomerId; // 更新ID
            }

            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var customer = _dbContext.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var customer = _dbContext.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }
            _dbContext.Customers.Remove(customer);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
