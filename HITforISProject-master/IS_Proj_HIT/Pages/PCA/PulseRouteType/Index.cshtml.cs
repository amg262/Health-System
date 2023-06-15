using System.Collections.Generic;
using System.Threading.Tasks;
using IS_Proj_HIT.Entities;
using IS_Proj_HIT.Entities.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace IS_Proj_HIT
{
    public class IndexModelPulseRouteType : PageModel
    {
        private readonly WCTCHealthSystemContext _context;

        public IndexModelPulseRouteType(WCTCHealthSystemContext context)
        {
            _context = context;
        }

        public IList<PulseRouteType> PulseRouteType { get;set; }

        public async Task OnGetAsync()
        {
            PulseRouteType = await _context.PulseRouteTypes.ToListAsync();
        }
    }
}
