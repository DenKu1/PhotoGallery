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

namespace PhotoGallery.DAL.Presentation.Pages.Photos
{
    public class EditModel : PageModel
    {
        private readonly PhotoGallery.DAL.EF.GalleryContext _context;

        public EditModel(PhotoGallery.DAL.EF.GalleryContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Photo Photo { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Photo = await _context.Photos
                .Include(p => p.Album).FirstOrDefaultAsync(m => m.Id == id);

            if (Photo == null)
            {
                return NotFound();
            }
           ViewData["AlbumId"] = new SelectList(_context.Albums, "Id", "Id");
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

            _context.Attach(Photo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhotoExists(Photo.Id))
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

        private bool PhotoExists(int id)
        {
            return _context.Photos.Any(e => e.Id == id);
        }
    }
}
