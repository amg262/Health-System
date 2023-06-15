using System.Collections.Generic;
using System.Threading.Tasks;
using IS_Proj_HIT.Entities;
using IS_Proj_HIT.Entities.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace IS_Proj_HIT
{
    public class IndexModelTempRouteType : PageModel
    {
        private readonly WCTCHealthSystemContext _context;

        public IndexModelTempRouteType(WCTCHealthSystemContext context)
        {
            _context = context;
        }

        public IList<TempRouteType> TempRouteType { get;set; }

        public async Task OnGetAsync()
        {
            TempRouteType = await _context.TempRouteTypes.ToListAsync();
        }
    }
}
