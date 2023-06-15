using System.Collections.Generic;
using System.Threading.Tasks;
using IS_Proj_HIT.Entities;
using IS_Proj_HIT.Entities.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace IS_Proj_HIT
{
    public class IndexModelPhysicianRole : PageModel
    {
        private readonly WCTCHealthSystemContext _context;

        public IndexModelPhysicianRole(WCTCHealthSystemContext context)
        {
            _context = context;
        }

        public IList<PhysicianRole> PhysicianRole { get;set; }

        public async Task OnGetAsync()
        {
            PhysicianRole = await _context.PhysicianRoles.ToListAsync();
        }
    }
}
