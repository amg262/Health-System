using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IS_Proj_HIT.Entities;
using IS_Proj_HIT.Entities.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IS_Proj_HIT.Models;
using Microsoft.AspNetCore.Authorization;

namespace IS_Proj_HIT
{
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Administrator")]
    public class EditModelNoteType : PageModel
    {
        private readonly WCTCHealthSystemContext _context;

        public EditModelNoteType(WCTCHealthSystemContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(NoteType).State = EntityState.Modified;

            try
            {
                NoteType.LastModified = DateTime.Now;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NoteTypeExists(NoteType.NoteTypeId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool NoteTypeExists(int id)
        {
            return _context.NoteTypes.Any(e => e.NoteTypeId == id);
        }
    }
}
