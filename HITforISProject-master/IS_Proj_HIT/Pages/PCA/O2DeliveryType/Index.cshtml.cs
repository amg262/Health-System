using System.Collections.Generic;
using System.Threading.Tasks;
using IS_Proj_HIT.Entities;
using IS_Proj_HIT.Entities.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace IS_Proj_HIT
{
    public class IndexModelO2deliveryType : PageModel
    {
        private readonly WCTCHealthSystemContext _context;

        public IndexModelO2deliveryType(WCTCHealthSystemContext context)
        {
            _context = context;
        }

        public IList<O2deliveryType> O2deliveryType { get;set; }

        public async Task OnGetAsync()
        {
            O2deliveryType = await _context.O2deliveryTypes.ToListAsync();
        }
    }
}
