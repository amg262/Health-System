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

    public class DeleteModelTempRouteType : PageModel
    {
        private readonly WCTCHealthSystemContext _context;

        public DeleteModelTempRouteType(WCTCHealthSystemContext context)
        {
            _context = context;
        }

        [BindProperty]
        public TempRouteType TempRouteType { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            ViewData["RegularMessage"] = "Are you sure you want to delete this?";
            ViewData["ErrorMessage"] = "";

            if (id == null)
            {
                return NotFound();
            }

            TempRouteType = await _context.TempRouteTypes.FirstOrDefaultAsync(m => m.TempRouteTypeId == id);

            if (TempRouteType == null)
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

            TempRouteType = await _context.TempRouteTypes.FindAsync(id);

            if (TempRouteType != null)
            {
                // See if any PCA records exist with this temp route type
                bool usingExists = _context.Pcarecords.Any(p => p.TempRouteTypeId == TempRouteType.TempRouteTypeId);
                if (usingExists)
                {
                    Console.WriteLine("PCA records exist using this record.");
                    ViewData["RegularMessage"] = "";
                    ViewData["ErrorMessage"] = "PCA records exist using this record. Delete not available.";
                    return Page();
                }

                _context.TempRouteTypes.Remove(TempRouteType);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
