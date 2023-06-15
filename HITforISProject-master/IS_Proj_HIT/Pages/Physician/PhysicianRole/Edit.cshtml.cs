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
    public class EditModel : PageModel
    {
        private readonly WCTCHealthSystemContext _context;

        public EditModel(WCTCHealthSystemContext context)
        {
            _context = context;
        }

        [BindProperty]
        public PhysicianRole PhysicianRole { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PhysicianRole = await _context.PhysicianRoles.FirstOrDefaultAsync(m => m.PhysicianRoleId == id);

            if (PhysicianRole == null)
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

            _context.Attach(PhysicianRole).State = EntityState.Modified;

            try
            {
                PhysicianRole.LastModified = DateTime.Now;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhysicianRoleExists(PhysicianRole.PhysicianRoleId))
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

        private bool PhysicianRoleExists(int id)
        {
            return _context.PhysicianRoles.Any(e => e.PhysicianRoleId == id);
        }
    }
}
