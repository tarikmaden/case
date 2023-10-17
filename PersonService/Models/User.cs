using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PersonService.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        
        [Required]
        public string FirstName { get; set; }
        
        [Required]
        public string LastName { get; set; }
        
        public string Company { get; set; }

        public List<ContactInfo> ContactInfos { get; set; }
    }
}
