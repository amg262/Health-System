using System.Threading.Tasks;
using IS_Proj_HIT.Entities;
using IS_Proj_HIT.Entities.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace IS_Proj_HIT
{
    public class DetailsModelFacility : PageModel
    {
        private readonly WCTCHealthSystemContext _context;

        public DetailsModelFacility(WCTCHealthSystemContext context)
        {
            _context = context;
        }

        public Facility Facility { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Facility = await _context.Facilities
                .Include(f => f.Address).FirstOrDefaultAsync(m => m.FacilityId == id);

            if (Facility == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
