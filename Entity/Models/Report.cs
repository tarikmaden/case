

using System.ComponentModel.DataAnnotations;

namespace Entity.Models
{
    public class Report
    {
        [Key]
        public Guid Id { get; set; }
        
        public Guid UserId { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
        
        [Required]
        public string Status { get; set; }
        public string Path { get; set; }
    }
}