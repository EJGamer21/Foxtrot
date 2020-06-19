using System;
using System.Collections.Generic;
using Foxtrot.Models.Contracts;

namespace Foxtrot.Models
{
    public class Role : BaseModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<User> Users { get; set; }
    }
}