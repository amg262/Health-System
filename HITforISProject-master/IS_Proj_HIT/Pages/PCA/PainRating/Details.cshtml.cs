using System.Threading.Tasks;
using IS_Proj_HIT.Entities;
using IS_Proj_HIT.Entities.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace IS_Proj_HIT
{
    public class DetailsModelPainRating : PageModel
    {
        private readonly WCTCHealthSystemContext _context;

        public DetailsModelPainRating(WCTCHealthSystemContext context)
        {
            _context = context;
        }

        public PainRating PainRating { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PainRating = await _context.PainRatings
                .Include(p => p.PainParameter).FirstOrDefaultAsync(m => m.PainRatingId == id);

            if (PainRating == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
