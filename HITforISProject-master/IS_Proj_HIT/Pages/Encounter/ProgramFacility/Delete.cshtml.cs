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
    public class DeleteModelProgramFacility : PageModel
    {
        private readonly WCTCHealthSystemContext _context;

        public DeleteModelProgramFacility(WCTCHealthSystemContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ProgramFacility ProgramFacility { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProgramFacility = await _context.ProgramFacilities
                .Include(p => p.Facility)
                .Include(p => p.Program).FirstOrDefaultAsync(m => m.ProgramFacilitiesId == id);

            if (ProgramFacility == null)
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

            ProgramFacility = await _context.ProgramFacilities.FindAsync(id);

            if (ProgramFacility != null)
            {
                _context.ProgramFacilities.Remove(ProgramFacility);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
