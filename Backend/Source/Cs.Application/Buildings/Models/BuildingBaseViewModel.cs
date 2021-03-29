using System;

namespace Cs.Application.Buildings.Models
{
    public class BuildingBaseViewModel
    {
        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int? DurationInDays { get; set; }

        public int OrganizationId { get; set; }
    }
}