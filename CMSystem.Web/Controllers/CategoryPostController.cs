using CMSystem.Web.Data;
using CMSystem.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMSystem.Web.Controllers
{
    public class CategoryPostController : BaseController
    {

        public CategoryPostController(ApplicationDbContext DB):base(DB)
        {
            
        }
        public IActionResult Index()
        {
           
            return View(_DB.PostCategorys.Where(x=>!x.IsDelete).ToList());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(PostCategory postCategory)
        {
            _DB.Add(postCategory);
            _DB.SaveChanges();
            TempData["msg"] = "Add Item Successfuly !!  ";

            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var postCategory = _DB.PostCategorys.SingleOrDefault(x => x.Id == id && !x.IsDelete);
            if (postCategory == null)
            {
                return NotFound();
            }
            return View(postCategory);
        }
        [HttpPost]
        public IActionResult Edit(PostCategory postCategory)
        {
            if (ModelState.IsValid)
            {
                _DB.Update(postCategory);
                _DB.SaveChanges();
            }
            TempData["msg"] = "Edit Item Successfuly !!  ";

            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var postCategory = _DB.PostCategorys.SingleOrDefault(x => x.Id == id && !x.IsDelete);
            if (postCategory==null)
            {
                return NotFound();
            }
            postCategory.IsDelete = true;
            _DB.Update(postCategory);
            _DB.SaveChanges();
            TempData["msg"] = "Delete Item Successfuly !!  ";

            return View(postCategory);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var PostCategorys = await _DB.PostCategorys.FindAsync(id);
            _DB.PostCategorys.Remove(PostCategorys);
            await _DB.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
