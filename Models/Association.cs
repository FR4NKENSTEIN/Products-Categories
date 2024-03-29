using System;
using System.ComponentModel.DataAnnotations;

namespace ProductsCategories.Models
{
    public class Association
    {
        [Key]
        public int AssociationId { get; set; }
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public Product Product { get; set; }
        public Category Category { get; set; }

    }
}