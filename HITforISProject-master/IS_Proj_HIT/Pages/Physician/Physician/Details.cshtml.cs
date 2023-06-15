using System.Threading.Tasks;
using IS_Proj_HIT.Entities;
using IS_Proj_HIT.Entities.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace IS_Proj_HIT
{
    public class DetailsModelPhysician : PageModel
    {
        private readonly WCTCHealthSystemContext _context;

        public DetailsModelPhysician(WCTCHealthSystemContext context)
        {
            _context = context;
        }

        public Physician Physician { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Physician = await _context.Physicians
                .Include(p => p.Address)
                .Include(p => p.ProviderType)
                .Include(p => p.Specialty).FirstOrDefaultAsync(m => m.PhysicianId == id);

            if (Physician == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
