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
    public class EditModelPainParameter : PageModel
    {
        private readonly WCTCHealthSystemContext _context;

        public EditModelPainParameter(WCTCHealthSystemContext context)
        {
            _context = context;
        }

        [BindProperty]
        public PainParameter PainParameter { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PainParameter = await _context.PainParameters
                .Include(p => p.PainScaleType).FirstOrDefaultAsync(m => m.PainParameterId == id);

            if (PainParameter == null)
            {
                return NotFound();
            }
           ViewData["PainScaleTypeId"] = new SelectList(_context.PainScaleTypes, "PainScaleTypeId", "PainScaleTypeName");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(PainParameter).State = EntityState.Modified;

            try
            {
                PainParameter.LastModified = DateTime.Now;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PainParameterExists(PainParameter.PainParameterId))
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

        private bool PainParameterExists(int id)
        {
            return _context.PainParameters.Any(e => e.PainParameterId == id);
        }
    }
}
