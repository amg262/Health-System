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
    public class EditModelProviderType : PageModel
    {
        private readonly WCTCHealthSystemContext _context;

        public EditModelProviderType(WCTCHealthSystemContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ProviderType ProviderType { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProviderType = await _context.ProviderTypes.FirstOrDefaultAsync(m => m.ProviderTypeId == id);

            if (ProviderType == null)
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

            _context.Attach(ProviderType).State = EntityState.Modified;

            try
            {
                ProviderType.LastModified = DateTime.Now;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProviderTypeExists(ProviderType.ProviderTypeId))
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

        private bool ProviderTypeExists(int id)
        {
            return _context.ProviderTypes.Any(e => e.ProviderTypeId == id);
        }
    }
}
