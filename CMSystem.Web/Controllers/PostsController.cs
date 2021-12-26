using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CMSystem.Web.Data;
using CMSystem.Web.Models;
using Microsoft.AspNetCore.Http;
using CMSystem.Web.Service.Files;
using Microsoft.AspNetCore.Authorization;
using CMSystem.Web.Service;

namespace CMSystem.Web.Controllers
{
    public class PostsController : BaseController
    {
        private readonly IFileService _IFileService;
        private readonly IEmailSender _emailSender;
        public PostsController(IEmailSender emailSender,ApplicationDbContext DB, IFileService IFileService):base(DB)
        {
            _emailSender = emailSender;
            _IFileService = IFileService;
        }

        // GET: Posts
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _DB.Posts.Include(p => p.Orgnisation).Include(p => p.PostCategory);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Posts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _DB.Posts
                .Include(p => p.Orgnisation)
                .Include(p => p.PostCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // GET: Posts/Create
        public IActionResult Create()
        {
            ViewData["OrgnisationId"] = new SelectList(_DB.Orgnisations, "Id", "Address");
            ViewData["PostCategoryId"] = new SelectList(_DB.PostCategorys, "Id", "Name");
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Post post, IFormFile imgFile, IFormFile attatcmentFile)
        {
            string UserName = User.Identity.Name;
            var user = _DB.Users.SingleOrDefault(x => x.UserName == UserName);
            string users = user.Email;

            if (ModelState.IsValid)
            {
                var imge = await _IFileService.SaveFile(imgFile, "Imeges");
                var attatcment = await _IFileService.SaveFile(attatcmentFile, "Attatcment");
                post.ImageUrl = imge;
                post.AttatcmentUrl = attatcment;

                _DB.Add(post);
                await _DB.SaveChangesAsync();
                await _emailSender.Send("alaa199903@gmail.com", users, "welcome !!");
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrgnisationId"] = new SelectList(_DB.Orgnisations, "Id", "Address", post.OrgnisationId);
            ViewData["PostCategoryId"] = new SelectList(_DB.PostCategorys, "Id", "Name", post.PostCategoryId);
            return View(post);
        }

        // GET: Posts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _DB.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            ViewData["OrgnisationId"] = new SelectList(_DB.Orgnisations, "Id", "Address", post.OrgnisationId);
            ViewData["PostCategoryId"] = new SelectList(_DB.PostCategorys, "Id", "Name", post.PostCategoryId);
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Title,Body,ImageUrl,AttatcmentUrl,PubilishAt,PostCategoryId,OrgnisationId,Id,CreatedBy,UpdatedBy,UpdatedAt,CreatedAt,IsDelete")] Post post)
        {
            if (id != post.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _DB.Update(post);
                    await _DB.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrgnisationId"] = new SelectList(_DB.Orgnisations, "Id", "Address", post.OrgnisationId);
            ViewData["PostCategoryId"] = new SelectList(_DB.PostCategorys, "Id", "Name", post.PostCategoryId);
            return View(post);
        }

        // GET: Posts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _DB.Posts
                .Include(p => p.Orgnisation)
                .Include(p => p.PostCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _DB.Posts.FindAsync(id);
            _DB.Posts.Remove(post);
            await _DB.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(int id)
        {
            return _DB.Posts.Any(e => e.Id == id);
        }
    }
}
