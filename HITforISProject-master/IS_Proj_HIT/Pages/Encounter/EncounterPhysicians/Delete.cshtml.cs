using System;
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
    public class DeleteModelEncounterPhysicians : PageModel
    {
        private readonly WCTCHealthSystemContext _context;

        public DeleteModelEncounterPhysicians(WCTCHealthSystemContext context)
        {
            _context = context;
        }

        [BindProperty]
        public EncounterPhysician EncounterPhysicians { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            ViewData["RegularMessage"] = "Are you sure you want to delete this?";
            ViewData["ErrorMessage"] = "";

            if (id == null)
            {
                return NotFound();
            }

            EncounterPhysicians = await _context.EncounterPhysicians
                .Include(e => e.Physician)
                .Include(e => e.PhysicianRole).FirstOrDefaultAsync(m => m.EncounterPhysiciansId == id);

            if (EncounterPhysicians == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            EncounterPhysicians = await _context.EncounterPhysicians.FindAsync(id);

            if (EncounterPhysicians != null)
            {
                // See if any Encounter records exist with this type 
                // Is This needed? Is this working?
                bool usingExists = _context.EncounterPhysicians.Any(e => e.EncounterPhysiciansId == EncounterPhysicians.EncounterPhysiciansId);
                if (usingExists)
                {
                    Console.WriteLine("Encounter records exist using this record.");
                    ViewData["RegularMessage"] = "";
                    ViewData["ErrorMessage"] = "Encounter records exist using this record. Delete not available.";
                    return Page();
                }

                _context.EncounterPhysicians.Remove(EncounterPhysicians);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
