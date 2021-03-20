using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PhotoGallery.DAL.EF;
using PhotoGallery.DAL.Entities;

namespace PhotoGallery.DAL.Presentation.Pages.Likes
{
    public class EditModel : PageModel
    {
        private readonly PhotoGallery.DAL.EF.GalleryContext _context;

        public EditModel(PhotoGallery.DAL.EF.GalleryContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Like Like { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Like = await _context.Likes
                .Include(l => l.Photo)
                .Include(l => l.User).FirstOrDefaultAsync(m => m.Id == id);

            if (Like == null)
            {
                return NotFound();
            }
           ViewData["PhotoId"] = new SelectList(_context.Photos, "Id", "Id");
           ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Like).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LikeExists(Like.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool LikeExists(int id)
        {
            return _context.Likes.Any(e => e.Id == id);
        }
    }
}
