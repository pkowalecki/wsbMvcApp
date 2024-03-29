﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using wsbMvcApp.Data;
using wsbMvcApp.Models;

namespace wsbMvcApp.Controllers
{
    [Authorize]
    public class MealsController : Controller
    {
        private readonly wsbMvcAppContext _context;

        public MealsController(wsbMvcAppContext context)
        {
            _context = context;
        }

        // GET: Meals
        public async Task<IActionResult> Index()
        {
              return _context.Meal != null ? 
                          View(await _context.Meal.ToListAsync()) :
                          Problem("Entity set 'wsbMvcAppContext.Meal'  is null.");
        }

        // GET: Meals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Meal == null)
            {
                return NotFound();
            }

            var meal = await _context.Meal
                .FirstOrDefaultAsync(m => m.Id == id);
            if (meal == null)
            {
                return NotFound();
            }

            return View(meal);
        }

        // GET: Meals/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Meals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description")] Meal meal)
        {
            var userId = HttpContext.Session.GetString("UserId");

            if (userId != null)
            {
                if (int.TryParse(userId, out int userIdInt))
                {
                    var user = await _context.User.FindAsync(userIdInt);

                    if (user != null)
                    {
                        meal.User = user;
                        _context.Add(meal);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                        
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Nie znaleziono użytkownika o podanym identyfikatorze.");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Błąd podczas przetwarzania identyfikatora użytkownika.");
                }
            }

            return View(meal);
        }

        // GET: Meals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Meal == null)
            {
                return NotFound();
            }

            var meal = await _context.Meal.FindAsync(id);
            if (meal == null)
            {
                return NotFound();
            }
            return View(meal);
        }

        // POST: Meals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] Meal meal)
        {
            if (id != meal.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var userId = HttpContext.Session.GetString("UserId");
                    if (userId != null)
                    {
                        meal.UserId = int.Parse(userId);
                    }

                    _context.Update(meal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MealExists(meal.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(meal);
        }

        // GET: Meals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Meal == null)
            {
                return NotFound();
            }

            var meal = await _context.Meal
                .FirstOrDefaultAsync(m => m.Id == id);
            if (meal == null)
            {
                return NotFound();
            }

            return View(meal);
        }

        // POST: Meals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Meal == null)
            {
                return Problem("Entity set 'wsbMvcAppContext.Meal'  is null.");
            }
            var meal = await _context.Meal.FindAsync(id);
            if (meal != null)
            {
                _context.Meal.Remove(meal);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MealExists(int id)
        {
          return (_context.Meal?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
