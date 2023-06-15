using System.Threading.Tasks;
using IS_Proj_HIT.Entities;
using IS_Proj_HIT.Entities.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace IS_Proj_HIT
{
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Administrator")]
    public class DeleteModelPainRatingImage : PageModel
    {
        private readonly WCTCHealthSystemContext _context;

        public DeleteModelPainRatingImage(WCTCHealthSystemContext context)
        {
            _context = context;
        }

        [BindProperty]
        public PainRatingImage PainRatingImage { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PainRatingImage = await _context.PainRatingImages
                .Include(p => p.PainRating).FirstOrDefaultAsync(m => m.PainRatingId == id);

            if (PainRatingImage == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PainRatingImage = await _context.PainRatingImages.FindAsync(id);

            if (PainRatingImage != null)
            {
                _context.PainRatingImages.Remove(PainRatingImage);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
