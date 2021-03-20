using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PhotoGallery.DAL.EF;
using PhotoGallery.DAL.Entities;

namespace PhotoGallery.DAL.Presentation.Pages.Photos
{
    public class DeleteModel : PageModel
    {
        private readonly PhotoGallery.DAL.EF.GalleryContext _context;

        public DeleteModel(PhotoGallery.DAL.EF.GalleryContext context)
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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Photo = await _context.Photos.FindAsync(id);

            if (Photo != null)
            {
                _context.Photos.Remove(Photo);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
