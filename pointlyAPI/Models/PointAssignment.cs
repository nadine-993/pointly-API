namespace Pointly.Models
{
    public enum AssignmentType
    {
        AdminToCharger,
        ChargerToMerchant
    }

    public class PointAssignment
    {
        public int Id {get; set;}
        public AssignmentType Type {get; set;}
        public int? FromAdminId {get; set;}
        public Admin? FromAdmin { get; set;}
        public int? FromChargerId {get;set;}
        public Charger? FromCharger {get; set;}
        public int? ToChargerId {get; set;}
        public Charger? ToCharger {get; set;}
        public int? ToMerchantId {get; set;}
        public Merchant? ToMerchant {get; set;}
        public decimal PointsAmount {get; set;}
        public string Notes { get; set;}= string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        


 


    }
}