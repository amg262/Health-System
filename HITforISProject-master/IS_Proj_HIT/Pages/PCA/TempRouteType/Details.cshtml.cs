using System.Threading.Tasks;
using IS_Proj_HIT.Entities;
using IS_Proj_HIT.Entities.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace IS_Proj_HIT
{
    public class DetailsModelTempRouteType : PageModel
    {
        private readonly WCTCHealthSystemContext _context;

        public DetailsModelTempRouteType(WCTCHealthSystemContext context)
        {
            _context = context;
        }

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
    }
}
