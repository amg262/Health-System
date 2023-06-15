using System.Threading.Tasks;
using IS_Proj_HIT.Entities;
using IS_Proj_HIT.Entities.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace IS_Proj_HIT
{
    public class DetailsModelBloodPressureRouteType : PageModel
    {
        private readonly WCTCHealthSystemContext _context;

        public DetailsModelBloodPressureRouteType(WCTCHealthSystemContext context)
        {
            _context = context;
        }

        public BloodPressureRouteType BloodPressureRouteType { get; set; }

        public async Task<IActionResult> OnGetAsync(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            BloodPressureRouteType = await _context.BloodPressureRouteTypes.FirstOrDefaultAsync(m => m.BloodPressureRouteTypeId == id);

            if (BloodPressureRouteType == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
