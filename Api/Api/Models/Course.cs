using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models
{
    public class Course
    {
        [Display(Name = "Number")]
        public int CourseID { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}
