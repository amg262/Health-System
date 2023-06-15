﻿using System.Threading.Tasks;
using IS_Proj_HIT.Entities;
using IS_Proj_HIT.Entities.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace IS_Proj_HIT
{
    public class DetailsModelDepartments : PageModel
    {
        private readonly WCTCHealthSystemContext _context;

        public DetailsModelDepartments(WCTCHealthSystemContext context)
        {
            _context = context;
        }

        public Department Departments { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Departments = await _context.Departments.FirstOrDefaultAsync(m => m.DepartmentId == id);

            if (Departments == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
