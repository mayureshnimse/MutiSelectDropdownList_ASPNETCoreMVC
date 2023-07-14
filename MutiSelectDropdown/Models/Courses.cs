using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MutiSelectDropdown.Models
{
    public class Courses
    {
        public Courses() 
        {
            StudentCourses = new HashSet<StudentCourses>();
        }

        [Key]public long Id { get; set; }

        [Required]
        [StringLength(255)]

        public string Name { get; set; }

        [InverseProperty("Course")]

        public virtual ICollection<StudentCourses> StudentCourses { get; set; }
    }
}
