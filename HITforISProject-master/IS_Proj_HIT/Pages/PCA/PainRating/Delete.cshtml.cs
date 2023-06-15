using System;
using System.Linq;
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
    public class DeleteModelPainRating : PageModel
    {
        private readonly WCTCHealthSystemContext _context;

        public DeleteModelPainRating(WCTCHealthSystemContext context)
        {
            _context = context;
        }

        [BindProperty]
        public PainRating PainRating { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            ViewData["RegularMessage"] = "Are you sure you want to delete this?";
            ViewData["ErrorMessage"] = "";

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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PainRating = await _context.PainRatings.FindAsync(id);

            if (PainRating != null)
            {
                // See if any PCA pain assessment records exist with this type
                bool usingExists = _context.PcapainAssessments.Any(p => p.PainRatingId== PainRating.PainRatingId);
                if (usingExists)
                {
                    Console.WriteLine("PCA pain assessment records exist using this record.");
                    ViewData["RegularMessage"] = "";
                    ViewData["ErrorMessage"] = "PCA pain assessment records exist using this record. Delete not available.";
                    return Page();
                }

                _context.PainRatings.Remove(PainRating);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
