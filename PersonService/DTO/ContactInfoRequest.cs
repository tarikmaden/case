using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonService.DTO
{
    public class ContactInfoRequest
    {
        public Guid UserId { get; set; }
        public Guid ContactTypeId { get; set; }
        public string Content { get; set; }
    }
}