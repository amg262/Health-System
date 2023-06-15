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
    public class DeleteModelProviderType : PageModel
    {
        private readonly WCTCHealthSystemContext _context;

        public DeleteModelProviderType(WCTCHealthSystemContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ProviderType ProviderType { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            ViewData["RegularMessage"] = "Are you sure you want to delete this?";
            ViewData["ErrorMessage"] = "";

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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProviderType = await _context.ProviderTypes.FindAsync(id);

            if (ProviderType != null)
            {
                // See if any Physician records exist with this type 
                bool usingExists = _context.Physicians.Any(e => e.ProviderTypeId== ProviderType.ProviderTypeId);
                if (usingExists)
                {
                    Console.WriteLine("Physician records exist using this record.");
                    ViewData["RegularMessage"] = "";
                    ViewData["ErrorMessage"] = "Physician records exist using this record. Delete not available.";
                    return Page();
                }

                _context.ProviderTypes.Remove(ProviderType);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
