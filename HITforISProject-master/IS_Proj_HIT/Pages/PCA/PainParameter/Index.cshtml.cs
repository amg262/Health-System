using System.Collections.Generic;
using System.Threading.Tasks;
using IS_Proj_HIT.Entities;
using IS_Proj_HIT.Entities.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace IS_Proj_HIT
{
    public class IndexModelPainParameter : PageModel
    {
        private readonly WCTCHealthSystemContext _context;

        public IndexModelPainParameter(WCTCHealthSystemContext context)
        {
            _context = context;
        }

        public IList<PainParameter> PainParameter { get;set; }

        public async Task OnGetAsync()
        {
            PainParameter = await _context.PainParameters
                .Include(p => p.PainScaleType).ToListAsync();
        }
    }
}
