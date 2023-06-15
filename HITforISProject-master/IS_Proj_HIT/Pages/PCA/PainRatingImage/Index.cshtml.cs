using System.Collections.Generic;
using System.Threading.Tasks;
using IS_Proj_HIT.Entities;
using IS_Proj_HIT.Entities.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace IS_Proj_HIT
{
    public class IndexModelPainRatingImage : PageModel
    {
        private readonly WCTCHealthSystemContext _context;

        public IndexModelPainRatingImage(WCTCHealthSystemContext context)
        {
            _context = context;
        }

        public IList<PainRatingImage> PainRatingImage { get;set; }

        public async Task OnGetAsync()
        {
            PainRatingImage = await _context.PainRatingImages
                .Include(p => p.PainRating).ToListAsync();
        }
    }
}
