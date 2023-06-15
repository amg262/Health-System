using System.Threading.Tasks;
using IS_Proj_HIT.Entities;
using IS_Proj_HIT.Entities.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace IS_Proj_HIT
{
    public class DetailsModelProgramFacility : PageModel
    {
        private readonly WCTCHealthSystemContext _context;

        public DetailsModelProgramFacility(WCTCHealthSystemContext context)
        {
            _context = context;
        }

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
    }
}
