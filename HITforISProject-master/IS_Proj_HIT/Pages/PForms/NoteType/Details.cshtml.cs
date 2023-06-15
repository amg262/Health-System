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
    public class DetailsModelNoteType : PageModel
    {
        private readonly WCTCHealthSystemContext _context;

        public DetailsModelNoteType(WCTCHealthSystemContext context)
        {
            _context = context;
        }

        public NoteType NoteType { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            NoteType = await _context.NoteTypes.FirstOrDefaultAsync(m => m.NoteTypeId == id);

            if (NoteType == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
