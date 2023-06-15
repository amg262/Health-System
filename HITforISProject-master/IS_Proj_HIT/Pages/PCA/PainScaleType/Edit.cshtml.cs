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
    public class EditModelPainScaleType : PageModel
    {
        private readonly WCTCHealthSystemContext _context;

        public EditModelPainScaleType(WCTCHealthSystemContext context)
        {
            _context = context;
        }

        [BindProperty]
        public PainScaleType PainScaleType { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PainScaleType = await _context.PainScaleTypes.FirstOrDefaultAsync(m => m.PainScaleTypeId == id);

            if (PainScaleType == null)
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

            _context.Attach(PainScaleType).State = EntityState.Modified;

            try
            {
                PainScaleType.LastModified = DateTime.Now;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PainScaleTypeExists(PainScaleType.PainScaleTypeId))
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

        private bool PainScaleTypeExists(int id)
        {
            return _context.PainScaleTypes.Any(e => e.PainScaleTypeId == id);
        }
    }
}
