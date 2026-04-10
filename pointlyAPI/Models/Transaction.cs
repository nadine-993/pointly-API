namespace Pointly.Models
{
    public enum TransactionType
    {
        Earned,
        Redeemed,
        Refunded
    }

    public class Transaction
    {
        public int Id {get; set;}
        public int CustomerId  {get; set;}
        public Customer Customer  {get; set;} = null!;
        public int MerchantId  {get; set;}
        public Merchant Merchant  {get; set;} = null!;
        public TransactionType Type  {get; set;}
        public decimal PointsAmount  {get; set;}
        public decimal PurchaseAmount  {get; set;}
        public string Description { get; set; } = string.Empty;
        public int? OfferId {get; set;}
        public Offer? Offer {get; set;}
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


    }
}