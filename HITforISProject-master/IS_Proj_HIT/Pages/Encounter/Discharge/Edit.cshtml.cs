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
    public class EditModelDischarge : PageModel
    {
        private readonly WCTCHealthSystemContext _context;

        public EditModelDischarge(WCTCHealthSystemContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Discharge Discharge { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Discharge = await _context.Discharges.FirstOrDefaultAsync(m => m.DischargeId == id);

            if (Discharge == null)
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

            _context.Attach(Discharge).State = EntityState.Modified;

            try
            {
                Discharge.LastModified = DateTime.Now;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DischargeExists(Discharge.DischargeId))
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

        private bool DischargeExists(int id)
        {
            return _context.Discharges.Any(e => e.DischargeId == id);
        }
    }
}
