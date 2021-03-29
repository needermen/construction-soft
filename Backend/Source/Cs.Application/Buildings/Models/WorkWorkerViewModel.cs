using Newtonsoft.Json;

namespace Cs.Application.Buildings.Models
{
    public class WorkWorkerViewModel
    {
        public int WorkId { get; set; }

        [JsonProperty("Id")] public int WorkerId { get; set; }
        public int Count { get; set; }

        public string WorkerName { get; set; }
        public decimal WorkerSalary { get; set; }
        public string WorkerPaymentType { get; set; }
        public string WorkerCategory { get; set; }
        public decimal WorkerCoefficient { get; set; }

        public decimal FullPrice => WorkerSalary * Count * WorkerCoefficient;
    }
}