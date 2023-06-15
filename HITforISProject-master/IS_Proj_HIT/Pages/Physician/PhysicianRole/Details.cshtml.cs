﻿using System.Threading.Tasks;
using IS_Proj_HIT.Entities;
using IS_Proj_HIT.Entities.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace IS_Proj_HIT
{
    public class DetailsModelPhysicianRole : PageModel
    {
        private readonly WCTCHealthSystemContext _context;

        public DetailsModelPhysicianRole(WCTCHealthSystemContext context)
        {
            _context = context;
        }

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
    }
}
