namespace Pointly.Models
{
    public class Merchant
    {
       public int Id {get; set;}
       public int UserId {get; set;}
       public User User  {get; set;} = null!;
       public string BusinessName {get; set;} = string.Empty;
       public string Description {get; set;} = string.Empty;
       public string Address {get; set;} = string.Empty;
       public  string Category {get; set;} = string.Empty;
       public decimal PointsBalance {get; set;}
       public int? ChargedByChargetId {get; set;}
       public Charger? ChrargedByCharger {get; set;}
       public DateTime CreatedAt {get; set;} = DateTime.UtcNow;

       public ICollection<Store> Stores {get; set;} =new  List <Store> ();
       public ICollection<Offer> Offers{get; set;} = new List <Offer>();


    }
}