using System.Collections.Generic;
using System.Threading.Tasks;
using IS_Proj_HIT.Entities;
using IS_Proj_HIT.Entities.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace IS_Proj_HIT
{
    public class IndexModelPhysician : PageModel
    {
        private readonly WCTCHealthSystemContext _context;

        public IndexModelPhysician(WCTCHealthSystemContext context)
        {
            _context = context;
        }

        public IList<Physician> Physician { get;set; }

        public async Task OnGetAsync()
        {
            Physician = await _context.Physicians
                .Include(p => p.Address)
                .Include(p => p.ProviderType)
                .Include(p => p.Specialty).ToListAsync();
        }
    }
}
