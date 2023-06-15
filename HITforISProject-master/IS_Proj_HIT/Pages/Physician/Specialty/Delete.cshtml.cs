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
    public class DeleteModelSpecialty : PageModel
    {
        private readonly WCTCHealthSystemContext _context;

        public DeleteModelSpecialty(WCTCHealthSystemContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Specialty Specialty { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            ViewData["RegularMessage"] = "Are you sure you want to delete this?";
            ViewData["ErrorMessage"] = "";

            if (id == null)
            {
                return NotFound();
            }

            Specialty = await _context.Specialties.FirstOrDefaultAsync(m => m.SpecialtyId == id);

            if (Specialty == null)
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

            Specialty = await _context.Specialties.FindAsync(id);

            if (Specialty != null)
            {
                // See if any Physician records exist with this type 
                bool usingExists = _context.Physicians.Any(e => e.SpecialtyId == Specialty.SpecialtyId);
                if (usingExists)
                {
                    Console.WriteLine("Physician records exist using this record.");
                    ViewData["RegularMessage"] = "";
                    ViewData["ErrorMessage"] = "Physician records exist using this record. Delete not available.";
                    return Page();
                }

                _context.Specialties.Remove(Specialty);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
