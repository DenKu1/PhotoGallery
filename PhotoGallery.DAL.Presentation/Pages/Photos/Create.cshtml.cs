using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PhotoGallery.DAL.EF;
using PhotoGallery.DAL.Entities;

namespace PhotoGallery.DAL.Presentation.Pages.Photos
{
    public class CreateModel : PageModel
    {
        private readonly PhotoGallery.DAL.EF.GalleryContext _context;

        public CreateModel(PhotoGallery.DAL.EF.GalleryContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["AlbumId"] = new SelectList(_context.Albums, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Photo Photo { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Photos.Add(Photo);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
