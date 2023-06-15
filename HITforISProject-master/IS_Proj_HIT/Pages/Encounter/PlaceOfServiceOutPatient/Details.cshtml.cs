using System.Threading.Tasks;
using IS_Proj_HIT.Entities;
using IS_Proj_HIT.Entities.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace IS_Proj_HIT
{
    public class DetailsModelPlaceOfServiceOutPatient : PageModel
    {
        private readonly WCTCHealthSystemContext _context;

        public DetailsModelPlaceOfServiceOutPatient(WCTCHealthSystemContext context)
        {
            _context = context;
        }

        public PlaceOfServiceOutPatient PlaceOfServiceOutPatient { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PlaceOfServiceOutPatient = await _context.PlaceOfServiceOutPatients.FirstOrDefaultAsync(m => m.PlaceOfServiceId == id);

            if (PlaceOfServiceOutPatient == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
