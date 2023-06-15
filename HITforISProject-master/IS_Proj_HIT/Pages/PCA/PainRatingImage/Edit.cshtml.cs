﻿using System;
using System.Linq;
using System.Threading.Tasks;
using IS_Proj_HIT.Entities;
using IS_Proj_HIT.Entities.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace IS_Proj_HIT
{
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Administrator")]
    public class EditModelPainRatingImage : PageModel
    {
        private readonly WCTCHealthSystemContext _context;

        public EditModelPainRatingImage(WCTCHealthSystemContext context)
        {
            _context = context;
        }

        [BindProperty]
        public PainRatingImage PainRatingImage { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PainRatingImage = await _context.PainRatingImages
                .Include(p => p.PainRating).FirstOrDefaultAsync(m => m.PainRatingId == id);

            if (PainRatingImage == null)
            {
                return NotFound();
            }
           ViewData["PainRatingId"] = new SelectList(_context.PainRatings, "PainRatingId", "Description");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(PainRatingImage).State = EntityState.Modified;

            try
            {
                PainRatingImage.LastModified = DateTime.Now;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PainRatingImageExists(PainRatingImage.PainRatingId))
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

        private bool PainRatingImageExists(int id)
        {
            return _context.PainRatingImages.Any(e => e.PainRatingId == id);
        }
    }
}
