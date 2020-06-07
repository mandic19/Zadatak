using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models
{
    public class Student
    {
        [Key]
        public int StudentID { get; set; }
        [ForeignKey("Status")]
        public int Status_Id { get; set; }
        [Required]
        [StringLength(15)]
        public string Index { get; set; }
        [StringLength(50)]
        public string FirstName { get; set; }
        [StringLength(50)]
        public string LastName { get; set; }
        [StringLength(10)]
        public string Year { get; set; }

        
        public virtual Status Status { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
    }
}
