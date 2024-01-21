using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using wsbMvcApp.Data;
using wsbMvcApp.Models;

namespace wsbMvcApp.Pages.Meals
{
    public class IndexModel : PageModel
    {
        private readonly wsbMvcApp.Data.wsbMvcAppContext _context;

        public IndexModel(wsbMvcApp.Data.wsbMvcAppContext context)
        {
            _context = context;
        }

        public IList<Meal> Meal { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Meal != null)
            {
                Meal = await _context.Meal.ToListAsync();
            }
        }
    }
}
