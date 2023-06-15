using System.Collections.Generic;
using System.Threading.Tasks;
using IS_Proj_HIT.Entities;
using IS_Proj_HIT.Entities.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace IS_Proj_HIT
{
    public class IndexModelPainRating : PageModel
    {
        private readonly WCTCHealthSystemContext _context;

        public IndexModelPainRating(WCTCHealthSystemContext context)
        {
            _context = context;
        }

        public IList<PainRating> PainRating { get;set; }

        public async Task OnGetAsync()
        {
            PainRating = await _context.PainRatings
                .Include(p => p.PainParameter).ToListAsync();
        }
    }
}
