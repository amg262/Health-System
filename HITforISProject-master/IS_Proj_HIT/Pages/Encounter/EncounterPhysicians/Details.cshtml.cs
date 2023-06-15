using System.Threading.Tasks;
using IS_Proj_HIT.Entities;
using IS_Proj_HIT.Entities.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace IS_Proj_HIT
{
    public class DetailsModelEncounterPhysicians : PageModel
    {
        private readonly WCTCHealthSystemContext _context;

        public DetailsModelEncounterPhysicians(WCTCHealthSystemContext context)
        {
            _context = context;
        }

        public EncounterPhysician EncounterPhysicians { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            EncounterPhysicians = await _context.EncounterPhysicians
                .Include(e => e.Physician)
                .Include(e => e.PhysicianRole).FirstOrDefaultAsync(m => m.EncounterPhysiciansId == id);

            if (EncounterPhysicians == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
