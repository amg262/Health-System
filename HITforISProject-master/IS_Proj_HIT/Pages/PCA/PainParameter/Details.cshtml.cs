using System.Threading.Tasks;
using IS_Proj_HIT.Entities;
using IS_Proj_HIT.Entities.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace IS_Proj_HIT
{
    public class DetailsModelPainParameter : PageModel
    {
        private readonly WCTCHealthSystemContext _context;

        public DetailsModelPainParameter(WCTCHealthSystemContext context)
        {
            _context = context;
        }

        public PainParameter PainParameter { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PainParameter = await _context.PainParameters
                .Include(p => p.PainScaleType).FirstOrDefaultAsync(m => m.PainParameterId == id);

            if (PainParameter == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
