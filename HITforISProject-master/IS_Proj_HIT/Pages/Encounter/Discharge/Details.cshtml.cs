using System.Threading.Tasks;
using IS_Proj_HIT.Entities;
using IS_Proj_HIT.Entities.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace IS_Proj_HIT
{
    public class DetailsModelDischarge : PageModel
    {
        private readonly WCTCHealthSystemContext _context;

        public DetailsModelDischarge(WCTCHealthSystemContext context)
        {
            _context = context;
        }

        public Discharge Discharge { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Discharge = await _context.Discharges.FirstOrDefaultAsync(m => m.DischargeId == id);

            if (Discharge == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
