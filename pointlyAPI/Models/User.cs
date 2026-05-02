namespace Pointly.Models
{
    public enum UserRole
    {
        Admin,
        Charger,
        Merchant,
        Customer,
    }

    public class User
    {
        public int Id {get; set; }
        public String Email {get; set;} = string.Empty;
        public String PasswordHash {get; set;} = string.Empty;
        public String FullName {get; set;} = string.Empty;
        public String PhoneNumber{get; set;} = string.Empty;
        public UserRole Role {get; set;}
         public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;


    }
}