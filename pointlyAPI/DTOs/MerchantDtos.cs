using System.ComponentModel;

namespace Poitnly.DTOs
{
    public class CreateMerchantRequest
    {
        public string Email {get; set;} = string.Empty;
        public String Password{get; set;} = string.Empty;
        public string FullName {get; set;} = string.Empty;
        public string PhoneNumber {get; set;} = string.Empty;
        public string BusinessName {get; set;} = string.Empty;
        public string Description {get; set;} = string.Empty;
        public string Address {get; set;} = string.Empty;
        public string Category {get; set;} = string.Empty;
    }

    public class MerchantResponse
    {
        public int Id {get; set;}
        public string Email {get; set;} = string.Empty;
        public string FullName {get; set;} = string.Empty;
        public string PhoneNumber {get; set;} = string.Empty;
        public string BusinessName {get; set;} = string.Empty;
        public string  Description {get; set;} = string.Empty;
        public string Address {get; set;} = string.Empty;
        public string Category {get; set;} = string.Empty;
        public decimal PointsBalance {get; set;}
        public DateTime CreatedAt {get; set;}
    }
}