namespace ZOLLA.TestTask.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
    }
}