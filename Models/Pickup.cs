using System.ComponentModel.DataAnnotations;

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

        [Display(Name = "Before Photo")]
        public string BeforePhotoUrl { get; set; } = string.Empty;
        public string DuringPhotoUrl { get; set; } = string.Empty;
        public string AfterPhotoUrl { get; set; } = string.Empty;

        public enum Status
        {
            Pending, Completed, Cancelled
        }
    }
}
