namespace Pointly.DTOs

{
    public class CreateChargerRequest
    {
        public string Email {get; set;} = string.Empty;
        public string Password {get; set;} = string.Empty;
        public string FullName {get; set;} = string.Empty;
        public string PhoneNumber {get; set;}= string.Empty;
    }

    public class AssignPointsToChargerRequest
    {
        public int ChargerId {get; set;}
        public decimal PointAmount {get; set;}
        public string Notes {get; set;} = string.Empty;

    }

    public class ChargerPointsToMerchantRequest
    {
        public int MerchantId {get; set;}
        public decimal PointsAmount {get; set;}
        public string Notes {get; set;} = string.Empty;
    }
    public class ChargerResponse
    {
        public int Id {get; set;}
        public string Email {get; set; } = string.Empty;
        public string FullName {get; set;} = string.Empty;
        public string PhoneNumber {get; set;}= string.Empty;
        public decimal AvailablePoints {get; set;}
        public DateTime CreatedAt {get; set;}
    }
}