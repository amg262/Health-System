using System;
using System.Threading.Tasks;
using IS_Proj_HIT.Entities;
using IS_Proj_HIT.Entities.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace IS_Proj_HIT
{
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Administrator")]
    public class CreateModelEncounterPhysicians : PageModel
    {
        private readonly WCTCHealthSystemContext _context;

        public CreateModelEncounterPhysicians(WCTCHealthSystemContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["PhysicianId"] = new SelectList(_context.Physicians, "PhysicianId", "FirstName");
        ViewData["PhysicianRoleId"] = new SelectList(_context.PhysicianRoles, "PhysicianRoleId", "Name");
            return Page();
        }

        [BindProperty]
        public EncounterPhysician EncounterPhysicians { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            EncounterPhysicians.LastModified = DateTime.Now;
            _context.EncounterPhysicians.Add(EncounterPhysicians);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}