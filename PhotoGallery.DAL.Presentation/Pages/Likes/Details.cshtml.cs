﻿using System;
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
    public class DetailsModel : PageModel
    {
        private readonly PhotoGallery.DAL.EF.GalleryContext _context;

        public DetailsModel(PhotoGallery.DAL.EF.GalleryContext context)
        {
            _context = context;
        }

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
    }
}
