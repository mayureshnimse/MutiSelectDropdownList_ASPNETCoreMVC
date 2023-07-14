using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using MutiSelectDropdown.Data;
using MutiSelectDropdown.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



namespace MutiSelectDropdown.Controllers
{
    public class MainController : Controller
    {
        private readonly ApplicationDbContext db;
        public MainController(ApplicationDbContext db) 
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            var studentList = db.Student.ToList();
            return View(studentList);
        }


        public IActionResult AddStudent(long? Id) 
        {
            StudentDto model = new StudentDto(); List<long> courseIds = new List<long>();
            if (Id.HasValue) 
            {
                
                var student = db.Student.Include("StudentCourses").FirstOrDefault(x => x.Id == Id.Value);
                student.StudentCourses.ToList().ForEach(result => courseIds.Add(result.CourseId));
                model.drpCourses = db.Courses.Select(x => new SelectListItem { Text = x.Name, Value =x.Id.ToString() }).ToList();
                model.Id= student.Id;
                model.Name= student.Name;
                model.CourseIds = courseIds.ToArray();
            }
            else
            {
                model = new StudentDto();
                model.drpCourses = db.Courses.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
            }
            
            return View(model);
        }

        [HttpPost]
        public IActionResult AddStudent(StudentDto model) 
        { 
            Student student = new Student();
            List<StudentCourses> studentCourses = new List<StudentCourses>();


            if (model.Id > 0)
            {
                student = db.Student.Include("StudentCourses").FirstOrDefault(x => x.Id == model.Id);
                student.StudentCourses.ToList().ForEach(result => studentCourses.Add(result));
                db.StudentCourses.RemoveRange(studentCourses);
                db.SaveChanges();
                student.Name = model.Name;
                if (model.CourseIds.Length > 0)
                {
                    studentCourses = new List<StudentCourses>();

                    foreach (var courseid in model.CourseIds)
                    {
                        studentCourses.Add(new StudentCourses { CourseId = courseid, StudentId = model.Id });
                    }
                    student.StudentCourses = studentCourses;
                }
                db.SaveChanges();
            }
            else 
            {
                student.Name= model.Name;
                student.DateTimeInLocalTime = DateTime.Now;
                student.DateTimeInUtc = DateTime.UtcNow;
                if(model.CourseIds.Length > 0) 
                {
                    foreach (var courseid in model.CourseIds) 
                    {
                        studentCourses.Add(new StudentCourses { CourseId = courseid, StudentId = model.Id });
                    }
                    student.StudentCourses= studentCourses;
                }
                db.Student.Add(student);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
