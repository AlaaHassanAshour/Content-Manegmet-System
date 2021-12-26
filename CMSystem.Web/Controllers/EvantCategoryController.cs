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
    [Authorize]

    public class EvantCategoryController : BaseController
    {

        public EvantCategoryController(ApplicationDbContext DB):base(DB)
        {
        }
        public IActionResult Index()
        {
           
            return View(_DB.EvantCategorys.Where(x=>!x.IsDelete).ToList());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(EvantCategory evantCategory)
        {
            _DB.Add(evantCategory);
            _DB.SaveChanges();
            TempData["msg"] = "Add Item Successfuly !!  ";
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var evantCategory = _DB.EvantCategorys.SingleOrDefault(x => x.Id == id && !x.IsDelete);
            if (evantCategory == null)
            {
                return NotFound();
            }
            return View(evantCategory);
        }
        [HttpPost]
        public IActionResult Edit(EvantCategory evantCategory)
        {
            if (ModelState.IsValid)
            {
                _DB.Update(evantCategory);
                _DB.SaveChanges();
            }
            TempData["msg"] = "Edit Item Successfuly !!  ";

            return RedirectToAction("Index");
        }
     
        public IActionResult Delete(int id)
        {
            var evantCategory = _DB.EvantCategorys.SingleOrDefault(x => x.Id == id && !x.IsDelete);
            if (evantCategory == null)
            {
                return NotFound();
            }
            evantCategory.IsDelete = true;
            _DB.Update(evantCategory);
            _DB.SaveChanges();
            TempData["msg"] = "Delete Item Successfuly !!  ";

            return View(evantCategory);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var EvantCategorys = await _DB.EvantCategorys.FindAsync(id);
            _DB.EvantCategorys.Remove(EvantCategorys);
            await _DB.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
