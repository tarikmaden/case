using System.ComponentModel.DataAnnotations;

namespace PersonService.Models
{
    public class ContactType
    {
        [Key]
        public Guid Id { get; set; }
        public string? Name { get; set; }
    }
}