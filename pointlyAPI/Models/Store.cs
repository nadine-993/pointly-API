using System.Diagnostics.Contracts;

namespace Pointly.Models
{
    public class Store
    {
        public int Id {get; set;}
        public int MerchantId {get; set;}
        public Merchant Merchant {get; set;} = null!;
        public string StoreName {get; set;} = string.Empty;

        public string Address {get; set;} = string.Empty;
        public string PhoneNumber {get; set;} = string.Empty;
        public double Latitude {get; set;}
        public double Longitude { get; set;}
        public DateTime CreateAt {get; set;} = DateTime.UtcNow;


    }
}