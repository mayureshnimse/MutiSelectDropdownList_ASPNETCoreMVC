using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MutiSelectDropdown.Models
{
    public class Student
    {
        public Student() 
        {
            StudentCourses = new HashSet<StudentCourses>();  
        }

        [Key]
        public long Id { get; set; }
        [Required]
        [StringLength(200)]

        public string Name { get; set; }
        [Column(TypeName ="datetime")]

        public DateTime DateTimeInLocalTime { get; set; }
        [Column("DateTimeInUTC", TypeName = "datetime")]

        public DateTime DateTimeInUtc { get; set;}
        [InverseProperty("Student")]

        public virtual ICollection<StudentCourses> StudentCourses { get; set; }

    }
}
