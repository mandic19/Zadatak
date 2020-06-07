using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Api.Models
{

    public class Status
    {
        [Key]
        public int StatusID { get; set; }
        [Required]
        [StringLength(15)]
        public string Name { get; set; }

        [JsonIgnore]
        public virtual ICollection<Student> Students { get; set; }
    }
}
