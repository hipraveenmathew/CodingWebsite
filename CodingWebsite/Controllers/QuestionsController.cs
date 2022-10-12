using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CodingWebsite.Data;
using CodingWebsite.Models;
using System.Security.Claims;
using CodingWebsite.Services;

namespace CodingWebsite.Controllers
{
    public class QuestionsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;
        private string userId;

        public QuestionsController(ApplicationDbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
            userId= _userService.GetUserId();
        }

        // GET: Questions
        public async Task<IActionResult> Index()
        {
              return _context.Questions != null ? 
                          View(await _context.Questions.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Questions'  is null.");
        }
        public async Task<IActionResult> QuestionsTecher()
        {
            userId = _userService.GetUserId();
            var questions = await _context.Questions.ToListAsync();
            var questionTeacher=questions.Where(m=>m.TeacherId==userId).ToList();

            return View(questionTeacher);
        }

        public async Task<IActionResult> ListOfQuestionsStudents()
        {
            return _context.Questions != null ?
                        View(await _context.Questions.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Questions'  is null.");
        }

        // GET: Questions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Questions == null)
            {
                return NotFound();
            }

            var questions = await _context.Questions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (questions == null)
            {
                return NotFound();
            }

            return View(questions);
        }

        // GET: Questions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Questions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Questions questions)
        {
            questions.TeacherId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            questions.CreatedAt=DateTime.Now;
            questions.UpdatedAt=DateTime.Now;
           
                _context.Add(questions);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
        }

        public async Task<IActionResult> SelectRole(UserDetails userDetails)
        {
            userDetails.CreatedAt=DateTime.Now;
            userDetails.UpdatedAt=DateTime.Now;
            userDetails.UserId= _userService.GetUserId();
            userDetails.Email = _userService.GetUserName();
            _context.Add(userDetails);
            await _context.SaveChangesAsync();

            if (userDetails.UserRole == 1)
            {
                return RedirectToAction("ListOfQuestionsStudents");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // GET: Questions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Questions == null)
            {
                return NotFound();
            }

            var questions = await _context.Questions.FindAsync(id);
            if (questions == null)
            {
                return NotFound();
            }
            return View(questions);
        }

        // POST: Questions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TeacherId,TheQuestion,Input1,Output1,Input2,Output2,Input3,Output3,TheAnswer,FinalDate,Score,Difficulty,StartedCount,ProcessingCount,CompletedCount,CreatedAt,UpdatedAt")] Questions questions)
        {
            if (id != questions.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(questions);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionsExists(questions.Id))
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
            return View(questions);
        }

        // GET: Questions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Questions == null)
            {
                return NotFound();
            }

            var questions = await _context.Questions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (questions == null)
            {
                return NotFound();
            }

            return View(questions);
        }

        // POST: Questions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Questions == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Questions'  is null.");
            }
            var questions = await _context.Questions.FindAsync(id);
            if (questions != null)
            {
                _context.Questions.Remove(questions);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuestionsExists(int id)
        {
          return (_context.Questions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
