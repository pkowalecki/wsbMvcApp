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
    public class DetailsModel : PageModel
    {
        private readonly wsbMvcApp.Data.wsbMvcAppContext _context;

        public DetailsModel(wsbMvcApp.Data.wsbMvcAppContext context)
        {
            _context = context;
        }

      public Meal Meal { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Meal == null)
            {
                return NotFound();
            }

            var meal = await _context.Meal.FirstOrDefaultAsync(m => m.Id == id);
            if (meal == null)
            {
                return NotFound();
            }
            else 
            {
                Meal = meal;
            }
            return Page();
        }
    }
}
