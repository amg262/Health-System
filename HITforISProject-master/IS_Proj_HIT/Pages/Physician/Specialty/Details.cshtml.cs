using System.Threading.Tasks;
using IS_Proj_HIT.Entities;
using IS_Proj_HIT.Entities.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace IS_Proj_HIT
{
    public class DetailsModelSpecialty : PageModel
    {
        private readonly WCTCHealthSystemContext _context;

        public DetailsModelSpecialty(WCTCHealthSystemContext context)
        {
            _context = context;
        }

        public Specialty Specialty { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Specialty = await _context.Specialties.FirstOrDefaultAsync(m => m.SpecialtyId == id);

            if (Specialty == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
