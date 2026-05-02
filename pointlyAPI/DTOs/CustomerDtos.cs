namespace Poitnly.DTOs
{
    public class CreateCustomerRequest
    {
        public string Email {get; set;} = string.Empty;
        public string Password {get; set;} = string.Empty;
        public string FullName {get; set;} = string.Empty;
        public string PhoneNumber {get; set;} = string.Empty;
    }

    public class RecordPurchaseRequest
    {
        public int CustomerId {get; set;}
        public int MerchantId {get; set;}
        public decimal PurchaseAmount {get; set;}
        public string Description {get; set;} = string.Empty;
    }

    public class RedeemOfferRequest
    {
        public int CustomerId {get; set;}
        public int OfferId {get; set;}
    }

    public class CustomerResponse
    {
        public int Id {get; set;}
        public string Email {get; set;} = string.Empty;
        public string FullName {get; set;} = string.Empty;
        public string PhoneNumber {get; set;} = string.Empty;
        public decimal PointsBalance {get; set;}
        public DateTime CreatedAt {get; set;}
    }
}