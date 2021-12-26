using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CMSystem.Web.Data;
using CMSystem.Web.Models;

namespace CMSystem.Web.Controllers
{
    public class OrgnisationsController : BaseController
    {
     

        public OrgnisationsController(ApplicationDbContext DB):base(DB)
        {
        }

        // GET: Orgnisations
        public async Task<IActionResult> Index()
        {
            return View(await _DB.Orgnisations.Where(x=>!x.IsDelete && (x.Id==OrganazationId || OrganazationId==null)).ToListAsync());
        }

        // GET: Orgnisations/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var orgnisation = await _DB.Orgnisations
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (orgnisation == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(orgnisation);
        //}

        // GET: Orgnisations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Orgnisations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,WorkNature,Address,Email,Telepone,Id,CreatedBy,UpdatedBy,UpdatedAt,CreatedAt,IsDelete")] Orgnisation orgnisation)
        {
            if (ModelState.IsValid)
            {
                _DB.Add(orgnisation);
                await _DB.SaveChangesAsync();
                TempData["msg"] = "ADD Item Successfuly !!  ";

                return RedirectToAction(nameof(Index));
            }
            return View(orgnisation);
        }

        // GET: Orgnisations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orgnisation = await _DB.Orgnisations.FindAsync(id);
            if (orgnisation == null)
            {
                return NotFound();
            }
            return View(orgnisation);
        }

        // POST: Orgnisations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,WorkNature,Address,Email,Telepone,Id,CreatedBy,UpdatedBy,UpdatedAt,CreatedAt,IsDelete")] Orgnisation orgnisation)
        {
            if (id != orgnisation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _DB.Update(orgnisation);
                    await _DB.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrgnisationExists(orgnisation.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["msg"] = "Edit Item Successfuly !!  ";

                return RedirectToAction(nameof(Index));
            }
            return View(orgnisation);
        }

        // GET: Orgnisations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orgnisation = await _DB.Orgnisations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orgnisation == null)
            {
                return NotFound();
            }
            orgnisation.IsDelete = true;
            _DB.Update(orgnisation);
            _DB.SaveChanges();
            TempData["msg"] = "Delete Item Successfuly !!  ";

            return View(orgnisation);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orgnisation = await _DB.Orgnisations.FindAsync(id);
            _DB.Orgnisations.Remove(orgnisation);
            await _DB.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool OrgnisationExists(int id)
        {
            return _DB.Orgnisations.Any(e => e.Id == id);
        }
    }
}
