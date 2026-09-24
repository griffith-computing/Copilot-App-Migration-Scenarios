using System;
using System.ComponentModel.DataAnnotations;

namespace ProductsApp.Models
{
    /// <summary>
    /// Simple Code-First entity mapped by EF6. Data annotations here drive
    /// both EF6's schema generation and Web Forms' validator controls.
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
