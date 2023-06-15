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

    public class EditModelTempRouteType : PageModel
    {
        private readonly WCTCHealthSystemContext _context;

        public EditModelTempRouteType(WCTCHealthSystemContext context)
        {
            _context = context;
        }

        [BindProperty]
        public TempRouteType TempRouteType { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(TempRouteType).State = EntityState.Modified;

            try
            {
                TempRouteType.LastModified = DateTime.Now;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TempRouteTypeExists(TempRouteType.TempRouteTypeId))
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

        private bool TempRouteTypeExists(int id)
        {
            return _context.TempRouteTypes.Any(e => e.TempRouteTypeId == id);
        }
    }
}
