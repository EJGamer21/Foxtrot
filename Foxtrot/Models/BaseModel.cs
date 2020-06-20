using System;
using Foxtrot.Models.Contracts;

namespace Foxtrot.Models
{
    public abstract class BaseModel : IAuditableModel//, IConcurrencyModel
    {
        public DateTime CreatedDate { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public Guid UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        //public byte[] RowVersion { get; set; }
    }
}