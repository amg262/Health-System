using System.Collections.Generic;
using System.Threading.Tasks;
using IS_Proj_HIT.Entities;
using IS_Proj_HIT.Entities.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace IS_Proj_HIT
{
    public class IndexModelFacility : PageModel
    {
        private readonly WCTCHealthSystemContext _context;

        public IndexModelFacility(WCTCHealthSystemContext context)
        {
            _context = context;
        }

        public IList<Facility> Facility { get;set; }

        public async Task OnGetAsync()
        {
            Facility = await _context.Facilities
                .Include(f => f.Address).ToListAsync();
        }
    }
}
