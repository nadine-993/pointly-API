namespace Pointly.Models
{
  public class Admin
    {
        public int Id {get; set;}
        public int UserId {get; set;}
        public User User {get; set;}
        public decimal TotalPointsPool {get; set;}
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }  
}

