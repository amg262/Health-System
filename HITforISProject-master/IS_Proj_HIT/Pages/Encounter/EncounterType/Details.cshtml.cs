using System.Threading.Tasks;
using IS_Proj_HIT.Entities;
using IS_Proj_HIT.Entities.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace IS_Proj_HIT
{
    public class DetailsModelEncounterType : PageModel
    {
        private readonly WCTCHealthSystemContext _context;

        public DetailsModelEncounterType(WCTCHealthSystemContext context)
        {
            _context = context;
        }

        public EncounterType EncounterType { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            EncounterType = await _context.EncounterTypes.FirstOrDefaultAsync(m => m.EncounterTypeId == id);

            if (EncounterType == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
