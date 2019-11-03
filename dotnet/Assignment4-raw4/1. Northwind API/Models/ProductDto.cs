using _0._Models;

namespace _1._Northwind_API.Models
{
    public class ProductDto
    {
        public string Link { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
        public string QuantityPerUnit { get; set; }
        public int UnitPrice { get; set; }
        public int UnitsInStock { get; set; }
        public string ProductName => Name;
        public string CategoryName => Category?.Name;
    }
}
