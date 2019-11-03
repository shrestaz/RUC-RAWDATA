using System.ComponentModel.DataAnnotations;

namespace _1._Northwind_API.Models
{
    public class ProductForCreation
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(15)]
        public string Category { get; set; }
        public string QuantityPerUnit { get; set; }
        public int UnitPrice { get; set; }
        public int UnitsInStock { get; set; }

    }
}
