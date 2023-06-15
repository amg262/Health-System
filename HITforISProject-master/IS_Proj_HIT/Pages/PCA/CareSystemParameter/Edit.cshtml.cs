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
    public class EditModelCareSystemParameter : PageModel
    {
        private readonly WCTCHealthSystemContext _context;

        public EditModelCareSystemParameter(WCTCHealthSystemContext context)
        {
            _context = context;
        }

        [BindProperty]
        public CareSystemParameter CareSystemParameter { get; set; }

        public async Task<IActionResult> OnGetAsync(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CareSystemParameter = await _context.CareSystemParameters
                .Include(c => c.CareSystemType).FirstOrDefaultAsync(m => m.CareSystemParameterId == id);

            if (CareSystemParameter == null)
            {
                return NotFound();
            }
           ViewData["CareSystemTypeId"] = new SelectList(_context.CareSystemTypes, "CareSystemTypeId", "Name", "NormalLimitsDescription");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(CareSystemParameter).State = EntityState.Modified;

            try
            {
                CareSystemParameter.LastModified = DateTime.Now;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CareSystemParameterExists(CareSystemParameter.CareSystemParameterId))
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

        private bool CareSystemParameterExists(short id)
        {
            return _context.CareSystemParameters.Any(e => e.CareSystemParameterId == id);
        }
    }
}
