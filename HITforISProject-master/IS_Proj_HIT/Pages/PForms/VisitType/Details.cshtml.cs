using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IS_Proj_HIT.Entities;
using IS_Proj_HIT.Entities.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using IS_Proj_HIT.Models;

namespace IS_Proj_HIT
{
    public class DetailsModelVisitType : PageModel
    {
        private readonly WCTCHealthSystemContext _context;

        public DetailsModelVisitType(WCTCHealthSystemContext context)
        {
            _context = context;
        }

        public VisitType VisitType { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            VisitType = await _context.VisitTypes.FirstOrDefaultAsync(m => m.VisitTypeId == id);

            if (VisitType == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
