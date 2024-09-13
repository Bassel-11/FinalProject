// FinalProject.Controllers.ProductController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinalProject.Context;
using FinalProject.Models;

namespace FinalProject.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext db;

        public ProductController(AppDbContext context)
        {
            db = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            // Get all products including their associated categories
            var allProducts = db.Products.Include(p => p.Category).ToList();
            return View(allProducts);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var product = db.Products.Include(p => p.Category).FirstOrDefault(p => p.ProductId == id);
            if (product == null)
            {
                return RedirectToAction("Index");
            }
            return View(product);
        }

        [HttpGet]
        public IActionResult Create()
        {
            // Populate data for the dropdown
            ViewBag.Categories = new SelectList(db.Categories, "CategoryId", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid) // Check if the model passes validation rules
            {
                db.Products.Add(product); // Add product to context
                db.SaveChanges(); // Save changes to the database
                return RedirectToAction("Index"); // Redirect to the Index action after successful add
            }

            // Log the errors in ModelState
            foreach (var modelState in ModelState.Values)
            {
                foreach (var error in modelState.Errors)
                {
                    Console.WriteLine(error.ErrorMessage); // Or use any logger
                }
            }

            // If model state is invalid, return to the Create view with the current data
            ViewBag.Categories = new SelectList(db.Categories, "CategoryId", "Name");
            return View(product);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            // Find the product by ID and include its associated category
            var existingProduct = db.Products.Include(p => p.Category).FirstOrDefault(p => p.ProductId == id);
            if (existingProduct == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.Categories = new SelectList(db.Categories, "CategoryId", "Name");
            return View(existingProduct);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            var existingProduct = db.Products.FirstOrDefault(p => p.ProductId == product.ProductId);
            if (existingProduct == null)
            {
                return RedirectToAction("Index");
            }
            existingProduct.Title = product.Title;
            existingProduct.Price = product.Price;
            existingProduct.Description = product.Description;
            existingProduct.Quantity = product.Quantity;
            existingProduct.CategoryId = product.CategoryId;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var productToDelete = db.Products.FirstOrDefault(p => p.ProductId == id);
            if (productToDelete == null)
            {
                return RedirectToAction("Index");
            }
            db.Products.Remove(productToDelete);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
