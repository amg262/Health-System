using System.Collections.Generic;
using System.Threading.Tasks;
using IS_Proj_HIT.Entities;
using IS_Proj_HIT.Entities.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace IS_Proj_HIT
{
    public class IndexModelCareSystemType : PageModel
    {
        private readonly WCTCHealthSystemContext _context;

        public IndexModelCareSystemType(WCTCHealthSystemContext context)
        {
            _context = context;
        }

        public IList<CareSystemType> CareSystemType { get;set; }

        public async Task OnGetAsync()
        {
            CareSystemType = await _context.CareSystemTypes.ToListAsync();
        }
    }
}
