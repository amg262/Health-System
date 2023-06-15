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
    public class DeleteModelPhysician : PageModel
    {
        private readonly WCTCHealthSystemContext _context;

        public DeleteModelPhysician(WCTCHealthSystemContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Physician Physician { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            ViewData["RegularMessage"] = "Are you sure you want to delete this?";
            ViewData["ErrorMessage"] = "";

            if (id == null)
            {
                return NotFound();
            }

            Physician = await _context.Physicians
                .Include(p => p.Address)
                .Include(p => p.ProviderType)
                .Include(p => p.Specialty).FirstOrDefaultAsync(m => m.PhysicianId == id);

            if (Physician == null)
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

            Physician = await _context.Physicians.FindAsync(id);

            if (Physician != null)
            {
                // See if any Encounter records exist with this type 
                bool usingExists = _context.EncounterPhysicians.Any(e => e.PhysicianId == Physician.PhysicianId);
                if (usingExists)
                {
                    Console.WriteLine("Encounter records exist using this record.");
                    ViewData["RegularMessage"] = "";
                    ViewData["ErrorMessage"] = "Encounter records exist using this record. Delete not available.";
                    return Page();
                }

                _context.Physicians.Remove(Physician);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
