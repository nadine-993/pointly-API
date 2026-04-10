namespace Pointly.Models

{
    public class Wallet
    {
        public int Id {get; set;}
        public int CustomerId {get; set;}
        public Customer Customer {get; set;} = null!;
        public decimal PointsBalance {get; set;} = 0;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;



    }
}