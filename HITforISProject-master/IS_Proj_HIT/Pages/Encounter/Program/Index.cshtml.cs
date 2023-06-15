using System.Collections.Generic;
using System.Threading.Tasks;
using IS_Proj_HIT.Entities.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace IS_Proj_HIT
{
    public class IndexModelProgram : PageModel
    {
        private readonly WCTCHealthSystemContext _context;

        public IndexModelProgram(WCTCHealthSystemContext context)
        {
            _context = context;
        }

        public IList<Entities.Program> Program { get;set; }

        public async Task OnGetAsync()
        {
            Program = await _context.Programs.ToListAsync();
        }
    }
}
