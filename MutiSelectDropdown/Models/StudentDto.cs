using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MutiSelectDropdown.Models
{
    public class StudentDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public List<SelectListItem> drpCourses { get; set; }
        
        [Display(Name = "Courses")]
        public long[] CourseIds { get; set; }

    }
}
