using System.Collections.Generic;
using System.Threading.Tasks;
using IS_Proj_HIT.Entities;
using IS_Proj_HIT.Entities.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace IS_Proj_HIT
{
    public class IndexModelDepartments : PageModel
    {
        private readonly WCTCHealthSystemContext _context;

        public IndexModelDepartments(WCTCHealthSystemContext context)
        {
            _context = context;
        }

        public IList<Department> Departments { get;set; }

        public async Task OnGetAsync()
        {
            Departments = await _context.Departments.ToListAsync();
        }
    }
}
