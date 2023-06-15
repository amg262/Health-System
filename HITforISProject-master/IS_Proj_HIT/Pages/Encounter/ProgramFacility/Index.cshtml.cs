using System.Collections.Generic;
using System.Threading.Tasks;
using IS_Proj_HIT.Entities;
using IS_Proj_HIT.Entities.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace IS_Proj_HIT
{
    public class IndexModelProgramFacility : PageModel
    {
        private readonly WCTCHealthSystemContext _context;

        public IndexModelProgramFacility(WCTCHealthSystemContext context)
        {
            _context = context;
        }

        public IList<ProgramFacility> ProgramFacility { get;set; }

        public async Task OnGetAsync()
        {
            ProgramFacility = await _context.ProgramFacilities
                .Include(p => p.Facility)
                .Include(p => p.Program).ToListAsync();
        }
    }
}
