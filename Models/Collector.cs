using System.ComponentModel.DataAnnotations;

namespace KutipWeb.Models
{
    public class Collector
    {
        [Key]
        public int CollectorId { get; set; }

        [Required]
        [Display(Name = "Full Name")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Vehicle Plate")]
        public string VehiclePlate { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; } = string.Empty;
    }
}
