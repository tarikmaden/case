using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Models
{
    public class ContactInfo
    {
        [Key]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ContactTypeId { get; set; }
        public string Content { get; set; }
        public virtual ContactType ContactTypes { get; set; }
    }
}