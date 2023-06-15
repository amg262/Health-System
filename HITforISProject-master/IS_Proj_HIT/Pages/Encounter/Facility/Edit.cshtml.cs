﻿using System;
using System.Linq;
using System.Threading.Tasks;
using IS_Proj_HIT.Entities;
using IS_Proj_HIT.Entities.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace IS_Proj_HIT
{
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Administrator")]
    public class EditModelFacility : PageModel
    {
        private readonly WCTCHealthSystemContext _context;

        public EditModelFacility(WCTCHealthSystemContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Facility Facility { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Facility = await _context.Facilities
                .Include(f => f.Address).FirstOrDefaultAsync(m => m.FacilityId == id);

            if (Facility == null)
            {
                return NotFound();
            }
           ViewData["AddressId"] = new SelectList(_context.Addresses, "AddressId", "Address1");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Facility).State = EntityState.Modified;

            try
            {
                Facility.LastModified = DateTime.Now;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FacilityExists(Facility.FacilityId))
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

        private bool FacilityExists(int id)
        {
            return _context.Facilities.Any(e => e.FacilityId == id);
        }
    }
}
