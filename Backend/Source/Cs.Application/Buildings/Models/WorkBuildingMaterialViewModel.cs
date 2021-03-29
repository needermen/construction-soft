using Newtonsoft.Json;

namespace Cs.Application.Buildings.Models
{
    public class WorkBuildingMaterialViewModel
    {
        public int WorkId { get; set; }

        [JsonProperty("Id")] public int MaterialId { get; set; }
        public int Count { get; set; }

        public string MaterialName { get; set; }
        public decimal MaterialPrice { get; set; }
        public string MaterialDimension { get; set; }
        public string MaterialCategory { get; set; }
        public decimal MaterialCoefficient { get; set; }

        public decimal FullPrice => MaterialPrice * Count * MaterialCoefficient;
    }
}