using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PhotoGallery.DAL.EF;
using PhotoGallery.DAL.Entities;

namespace PhotoGallery.DAL.Presentation.Pages.Likes
{
    public class DeleteModel : PageModel
    {
        private readonly PhotoGallery.DAL.EF.GalleryContext _context;

        public DeleteModel(PhotoGallery.DAL.EF.GalleryContext context)
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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Like = await _context.Likes.FindAsync(id);

            if (Like != null)
            {
                _context.Likes.Remove(Like);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
