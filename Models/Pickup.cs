using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KutipWeb.Models
{
    public class Pickup
    {
        [Key]
        public int PickupId { get; set; }

        [Display(Name = "Bin")]
        public int BinId { get; set; }

        // Add navigation property for Bin
        public virtual required Bin Bin { get; set; }

        [Display(Name = "Collector")]
        public int CollectorId { get; set; }
        public virtual required Collector Collector { get; set; }  // single Collector navigation

        [Display(Name = "Pickup Time")]
        public DateTimeOffset PickupTime { get; set; }

        [Display(Name = "Pickup Status")]
        public Status status { get; set; }

        [Display(Name = "Photo")]
        public string PhotoUrl { get; set; } = "";
        [Required(ErrorMessage = "Upload Photo")]
        [Display(Name = "Upload Photo")]
        [NotMapped]
        public IFormFile Photo { get; set; } = null!;

        public enum Status
        {
            Pending, Completed, Cancelled
        }
    }
}
