using System.Collections.Generic;
using System.Threading.Tasks;
using IS_Proj_HIT.Entities;
using IS_Proj_HIT.Entities.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace IS_Proj_HIT
{
    public class IndexModelProviderType : PageModel
    {
        private readonly WCTCHealthSystemContext _context;

        public IndexModelProviderType(WCTCHealthSystemContext context)
        {
            _context = context;
        }

        public IList<ProviderType> ProviderType { get;set; }

        public async Task OnGetAsync()
        {
            ProviderType = await _context.ProviderTypes.ToListAsync();
        }
    }
}
