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
    public class EditModelPainRating : PageModel
    {
        private readonly WCTCHealthSystemContext _context;

        public EditModelPainRating(WCTCHealthSystemContext context)
        {
            _context = context;
        }

        [BindProperty]
        public PainRating PainRating { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PainRating = await _context.PainRatings
                .Include(p => p.PainParameter).FirstOrDefaultAsync(m => m.PainRatingId == id);

            if (PainRating == null)
            {
                return NotFound();
            }
           ViewData["PainParameterId"] = new SelectList(_context.PainParameters, "PainParameterId", "Description");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(PainRating).State = EntityState.Modified;

            try
            {
                PainRating.LastModified = DateTime.Now;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PainRatingExists(PainRating.PainRatingId))
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

        private bool PainRatingExists(int id)
        {
            return _context.PainRatings.Any(e => e.PainRatingId == id);
        }
    }
}
