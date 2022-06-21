using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorWeb.Models;

namespace RazorWeb.Pages.Categories
{
    [BindProperties]
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public EditModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public Category category { get; set; }
        public void OnGet(int id)
        {
            category = _db.Categories.Find(id);
        }
        public async Task<IActionResult> OnPost()
        {
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("category.Name", "DisplayOrder cannot exactly match the name .");
            }
            if (ModelState.IsValid)
            {
                 _db.Categories.Update(category);
                await _db.SaveChangesAsync();
                TempData["success"] = "Category Updated successfully";
                return RedirectToPage("Index");
            }

            return Page();
        }
    }
}
