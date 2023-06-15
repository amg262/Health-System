﻿using System;
using System.Linq;
using System.Threading.Tasks;
using IS_Proj_HIT.Entities;
using IS_Proj_HIT.Entities.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace IS_Proj_HIT
{
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Administrator")]

    public class EditModelPcaCommentType : PageModel
    {
        private readonly WCTCHealthSystemContext _context;

        public EditModelPcaCommentType(WCTCHealthSystemContext context)
        {
            _context = context;
        }

        [BindProperty]
        public PcacommentType PcaCommentType { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PcaCommentType = await _context.PcacommentTypes.FirstOrDefaultAsync(m => m.PcacommentTypeId == id);

            if (PcaCommentType == null)
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

            _context.Attach(PcaCommentType).State = EntityState.Modified;

            try
            {
                PcaCommentType.LastModified = DateTime.Now;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PcaCommentTypeExists(PcaCommentType.PcacommentTypeId))
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

        private bool PcaCommentTypeExists(int id)
        {
            return _context.PcacommentTypes.Any(e => e.PcacommentTypeId == id);
        }
    }
}
