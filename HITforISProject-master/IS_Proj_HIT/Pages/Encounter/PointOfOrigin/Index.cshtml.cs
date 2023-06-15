using System.Collections.Generic;
using System.Threading.Tasks;
using IS_Proj_HIT.Entities;
using IS_Proj_HIT.Entities.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace IS_Proj_HIT
{
    public class IndexModelPointOfOrigin : PageModel
    {
        private readonly WCTCHealthSystemContext _context;

        public IndexModelPointOfOrigin(WCTCHealthSystemContext context)
        {
            _context = context;
        }

        public IList<PointOfOrigin> PointOfOrigin { get;set; }

        public async Task OnGetAsync()
        {
            PointOfOrigin = await _context.PointOfOrigins.ToListAsync();
        }
    }
}
