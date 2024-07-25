using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using ProfessorWebsite.Data;
using ProfessorWebsite.Models;


namespace ProfessorWebsite.Controllers
{
    public class ProfessorsController : Controller
    {
        private readonly DataContext _context;
        private readonly IToastNotification _toast;

        public ProfessorsController(DataContext context, IToastNotification toast)
        {
            _context = context;
            _toast = toast;
        }
        public async Task<IActionResult> Index()
        {
            var professors = await _context.Professors.ToListAsync();
            return View(professors);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Professor professor)
        {
            if (!ModelState.IsValid)
            {
                return View(professor);
            }

            _context.Add(professor);
            await _context.SaveChangesAsync();
            _toast.AddSuccessToastMessage("Professor created successfully");
            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var professor = await _context.Professors.FindAsync(id);
            return View(professor);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Professor professorDto)
        {
            var professor = _context.Professors.FirstOrDefault(t => t.ProfessorId == professorDto.ProfessorId);
            // _context.Professors.Update(professor);
            if (professor == null)
            {
                return NotFound();
            }
            professor.Name = professorDto.Name;
            professor.Class = professorDto.Class;
            await _context.SaveChangesAsync();

            _toast.AddSuccessToastMessage("Professor updated successfully");
            return RedirectToAction("Index", "Professors");
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            // var professorDto = _context.Professors.Find(id);
            //return View(professorDto);

            var professor = await _context.Professors
                .Include(p => p.Courses)
                .FirstOrDefaultAsync(m => m.ProfessorId == id);
            if (professor == null)
            {
                return NotFound("there is no professorDto here");
            }


            return View(professor);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var professor = _context.Professors.Find(id);
            if (professor == null)
            {
                return RedirectToAction("Index", "Professors");
            }
            _context.Professors.Remove(professor);
            await _context.SaveChangesAsync();
            _toast.AddSuccessToastMessage("Professor deleted successfully");
            return RedirectToAction("Index", "Professors");
        }
    }
}
