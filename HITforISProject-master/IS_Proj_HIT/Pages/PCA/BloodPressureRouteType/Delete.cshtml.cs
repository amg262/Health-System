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
    public class DeleteModelBloodPressureRouteType : PageModel
    {
        private readonly WCTCHealthSystemContext _context;

        public DeleteModelBloodPressureRouteType(WCTCHealthSystemContext context)
        {
            _context = context;
        }

        [BindProperty]
        public BloodPressureRouteType BloodPressureRouteType { get; set; }

        public async Task<IActionResult> OnGetAsync(byte? id)
        {
            ViewData["RegularMessage"] = "Are you sure you want to delete this?";
            ViewData["ErrorMessage"] = "";

            if (id == null)
            {
                return NotFound();
            }

            BloodPressureRouteType = await _context.BloodPressureRouteTypes.FirstOrDefaultAsync(m => m.BloodPressureRouteTypeId == id);

            if (BloodPressureRouteType == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            BloodPressureRouteType = await _context.BloodPressureRouteTypes.FindAsync(id);

            if (BloodPressureRouteType != null)
            {
                // See if any PCA records exist with this type
                bool usingExists = _context.Pcarecords.Any(p => p.BloodPressureRouteTypeId == BloodPressureRouteType.BloodPressureRouteTypeId);
                if (usingExists)
                {
                    Console.WriteLine("PCA records exist using this record.");
                    ViewData["RegularMessage"] = "";
                    ViewData["ErrorMessage"] = "PCA records exist using this record. Delete not available.";
                    return Page();
                }

                _context.BloodPressureRouteTypes.Remove(BloodPressureRouteType);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
