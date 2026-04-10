using System.Transactions;

namespace Pointly.Models
{
    public class Customer
    {
        public int Id {get; set;}
        public int UserId {get; set;}
        public User User {get; set;} = null!;

        public DateTime CreatedAt {get; set;} = DateTime.UtcNow;

        public Wallet? Wallet {get; set;}
        public ICollection <Transaction> Transactions {get; set;} = new List<Transaction>(); 

    }
}