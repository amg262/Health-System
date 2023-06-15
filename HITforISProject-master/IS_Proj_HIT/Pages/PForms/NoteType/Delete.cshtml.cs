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
using Microsoft.AspNetCore.Authorization;

namespace IS_Proj_HIT
{
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Administrator")]
    public class DeleteModelNoteType : PageModel
    {
        private readonly WCTCHealthSystemContext _context;

        public DeleteModelNoteType(WCTCHealthSystemContext context)
        {
            _context = context;
        }

        [BindProperty]
        public NoteType NoteType { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            ViewData["RegularMessage"] = "Are you sure you want to delete this?";
            ViewData["ErrorMessage"] = "";

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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            NoteType = await _context.NoteTypes.FindAsync(id);

            if (NoteType != null)
            {
                // See if any Progress Notes exist with this type 
                bool usingExists = _context.ProgressNotes.Any(e => e.NoteTypeId== NoteType.NoteTypeId);
                if (usingExists)
                {
                    Console.WriteLine("Progress Notes exist using this record.");
                    ViewData["RegularMessage"] = "";
                    ViewData["ErrorMessage"] = "Progress Notes exist using this record. Delete not available.";
                    return Page();
                }

                _context.NoteTypes.Remove(NoteType);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
