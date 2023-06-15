using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IS_Proj_HIT.Entities;
using IS_Proj_HIT.Entities.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using IS_Proj_HIT.Models;
using Microsoft.AspNetCore.Authorization;

namespace IS_Proj_HIT
{
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Administrator")]
    public class DeleteModelVisitType : PageModel
    {
        private readonly WCTCHealthSystemContext _context;

        public DeleteModelVisitType(WCTCHealthSystemContext context)
        {
            _context = context;
        }

        [BindProperty]
        public VisitType VisitType { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            ViewData["RegularMessage"] = "Are you sure you want to delete this?";
            ViewData["ErrorMessage"] = "";

            if (id == null)
            {
                return NotFound();
            }

            VisitType = await _context.VisitTypes.FirstOrDefaultAsync(m => m.VisitTypeId == id);

            if (VisitType == null)
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

            VisitType = await _context.VisitTypes.FindAsync(id);

            if (VisitType != null)
            {
                // See if any Admit Orders exist with this type 
                bool usingExists = _context.AdmitOrders.Any(e => e.VisitTypeId== VisitType.VisitTypeId);
                if (usingExists)
                {
                    Console.WriteLine("Admit Orders exist using this record.");
                    ViewData["RegularMessage"] = "";
                    ViewData["ErrorMessage"] = "Admit Orders exist using this record. Delete not available.";
                    return Page();
                }

                _context.VisitTypes.Remove(VisitType);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
