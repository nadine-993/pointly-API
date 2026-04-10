namespace Pointly.Models
{
    public class Offer
    {
        public int Id {get; set;}
        public int MerchantId  {get; set;}
        public Merchant Merchant  {get; set;}= null!;
        public string Title  {get; set;} = string.Empty;
        public string Description  {get; set;} = string.Empty;
        public decimal PointsCost  {get; set;}
        public string ImageUrl  {get; set;}= string.Empty;
        public DateTime ValidFrom  {get; set;}
        public DateTime ValidUntil  {get; set;}
        public bool IsActive  {get; set;} = true;
        public int? QuantityAvailable  {get; set;}
        public DateTime CreatedAt {get; set;} = DateTime.UtcNow;

    }
}