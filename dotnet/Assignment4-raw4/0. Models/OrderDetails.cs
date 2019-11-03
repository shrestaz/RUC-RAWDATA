namespace _0._Models
{
    public class OrderDetails
    {
        public int Quantity { get; set; }
        public int Discount { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int UnitPrice { get; set; }
    }
}
