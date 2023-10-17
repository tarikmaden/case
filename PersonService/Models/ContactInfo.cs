using System.ComponentModel.DataAnnotations;

namespace PersonService.Models
{
    public class ContactInfo
    {
        [Key]
        public Guid Id { get; set; }
        public Guid PersonId { get; set; }
        public int ContactTypeId { get; set; }
        public string Content { get; set; }
    }
}