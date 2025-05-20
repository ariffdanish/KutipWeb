using System.ComponentModel.DataAnnotations;

namespace KutipWeb.Models
{
    public class Route
    {
        [Key]
        public int RouteId { get; set; }
        public int ScheduleId { get; set; }
        public string Street { get; set; } = string.Empty;
        public string Area { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public required ICollection<Bin> Bins { get; set; }
    }
}
