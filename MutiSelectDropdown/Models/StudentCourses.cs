using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MutiSelectDropdown.Models
{
    public class StudentCourses
    {
        [Key]
        public long Id { get; set; }

        public long StudentId { get; set; } 

        public long CourseId { get; set; }

        [ForeignKey(nameof(CourseId))]
        [InverseProperty(nameof(Courses.StudentCourses))]

        public virtual Courses Course { get; set; }
        [ForeignKey(nameof(StudentId))]
        [InverseProperty("StudentCourses")]

        public virtual Student Student { get; set; }

    }
}
