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
    public class EditModelEncounterType : PageModel
    {
        private readonly WCTCHealthSystemContext _context;

        public EditModelEncounterType(WCTCHealthSystemContext context)
        {
            _context = context;
        }

        [BindProperty]
        public EncounterType EncounterType { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            EncounterType = await _context.EncounterTypes.FirstOrDefaultAsync(m => m.EncounterTypeId == id);

            if (EncounterType == null)
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

            _context.Attach(EncounterType).State = EntityState.Modified;

            try
            {
                EncounterType.LastModified = DateTime.Now;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EncounterTypeExists(EncounterType.EncounterTypeId))
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

        private bool EncounterTypeExists(int id)
        {
            return _context.EncounterTypes.Any(e => e.EncounterTypeId == id);
        }
    }
}
