namespace ECommerceNet.Models
{
    public class PurchaseHistory
    {
        public int Id { get; set; }
        public bool PurchaseSuccess { get; set; }
        public int UserId { get; set; }
        public int CartId { get; set; }

    }
}
