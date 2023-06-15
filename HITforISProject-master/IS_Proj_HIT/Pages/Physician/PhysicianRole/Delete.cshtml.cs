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
    public class DeleteModelPhysicianRole : PageModel
    {
        private readonly WCTCHealthSystemContext _context;

        public DeleteModelPhysicianRole(WCTCHealthSystemContext context)
        {
            _context = context;
        }

        [BindProperty]
        public PhysicianRole PhysicianRole { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            ViewData["RegularMessage"] = "Are you sure you want to delete this?";
            ViewData["ErrorMessage"] = "";

            if (id == null)
            {
                return NotFound();
            }

            PhysicianRole = await _context.PhysicianRoles.FirstOrDefaultAsync(m => m.PhysicianRoleId == id);

            if (PhysicianRole == null)
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

            PhysicianRole = await _context.PhysicianRoles.FindAsync(id);

            if (PhysicianRole != null)
            {
                // See if any Encounter records exist with this type 
                bool usingExists = _context.EncounterPhysicians.Any(e => e.PhysicianRoleId == PhysicianRole.PhysicianRoleId);
                if (usingExists)
                {
                    Console.WriteLine("Encounter records exist using this record.");
                    ViewData["RegularMessage"] = "";
                    ViewData["ErrorMessage"] = "Encounter records exist using this record. Delete not available.";
                    return Page();
                }


                _context.PhysicianRoles.Remove(PhysicianRole);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
