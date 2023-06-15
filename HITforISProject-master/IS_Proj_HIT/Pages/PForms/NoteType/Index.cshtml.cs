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
    public class IndexModelNoteType : PageModel
    {
        private readonly WCTCHealthSystemContext _context;

        public IndexModelNoteType(WCTCHealthSystemContext context)
        {
            _context = context;
        }

        public IList<NoteType> NoteType { get;set; }

        public async Task OnGetAsync()
        {
            NoteType = await _context.NoteTypes.ToListAsync();
        }
    }
}
