using System;
using System.ComponentModel.DataAnnotations;

namespace ProductsService.Models
{
    /// <summary>
    /// Same shape as the Product entity in scenarios 1 and 2, kept as its
    /// own copy so this scenario stays independently runnable.
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
