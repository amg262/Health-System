﻿using System.Collections.Generic;
using System.Threading.Tasks;
using IS_Proj_HIT.Entities;
using IS_Proj_HIT.Entities.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace IS_Proj_HIT
{
    public class IndexModelAdmitType : PageModel
    {
        private readonly WCTCHealthSystemContext _context;

        public IndexModelAdmitType(WCTCHealthSystemContext context)
        {
            _context = context;
        }

        public IList<AdmitType> AdmitType { get;set; }

        public async Task OnGetAsync()
        {
            AdmitType = await _context.AdmitTypes.ToListAsync();
        }
    }
}
