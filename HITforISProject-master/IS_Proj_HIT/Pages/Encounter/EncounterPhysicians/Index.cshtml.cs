using System.Collections.Generic;
using System.Threading.Tasks;
using IS_Proj_HIT.Entities;
using IS_Proj_HIT.Entities.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace IS_Proj_HIT
{
    public class IndexModelEncounterPhysicians : PageModel
    {
        private readonly WCTCHealthSystemContext _context;

        public IndexModelEncounterPhysicians(WCTCHealthSystemContext context)
        {
            _context = context;
        }

        public IList<EncounterPhysician> EncounterPhysicians { get;set; }

        public async Task OnGetAsync()
        {
            EncounterPhysicians = await _context.EncounterPhysicians
                .Include(e => e.Physician)
                .Include(e => e.PhysicianRole).ToListAsync();
        }
    }
}
