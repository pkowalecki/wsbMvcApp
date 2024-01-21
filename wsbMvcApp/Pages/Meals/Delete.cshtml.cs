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
    public class DeleteModel : PageModel
    {
        private readonly wsbMvcApp.Data.wsbMvcAppContext _context;

        public DeleteModel(wsbMvcApp.Data.wsbMvcAppContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Meal == null)
            {
                return NotFound();
            }
            var meal = await _context.Meal.FindAsync(id);

            if (meal != null)
            {
                Meal = meal;
                _context.Meal.Remove(Meal);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
