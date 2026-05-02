namespace Pointly.DTOs
{
    public class CreateOfferRequest
    {
        public int MerchantId {get; set;} 
        public string Title {get; set;} = string.Empty;
        public string Description {get; set;} = string.Empty;
        public decimal PointsCost {get; set;}
        public string ImageUrl {get; set;} = string.Empty;
        public DateTime ValidFrom {get; set;}
        public DateTime ValidUntil {get; set;}
        public int? QuantityAvailable {get; set;}

    }

    public class OfferResponse
    {
        public int Id {get; set;}
        public int MerchantId {get; set;} 
        public string MerchantName { get; set; } = string.Empty;

        public string Title {get; set;} = string.Empty;
        public string Description {get; set;} = string.Empty;
        public decimal PointsCost {get; set;}
        public string ImageUrl {get; set;} = string.Empty;
        public DateTime ValidFrom {get; set;}
        public DateTime ValidUntil {get; set;}
        public bool IsActive {get; set;}
        public int? QuantityAvailable {get; set;}
        public DateTime CreatedAt {get; set;}

    }
}