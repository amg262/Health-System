﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Transactions;
using IS_Proj_HIT.Entities;
using IS_Proj_HIT.Entities.Data;


namespace IS_Proj_HIT
{
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Administrator")]
    public class DeleteModelCareSystemType : PageModel
    {
        private readonly WCTCHealthSystemContext _context;

        public DeleteModelCareSystemType(WCTCHealthSystemContext context)
        {
            _context = context;
        }

        // Note: The CareSystemType model contains  public virtual ICollection<CareSystemParameter> CareSystemParameters { get; set; }
        [BindProperty]
        public CareSystemType CareSystemType { get; set; }

        public async Task<IActionResult> OnGetAsync(short? id)
        {
            ViewData["RegularMessage"] = "Are you sure you want to delete this?";
            ViewData["ErrorMessage"] = "";

            CareSystemType = await _context.CareSystemTypes
                .Include(csp=>csp.CareSystemParameters)
                .FirstOrDefaultAsync(m => m.CareSystemTypeId == id);

            if (CareSystemType == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CareSystemType = await _context.CareSystemTypes
                .Include(csp => csp.CareSystemParameters)
                .FirstOrDefaultAsync(m => m.CareSystemTypeId == id);

            if (CareSystemType != null)
            {
                // See if any care system assessments exist using these parameters
                foreach (CareSystemParameter csp in CareSystemType.CareSystemParameters)
                {
                    bool csaExists = _context.CareSystemAssessments.Any(c => c.CareSystemParameterId == csp.CareSystemParameterId);
                    if (csaExists)
                    {
                        Console.WriteLine("Assessments exist using these records.");
                        ViewData["RegularMessage"] = "";
                        ViewData["ErrorMessage"] = "Assessments exist using these records. Delete not available.";
                        return Page();
                    }
                }

                using (var tran = new TransactionScope())
                {
                    foreach (CareSystemParameter csp in CareSystemType.CareSystemParameters)
                    {
                        _context.CareSystemParameters.Remove(csp);
                    }
                    _context.CareSystemTypes.Remove(CareSystemType);
                    _context.SaveChanges();

                    tran.Complete();
                }
                
            }
            
            return RedirectToPage("./Index");
        }
    }

}