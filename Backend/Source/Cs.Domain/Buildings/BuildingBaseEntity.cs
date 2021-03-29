using System;
using Cs.Domain.Common;

namespace Cs.Domain.Buildings
{
    public class BuildingBaseEntity : OrganizationRelation
    {
        public DateTime? StartDate { get; set; }
        
        public DateTime? EndDate { get; set; }
        
        public int? DurationInDays { get; set; }
    }
}