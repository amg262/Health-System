using System.Collections.Generic;
using System.Threading.Tasks;
using IS_Proj_HIT.Entities;
using IS_Proj_HIT.Entities.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace IS_Proj_HIT
{
    public class IndexModelSpecialty : PageModel
    {
        private readonly WCTCHealthSystemContext _context;

        public IndexModelSpecialty(WCTCHealthSystemContext context)
        {
            _context = context;
        }

        public IList<Specialty> Specialty { get;set; }

        public async Task OnGetAsync()
        {
            Specialty = await _context.Specialties.ToListAsync();
        }
    }
}
