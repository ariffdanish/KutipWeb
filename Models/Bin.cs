using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KutipWeb.Models
{
    public class Bin
    {
        [Key]
        public int BinId { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Bin ID")]
        public string PlateID { get; set; } = string.Empty;

        [StringLength(100)]
        [Display(Name = "Location")]
        public string Location { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Type")]
        public Description Description { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Created")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [DataType(DataType.DateTime)]
        [Display(Name = "Updated")]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public virtual List<Collector> Collectors { get; set; } = new List<Collector>();
    }

    public enum Description
    {
        Available,
        NotAvailable,
        Broken,
        New
    }
}
