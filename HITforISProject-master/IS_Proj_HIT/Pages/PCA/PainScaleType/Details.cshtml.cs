using System.Threading.Tasks;
using IS_Proj_HIT.Entities;
using IS_Proj_HIT.Entities.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace IS_Proj_HIT
{
    public class DetailsModelPainScaleType : PageModel
    {
        private readonly WCTCHealthSystemContext _context;

        public DetailsModelPainScaleType(WCTCHealthSystemContext context)
        {
            _context = context;
        }

        public PainScaleType PainScaleType { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PainScaleType = await _context.PainScaleTypes.FirstOrDefaultAsync(m => m.PainScaleTypeId == id);

            if (PainScaleType == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
