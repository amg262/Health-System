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
    public class DeleteModelCareSystemParameter : PageModel
    {
        private readonly WCTCHealthSystemContext _context;

        public DeleteModelCareSystemParameter(WCTCHealthSystemContext context)
        {
            _context = context;
        }

        [BindProperty]
        public CareSystemParameter CareSystemParameter { get; set; }

        public async Task<IActionResult> OnGetAsync(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CareSystemParameter = await _context.CareSystemParameters
                .Include(c => c.CareSystemType).FirstOrDefaultAsync(m => m.CareSystemParameterId == id);

            if (CareSystemParameter == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CareSystemParameter = await _context.CareSystemParameters.FindAsync(id);

            if (CareSystemParameter != null)
            {
                _context.CareSystemParameters.Remove(CareSystemParameter);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
