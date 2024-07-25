using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using PagedList;
using PagedList.Mvc;
using System.Web;
using ProfessorWebsite.Data;
using ProfessorWebsite.Models;


namespace ProfessorWebsite.Controllers
{
    public class CoursesController : Controller
    {
        private readonly DataContext _context;
        private readonly IToastNotification _toast;
        public CoursesController(DataContext context, IToastNotification toast)
        {
            _context = context;
            _toast = toast;
        }

        public async Task<IActionResult> Index()
        {
            // int pageSize = 10; // Number of items per page
            // var pageCount = _context.Courses.Count() / pageSize;
           // int pageNumber = (page ?? 1);
           // var courses = _context.Courses.Include(t => t.Professor)
                    //        .OrderBy(c => c.Name)
                      //      .ToPagedList(pageNumber, pageSize);

            var courses = await _context.Courses.Include(t => t.Professor).ToListAsync();
            return View(courses);

        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var course = new CourseDto
            {
                Professors = await _context.Professors.OrderBy(t => t.Name).ToListAsync()
            };

            return View(course);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CourseDto courseDto)
        {
            var course = new Course
            {
                Name = courseDto.Name,
                ProfessorId = courseDto.ProfessorId,
            };
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            _toast.AddSuccessToastMessage("Course created successfully");
           
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var course = await _context.Courses.FindAsync(id);

            if (course == null)
                return NotFound();

            var courseDto = new CourseDto
            {
                CourseId = course.CourseId,
                Name = course.Name,
                ProfessorId = course.ProfessorId,
                Professors = await _context.Professors.OrderBy(m => m.Name).ToListAsync()
            };

            return View(courseDto);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(CourseDto courseDto, int id)
        {
            if (!ModelState.IsValid)
            {

                return View(courseDto);
            }

            var course = await _context.Courses.FindAsync(id); //finalllyyyyyyyyy Zabatt

            if (course == null)
                return NotFound("No Course Here!");

            course.Name = courseDto.Name;
            course.ProfessorId = courseDto.ProfessorId;
            courseDto.Professors = await _context.Professors.OrderBy(m => m.Name).ToListAsync();

            await _context.SaveChangesAsync();


            _toast.AddSuccessToastMessage("Course updated successfully");
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return BadRequest();

            var course = await _context.Courses.Include(t => t.Professor).SingleOrDefaultAsync(t => t.CourseId == id);

            if (course == null)
                return NotFound();

            return View(course);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var course = await _context.Courses.Include(t => t.Professor).SingleOrDefaultAsync(t => t.CourseId == id);
            if (course == null)
            {
                return NotFound("course not found");
            }
            return View(course);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return BadRequest();

            var course = await _context.Courses.FindAsync(id);

            if (course == null)
                return NotFound();

            _context.Courses.Remove(course);
            _context.SaveChanges();


            return RedirectToAction(nameof(Index));
        }


    }
}
