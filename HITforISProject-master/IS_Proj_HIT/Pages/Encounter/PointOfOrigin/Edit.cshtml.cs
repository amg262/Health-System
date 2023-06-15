﻿using System;
using System.Linq;
using System.Threading.Tasks;
using IS_Proj_HIT.Entities;
using IS_Proj_HIT.Entities.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace IS_Proj_HIT
{
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Administrator")]

    public class EditModelPointOfOrigin : PageModel
    {
        private readonly WCTCHealthSystemContext _context;

        public EditModelPointOfOrigin(WCTCHealthSystemContext context)
        {
            _context = context;
        }

        [BindProperty]
        public PointOfOrigin PointOfOrigin { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PointOfOrigin = await _context.PointOfOrigins.FirstOrDefaultAsync(m => m.PointOfOriginId == id);

            if (PointOfOrigin == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(PointOfOrigin).State = EntityState.Modified;

            try
            {
                PointOfOrigin.LastModified = DateTime.Now;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PointOfOriginExists(PointOfOrigin.PointOfOriginId))
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

        private bool PointOfOriginExists(int id)
        {
            return _context.PointOfOrigins.Any(e => e.PointOfOriginId == id);
        }
    }
}
