using System.Threading.Tasks;
using IS_Proj_HIT.Entities;
using IS_Proj_HIT.Entities.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace IS_Proj_HIT
{
    public class DetailsModelCareSystemType : PageModel
    {
        private readonly WCTCHealthSystemContext _context;

        public DetailsModelCareSystemType(WCTCHealthSystemContext context)
        {
            _context = context;
        }

        public CareSystemType CareSystemType { get; set; }

        public async Task<IActionResult> OnGetAsync(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CareSystemType = await _context.CareSystemTypes.FirstOrDefaultAsync(m => m.CareSystemTypeId == id);

            if (CareSystemType == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
