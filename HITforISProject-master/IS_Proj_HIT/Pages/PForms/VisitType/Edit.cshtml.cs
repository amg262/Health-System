using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IS_Proj_HIT.Entities;
using IS_Proj_HIT.Entities.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IS_Proj_HIT.Models;
using Microsoft.AspNetCore.Authorization;

namespace IS_Proj_HIT
{
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Administrator")]
    public class EditModelVisitType : PageModel
    {
        private readonly WCTCHealthSystemContext _context;

        public EditModelVisitType(WCTCHealthSystemContext context)
        {
            _context = context;
        }

        [BindProperty]
        public VisitType VisitType { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(VisitType).State = EntityState.Modified;

            try
            {
                VisitType.LastModified = DateTime.Now;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VisitTypeExists(VisitType.VisitTypeId))
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

        private bool VisitTypeExists(int id)
        {
            return _context.VisitTypes.Any(e => e.VisitTypeId == id);
        }
    }
}
