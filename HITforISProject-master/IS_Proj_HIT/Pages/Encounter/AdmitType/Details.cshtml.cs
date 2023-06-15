using System.Threading.Tasks;
using IS_Proj_HIT.Entities;
using IS_Proj_HIT.Entities.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace IS_Proj_HIT
{
    public class DetailsModelAdmitType : PageModel
    {
        private readonly WCTCHealthSystemContext _context;

        public DetailsModelAdmitType(WCTCHealthSystemContext context)
        {
            _context = context;
        }

        public AdmitType AdmitType { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AdmitType = await _context.AdmitTypes.FirstOrDefaultAsync(m => m.AdmitTypeId == id);

            if (AdmitType == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
