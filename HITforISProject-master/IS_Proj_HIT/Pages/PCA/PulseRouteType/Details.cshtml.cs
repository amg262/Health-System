using System.Threading.Tasks;
using IS_Proj_HIT.Entities;
using IS_Proj_HIT.Entities.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace IS_Proj_HIT
{
    public class DetailsModelPulseRouteType : PageModel
    {
        private readonly WCTCHealthSystemContext _context;

        public DetailsModelPulseRouteType(WCTCHealthSystemContext context)
        {
            _context = context;
        }

        public PulseRouteType PulseRouteType { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PulseRouteType = await _context.PulseRouteTypes.FirstOrDefaultAsync(m => m.PulseRouteTypeId == id);

            if (PulseRouteType == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
