using Newtonsoft.Json;

namespace Cs.Application.Buildings.Models
{
    public class WorkBrigadeViewModel
    {
        public int WorkId { get; set; }

        [JsonProperty("Id")] public int BrigadeId { get; set; }
        public int Count { get; set; }

        public string BrigadeName { get; set; }
        public decimal BrigadeSalary { get; set; }
        public string BrigadePaymentType { get; set; }
        public string BrigadeCategory { get; set; }

        public decimal FullPrice => BrigadeSalary * Count;
    }
}