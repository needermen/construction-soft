using System;

namespace Cs.Domain
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }

        public EntityStatus EntityStatus { get; set; } = EntityStatus.Active;

        public DateTime CreatedDate { get; set; }
    }

    public enum EntityStatus
    {
        Active = 0,
        Deleted = 1
    }
}