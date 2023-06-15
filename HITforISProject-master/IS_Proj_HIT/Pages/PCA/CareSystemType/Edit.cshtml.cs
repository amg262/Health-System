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
    public class EditModelCareSystemType : PageModel
    {
        private readonly WCTCHealthSystemContext _context;

        public EditModelCareSystemType(WCTCHealthSystemContext context)
        {
            _context = context;
        }

        [BindProperty]
        public CareSystemType CareSystemType { get; set; }

        public async Task<IActionResult> OnGetAsync(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CareSystemType = await _context.CareSystemTypes.FirstOrDefaultAsync(m => m.CareSystemTypeId == id);

            if (CareSystemType == null)
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

            _context.Attach(CareSystemType).State = EntityState.Modified;

            try
            {
                CareSystemType.LastModified = DateTime.Now;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CareSystemTypeExists(CareSystemType.CareSystemTypeId))
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

        private bool CareSystemTypeExists(short id)
        {
            return _context.CareSystemTypes.Any(e => e.CareSystemTypeId == id);
        }
    }
}
