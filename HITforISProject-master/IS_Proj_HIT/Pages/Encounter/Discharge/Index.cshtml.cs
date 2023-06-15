using System.Collections.Generic;
using System.Threading.Tasks;
using IS_Proj_HIT.Entities;
using IS_Proj_HIT.Entities.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace IS_Proj_HIT
{
    public class IndexModelDischarge : PageModel
    {
        private readonly WCTCHealthSystemContext _context;

        public IndexModelDischarge(WCTCHealthSystemContext context)
        {
            _context = context;
        }

        public IList<Discharge> Discharge { get;set; }

        public async Task OnGetAsync()
        {
            Discharge = await _context.Discharges.ToListAsync();
        }
    }
}
