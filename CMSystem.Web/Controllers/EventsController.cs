using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CMSystem.Web.Data;
using CMSystem.Web.Models;
using CMSystem.Web.Service.Files;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace CMSystem.Web.Controllers
{
    public class EventsController : BaseController
    {
        private readonly IFileService _IFileService;

        public EventsController(ApplicationDbContext DB, IFileService IFileService):base(DB)
        {
            _IFileService = IFileService;
        }
        //public IActionResult getall()
        //{
        //    var re = _DB.Users.ToList();
        //    foreach (var item in re)
        //    {
        //        item.IsDelete = false;
        //        _DB.Users.Update(item);
        //        _DB.SaveChanges();
        //    }


        //    return Ok("done");
        //}

        // GET: Events
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _DB.Events.Include(x => x.EvantCategory).Include(x => x.Orgnisation).Where(x=>!x.IsDelete && (x.OrgnisationId == OrganazationId || OrganazationId == null));
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Events/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await _DB.Events
                .Include(x => x.EvantCategory)
                .Include(x => x.Orgnisation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // GET: Events/Create
        public IActionResult Create()
        {
            ViewData["EvantCategoryId"] = new SelectList(_DB.EvantCategorys.Where(x=>!x.IsDelete), "Id", "Name");
            ViewData["OrgnisationId"] = new SelectList(_DB.Orgnisations.Where(x => !x.IsDelete), "Id", "Name");
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Event model,List<IFormFile> Imeges)
        {
            if (ModelState.IsValid)
            {
                model.CreatedBy = UserName;
                _DB.Add(model);
                await _DB.SaveChangesAsync();
                foreach (var img in Imeges)
                {
                    var imeg = await _IFileService.SaveFile(img, "Imeges");
                    var evantImege = new EvantImage();
                    evantImege.EventId = model.Id;
                    evantImege.ImageUrl = imeg;
                    _DB.Add(evantImege);
                }
                await _DB.SaveChangesAsync();
                TempData["msg"] = "Add Item Successfuly !!  ";

                return RedirectToAction(nameof(Index));
            }
            ViewData["EvantCategoryId"] = new SelectList(_DB.EvantCategorys, "Id", "Name", model.EvantCategoryId);
            ViewData["OrgnisationId"] = new SelectList(_DB.Orgnisations, "Id", "Address", model.OrgnisationId);
            return View(model);
        }

        // GET: Events/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await _DB.Events.FindAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            ViewData["EvantCategoryId"] = new SelectList(_DB.EvantCategorys, "Id", "Name", model.EvantCategoryId);
            ViewData["OrgnisationId"] = new SelectList(_DB.Orgnisations, "Id", "Address", model.OrgnisationId);
            return View(model);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Description,EvantDate,EvantCategoryId,OrgnisationId,Id,CreatedBy,UpdatedBy,UpdatedAt,CreatedAt,IsDelete")] Event model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _DB.Update(model);
                    await _DB.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(model.Id))
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
            ViewData["EvantCategoryId"] = new SelectList(_DB.EvantCategorys, "Id", "Name", model.EvantCategoryId);
            ViewData["OrgnisationId"] = new SelectList(_DB.Orgnisations, "Id", "Address", model.OrgnisationId);
            return View(model);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await _DB.Events
                .Include(x => x.EvantCategory)
                .Include(x => x.Orgnisation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (model == null)
            {
                return NotFound();
            }
            model.IsDelete = true;
            _DB.Update(model);
            _DB.SaveChanges();

            TempData["msg"] = "Delete Item Successfuly !!  ";

            return RedirectToAction("Index");
        }

        private bool EventExists(int id)
        {
            return _DB.Events.Any(e => e.Id == id);
        }
    }
}
