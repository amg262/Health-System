using System.Collections.Generic;
using System.Threading.Tasks;
using IS_Proj_HIT.Entities;
using IS_Proj_HIT.Entities.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace IS_Proj_HIT
{
    public class IndexModelPainScaleType : PageModel
    {
        private readonly WCTCHealthSystemContext _context;

        public IndexModelPainScaleType(WCTCHealthSystemContext context)
        {
            _context = context;
        }

        public IList<PainScaleType> PainScaleType { get;set; }

        public async Task OnGetAsync()
        {
            PainScaleType = await _context.PainScaleTypes.ToListAsync();
        }
    }
}
