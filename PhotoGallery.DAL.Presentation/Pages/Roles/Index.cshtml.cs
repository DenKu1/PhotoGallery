﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PhotoGallery.DAL.EF;
using PhotoGallery.DAL.Entities;

namespace PhotoGallery.DAL.Presentation.Pages.Roles
{
    public class IndexModel : PageModel
    {
        private readonly PhotoGallery.DAL.EF.GalleryContext _context;

        public IndexModel(PhotoGallery.DAL.EF.GalleryContext context)
        {
            _context = context;
        }

        public IList<Role> Role { get;set; }

        public async Task OnGetAsync()
        {
            Role = await _context.Roles.ToListAsync();
        }
    }
}