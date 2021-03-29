namespace Cs.Application.Buildings.Models
{
    public class PhaseViewModel : BuildingBaseViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int BuildingId { get; set; }

        public decimal FullPrice { get; set; }
        
        public decimal? ExtraPrice { get; set; }
    }
}