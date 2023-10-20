using System.ComponentModel.DataAnnotations;

namespace Entity.Models
{
    public class ContactType
    {
        [Key]
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Slug { get; set; }
    }
}