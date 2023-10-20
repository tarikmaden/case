using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entity.Models;

namespace PersonService.Resource
{
    public class ContactInfoResource
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public ContactType ContactTypes { get; set; }

    }
}