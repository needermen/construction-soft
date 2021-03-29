using Newtonsoft.Json;

namespace Cs.Application.Buildings.Models
{
    public class WorkTechnicViewModel
    {
        public int WorkId { get; set; }

        [JsonProperty("Id")] public int TechnicId { get; set; }
        public int Count { get; set; }

        public string TechnicName { get; set; }
        public decimal TechnicPrice { get; set; }
        public string TechnicDimension { get; set; }
        public string TechnicCategory { get; set; }
        public decimal TechnicCoefficient { get; set; }

        public decimal FullPrice => TechnicPrice * Count * TechnicCoefficient;
    }
}