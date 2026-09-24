using System;
using System.ComponentModel.DataAnnotations;

namespace ProductsDesktop.Models
{
    /// <summary>
    /// Same shape as the Product entity in the scenario 1 web app, so the
    /// two demos tell a consistent story. Kept as its own copy (not a
    /// shared library) so each scenario remains independently runnable.
    /// </summary>
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Category { get; set; }

        [Range(0, 100000)]
        public decimal Price { get; set; }

        public bool InStock { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
