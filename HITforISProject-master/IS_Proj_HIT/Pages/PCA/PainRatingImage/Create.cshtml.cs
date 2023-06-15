using System.Threading.Tasks;
using IS_Proj_HIT.Entities;
using IS_Proj_HIT.Entities.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace IS_Proj_HIT
{
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Administrator")]
    public class CreateModelPainRatingImage : PageModel
    {
        private readonly WCTCHealthSystemContext _context;

        public CreateModelPainRatingImage(WCTCHealthSystemContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["PainRatingId"] = new SelectList(_context.PainRatings, "PainRatingId", "Description");
            return Page();
        }

        [BindProperty]
        public PainRatingImage PainRatingImage { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.PainRatingImages.Add(PainRatingImage);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}