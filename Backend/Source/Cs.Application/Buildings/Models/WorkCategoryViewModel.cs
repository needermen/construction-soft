namespace Cs.Application.Buildings.Models
{
    public class WorkCategoryViewModel : BuildingBaseViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int PhaseId { get; set; }

        public decimal FullPrice { get; set; }
        
        public decimal? ExtraPrice { get; set; }
    }
}